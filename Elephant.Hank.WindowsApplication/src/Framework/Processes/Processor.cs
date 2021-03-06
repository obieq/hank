﻿// ---------------------------------------------------------------------------------------------------
// <copyright file="Processor.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The Processor class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Processes
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web.Script.Serialization;

    using Elephant.Hank.WindowsApplication.Framework.ApiHelper;
    using Elephant.Hank.WindowsApplication.Framework.Emailer;
    using Elephant.Hank.WindowsApplication.Framework.FileHelper;
    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Messages;
    using Elephant.Hank.WindowsApplication.Resources.Constants;
    using Elephant.Hank.WindowsApplication.Resources.Extensions;
    using Elephant.Hank.WindowsApplication.Resources.Models;

    /// <summary>
    /// The Processor class
    /// </summary>
    public static class Processor
    {
        /// <summary>
        /// The hub information
        /// </summary>
        public static readonly ConcurrentDictionary<Guid, Hub> HubInfo = new ConcurrentDictionary<Guid, Hub>();

        /// <summary>
        /// The queued test
        /// </summary>
        public static readonly ConcurrentDictionary<Guid, ResultMessage<List<TestQueue>>> QueuedTest = new ConcurrentDictionary<Guid, ResultMessage<List<TestQueue>>>();

        /// <summary>
        /// The maximum re try
        /// </summary>
        private const int MaxReTry = 10;

        /// <summary>
        /// The maximum re try gap
        /// </summary>
        private const int MaxReTryGapMs = 2000;

        /// <summary>
        /// Executes the service.
        /// </summary>
        /// <param name="isPauseEvent">if set to <c>true</c> [is pause event].</param>
        public static void ExecuteService(bool isPauseEvent)
        {
            try
            {
                if (!isPauseEvent)
                {
                    var testQueue = TestDataApi.Get<List<TestQueue>>(EndPoints.GetTestQueue);
                    if (!testQueue.IsError && testQueue.Item != null && testQueue.Item.Any())
                    {
                        var testQueueFirst = testQueue.Item.First();

                        testQueue.SchedulerId = testQueueFirst.SchedulerId;
                        testQueue.TestQueueId = testQueueFirst.Id;
                        testQueue.SeleniumAddress = testQueueFirst.Settings.SeleniumAddress;

                        var updateResult = TestDataApi.Get<bool>(string.Format(EndPoints.BulkUpdateTestQueue, testQueueFirst.GroupName, 1));

                        if (!updateResult.IsError)
                        {
                            SendTestToHub(testQueue);
                        }
                        else
                        {
                            var data = updateResult.Messages.Aggregate(string.Empty, (current, message) => current + (message.Name + ":" + message.Value + "\n"));
                            LoggerService.LogException("ExecuteService UpdateResult: " + data);
                        }
                    }
                    else
                    {
                        ProcessPendingQueue();
                    }
                }
                else
                {
                    ProcessPendingQueue();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogException("ExecuteService: " + ex.Message);
            }
        }

        /// <summary>
        /// Processes the pending queue.
        /// </summary>
        private static void ProcessPendingQueue()
        {
            foreach (var item in QueuedTest.OrderBy(x => x.Value.CreatedOn))
            {
                SendTestToHub(item.Value, item.Key);
            }
        }

        /// <summary>
        /// Sends the test to hub.
        /// </summary>
        /// <param name="testQueue">The test queue.</param>
        /// <param name="key">The key.</param>
        private static void SendTestToHub(ResultMessage<List<TestQueue>> testQueue, Guid key = new Guid())
        {
            Hub hub = GetHubBySeleniumAddress(testQueue.Item[0].Settings.SeleniumAddress);
            if (hub == null)
            {
                Hub hubCreated = AddHub(Guid.NewGuid(), testQueue.Item[0].Settings.SeleniumAddress);
                hubCreated.SchedulerId = testQueue.Item[0].SchedulerId;
                hubCreated.TestQueueId = testQueue.Item[0].Id;

                testQueue.Item.ForEach(x => x.HubInfo = hubCreated);
                ThreadPool.QueueUserWorkItem(ExecuteServiceThread, testQueue);

                DeleteFromQueueTest(key, testQueue.Item[0].Settings.SeleniumAddress);
            }
            else if (key == Guid.Empty)
            {
                AddToQueueTest(testQueue);
            }
        }

        /// <summary>
        /// Executes the service thread.
        /// </summary>
        /// <param name="testQueueData">The test queue data.</param>
        public static void ExecuteServiceThread(object testQueueData)
        {
            try
            {
                ResultMessage<List<TestQueue>> testQueue = testQueueData as ResultMessage<List<TestQueue>>;
                FileGenerator fileGenerator = new FileGenerator(testQueue.Item);
                string directoryName = fileGenerator.GenerateSpecFiles();

                if (!string.IsNullOrWhiteSpace(directoryName))
                {
                    new ProtractorConfigJsBuilder().Create(testQueue.Item[0], testQueue.Item.Count);

                    string groupName = testQueue.Item[0].GroupName;

                    var schedulerHistory = testQueue.Item[0].SchedulerId > 0
                                               ? TestDataApi.Get<SchedulerHistory>(string.Format(EndPoints.SchedulerHistory, groupName))
                                               : new ResultMessage<SchedulerHistory>();

                    if (schedulerHistory.Item == null || !schedulerHistory.Item.IsCancelled)
                    {
                        TestDataApi.Post(string.Format(EndPoints.SchedulerHistoryStatus, groupName, (int)SchedulerExecutionStatus.InProgress), new List<SchedulerHistory>());

                        ProtractorCommandRunner protractorCommandRunner = new ProtractorCommandRunner();

                        var status = protractorCommandRunner.ExecuteCommand(groupName);

                        schedulerHistory = testQueue.Item[0].SchedulerId > 0
                           ? TestDataApi.Get<SchedulerHistory>(string.Format(EndPoints.SchedulerHistory, groupName))
                           : new ResultMessage<SchedulerHistory>();

                        status = (schedulerHistory.Item == null || !schedulerHistory.Item.IsCancelled)
                                     ? status
                                     : SchedulerExecutionStatus.Cancelled;

                        TestDataApi.Post(string.Format(EndPoints.SchedulerHistoryStatus, groupName, (int)status), new List<SchedulerHistory>());

                        ProcessUnprocessedResultWithJson(groupName);
                    }
                    else
                    {
                        TestDataApi.Post(string.Format(EndPoints.SchedulerHistoryStatus, groupName, (int)SchedulerExecutionStatus.Cancelled), new List<SchedulerHistory>());
                    }

                    ProcessEmail(testQueue, groupName);

                    ImageProcessor.ProcessImages(groupName);
                }

                Hub hubInfo = testQueue.Item.First().HubInfo;

                DeleteHub(hubInfo.ProcessId, hubInfo.SeleniumAddress);
            }
            catch (Exception ex)
            {
                ResultMessage<List<TestQueue>> testQueue = testQueueData as ResultMessage<List<TestQueue>>;
                Hub hubInfo = testQueue.Item.First().HubInfo;
                DeleteHub(hubInfo.ProcessId, hubInfo.SeleniumAddress);
                LoggerService.LogException("ExecuteServiceThread: " + ex.Message);
            }
        }

        /// <summary>
        /// Processes the email.
        /// </summary>
        /// <param name="testQueue">The test queue.</param>
        /// <param name="groupName">Name of the group.</param>
        private static void ProcessEmail(ResultMessage<List<TestQueue>> testQueue, string groupName)
        {
            var emailStatus = SchedulerHistoryEmailStatus.NotSent;

            if (testQueue != null && testQueue.Item != null && groupName.IsNotBlank())
            {
                var schedulerIds = testQueue.Item.Select(x => x.SchedulerId).Distinct();

                var resultData = TestDataApi.Post<SearchReportObject, SearchReportResult>(EndPoints.ReportSearch, new SearchReportObject { ExecutionGroup = groupName });

                if (resultData == null || resultData.IsError)
                {
                    emailStatus = SchedulerHistoryEmailStatus.SendException;
                }
                else if (resultData.Item != null)
                {
                    var emailProcessor = new EmailProcessor();

                    foreach (var schedulerId in schedulerIds)
                    {
                        if (!schedulerId.HasValue)
                        {
                            continue;
                        }

                        var schedularData = TestDataApi.Get<Scheduler>(string.Format(EndPoints.SchedulerById, schedulerId));

                        if (schedularData != null && !schedularData.IsError && schedularData.Item != null)
                        {
                            var repostData = new ReportResultData(resultData.Item.Data, schedularData.Item, groupName);
                            emailStatus = emailProcessor.EmailReport(repostData);
                        }
                    }
                }
            }

            TestDataApi.Post(string.Format(EndPoints.SchedulerHistoryEmailStatus, groupName, (int)emailStatus), new List<SchedulerHistory>());
        }

        /// <summary>
        /// Executes the cleaner.
        /// </summary>
        public static void ExecuteCleaner()
        {
            var settings = SettingsHelper.Get();
            FileFolderRemover.DeleteOldFilesFolders(settings.BaseSpecPath, (uint)settings.ClearLogHours);
            FileFolderRemover.DeleteOldFilesFolders(settings.BaseTestLogPath, (uint)settings.ClearLogHours);
            FileFolderRemover.DeleteOldFilesFolders(settings.LogsLocation, (uint)settings.ClearLogHours);
            FileFolderRemover.DeleteOldFilesFolders(settings.BaseReportPath, (uint)settings.ClearReportHours);
        }

        /// <summary>
        /// Adds the hub.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <param name="seleniumAddress">The selenium address.</param>
        /// <returns>Hub object</returns>
        private static Hub AddHub(Guid processId, string seleniumAddress)
        {
            var hub = new Hub { ProcessId = processId, SeleniumAddress = seleniumAddress };
            HubInfo[processId] = hub;
            LoggerService.LogInformation(string.Format("AddHub: Added New Entry Selenium address: {0} and ProcessId: {1}", seleniumAddress, processId));
            return hub;
        }

        /// <summary>
        /// Deletes the hub.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <param name="seleniumAddress">The selenium address.</param>
        /// <param name="tryCount">The try count.</param>
        private static void DeleteHub(Guid processId, string seleniumAddress, int tryCount = 0)
        {
            if (tryCount > MaxReTry)
            {
                LoggerService.LogInformation(string.Format("DeleteHub: Max Try Reached Error Hun Locked Selenium address: {0}  and ProcessId: {1}, Try Count: {2}", seleniumAddress, processId, tryCount));
                return;
            }

            var hub = GetHubBySeleniumAddress(seleniumAddress);
            if (hub != null && HubInfo.ContainsKey(hub.ProcessId))
            {
                bool isRemoved = HubInfo.TryRemove(hub.ProcessId, out hub);

                if (!isRemoved)
                {
                    LoggerService.LogInformation(string.Format("DeleteHub: Try Count: {2} Error Hun Locked Selenium address: {0}  and ProcessId: {1}, Try Count: {2}", seleniumAddress, processId, tryCount));
                    Thread.Sleep(MaxReTryGapMs);
                    DeleteHub(processId, seleniumAddress, tryCount++);
                }
                else
                {
                    LoggerService.LogInformation(string.Format("DeleteHub: Successfully Selenium address: {0} and ProcessId: {1}, Try Count: {2}", seleniumAddress, processId, tryCount));
                }
            }
        }

        /// <summary>
        /// The add to queue test.
        /// </summary>
        /// <param name="testQueue">
        /// The test queue.
        /// </param>
        private static void AddToQueueTest(ResultMessage<List<TestQueue>> testQueue)
        {
            Guid testQueueId = Guid.NewGuid();
            LoggerService.LogInformation(string.Format("AddToQueueTest: Added New Entry Selenium address : {0} and ProcessId: {1}", testQueue.Item[0].Settings.SeleniumAddress, testQueueId));
            QueuedTest[testQueueId] = testQueue;
        }

        /// <summary>
        /// Deletes from queue test.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <param name="seleniumAddress">The selenium address.</param>
        /// <param name="tryCount">The try count.</param>
        private static void DeleteFromQueueTest(Guid processId, string seleniumAddress, int tryCount = 0)
        {
            if (tryCount > MaxReTry)
            {
                LoggerService.LogInformation(string.Format("DeleteFromQueueTest: Max Try Reached Error Hun Locked Selenium address: {0}  and ProcessId: {1}, Try Count: {2}", seleniumAddress, processId, tryCount));
                return;
            }

            if (QueuedTest.ContainsKey(processId))
            {
                ResultMessage<List<TestQueue>> hub;
                bool isRemoved = QueuedTest.TryRemove(processId, out hub);

                if (!isRemoved)
                {
                    LoggerService.LogInformation(string.Format("DeleteFromQueueTest: Error Hun Locked Selenium address: {0}  and ProcessId: {1}, Try Count: {2}", seleniumAddress, processId, tryCount));
                    Thread.Sleep(MaxReTryGapMs);
                    DeleteFromQueueTest(processId, seleniumAddress, tryCount++);
                }
                else
                {
                    LoggerService.LogInformation(string.Format("DeleteFromQueueTest: Successfully Selenium address: {0} and ProcessId: {1}, Try Count: {2}", seleniumAddress, processId, tryCount));
                }
            }
        }

        /// <summary>
        /// Gets the hub by selenium address.
        /// </summary>
        /// <param name="seleniumAddress">The selenium address.</param>
        /// <returns>Hub object</returns>
        private static Hub GetHubBySeleniumAddress(string seleniumAddress)
        {
            return HubInfo.Values.FirstOrDefault(u => u.SeleniumAddress == seleniumAddress);
        }

        /// <summary>
        /// Processes the unprocessed result with json.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        private static void ProcessUnprocessedResultWithJson(string groupName)
        {
            try
            {
                var settings = SettingsHelper.Get();
                var resultData = TestDataApi.Get<List<ReportData>>(string.Format(EndPoints.GetAllUnprocessedReports, groupName));
                if (!resultData.IsError)
                {
                    foreach (var item in resultData.Item)
                    {
                        string path = settings.BaseReportPath + "\\" + groupName + "\\JSON\\" + item.TestQueueId + "-" + item.Os.ToLower() + "-" + item.BrowserName.ToLower() + ".json";
                        if (File.Exists(path))
                        {
                            string jsonString = File.ReadAllText(path);
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            object output = serializer.Deserialize<object>(jsonString);
                            TestDataApi.Post<object>("api/report", output);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogException("ProcessUnprocessedResultWithJson: " + ex.Message);
            }
        }
    }
}
