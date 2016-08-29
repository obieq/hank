// ---------------------------------------------------------------------------------------------------
// <copyright file="TestQueueExecutableService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-25</date>
// <summary>
//     The TestQueueExecutableService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Elephant.Hank.Resources.ViewModal;

    /// <summary>
    /// The TestQueueExecutableService class
    /// </summary>
    public class TestQueueExecutableService : ITestQueueExecutableService
    {
        /// <summary>
        /// The suite service
        /// </summary>
        private readonly ISuiteService suiteService;

        /// <summary>
        /// The scheduler service
        /// </summary>
        private readonly ISchedulerService schedulerService;

        /// <summary>
        /// The scheduler history service
        /// </summary>
        private readonly ISchedulerHistoryService schedulerHistoryService;

        /// <summary>
        /// The test queue service
        /// </summary>
        private readonly ITestQueueService testQueueService;

        /// <summary>
        /// The test queue service
        /// </summary>
        private readonly IActionsService actionService;

        /// <summary>
        /// the browser service
        /// </summary>
        private readonly IBrowserService browserService;

        /// <summary>
        /// the browser service
        /// </summary>
        private readonly IApiConnectionService apiConncetionService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The test data shared test data map service
        /// </summary>
        private readonly ITestDataSharedTestDataMapService testDataSharedTestDataMapService;

        /// <summary>
        /// The shared test data service
        /// </summary>
        private readonly ISharedTestDataService sharedTestDataService;

        /// <summary>
        /// The environment service
        /// </summary>
        private readonly IEnvironmentService environmentService;

        /// <summary>
        /// The table test data
        /// </summary>
        private readonly IRepository<TblTestData> table;

        /// <summary>
        /// The automatic gen array
        /// </summary>
        private List<AutoGenModel> autoGenArray;

        /// <summary>
        /// Gets or sets the testPlan
        /// </summary>
        private List<TblTestDataDto> testPlan = new List<TblTestDataDto>();

        /// <summary>
        /// The this lock
        /// </summary>
        private object thisLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="TestQueueExecutableService" /> class.
        /// </summary>
        /// <param name="table">The table test data.</param>
        /// <param name="suiteService">The suite service.</param>
        /// <param name="testQueueService">The test queue service.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="schedulerService">The scheduler service.</param>
        /// <param name="browserService">The browser service.</param>
        /// <param name="actionService">The action service.</param>
        /// <param name="testDataSharedTestDataMapService">The test data shared test data map service.</param>
        /// <param name="sharedTestDataService">The shared test data service.</param>
        /// <param name="apiConncetionService">The API conncetion service.</param>
        /// <param name="schedulerHistoryService">The scheduler history service.</param>
        /// <param name="environmentService">The environment service.</param>
        public TestQueueExecutableService(
            IRepository<TblTestData> table,
            ISuiteService suiteService,
            ITestQueueService testQueueService,
            IMapperFactory mapperFactory,
            ISchedulerService schedulerService,
            IBrowserService browserService,
            IActionsService actionService,
            ITestDataSharedTestDataMapService testDataSharedTestDataMapService,
            ISharedTestDataService sharedTestDataService,
            IApiConnectionService apiConncetionService,
            ISchedulerHistoryService schedulerHistoryService,
            IEnvironmentService environmentService)
        {
            this.table = table;
            this.suiteService = suiteService;
            this.mapperFactory = mapperFactory;
            this.schedulerService = schedulerService;
            this.testQueueService = testQueueService;
            this.browserService = browserService;
            this.actionService = actionService;
            this.testDataSharedTestDataMapService = testDataSharedTestDataMapService;
            this.sharedTestDataService = sharedTestDataService;
            this.apiConncetionService = apiConncetionService;
            this.schedulerHistoryService = schedulerHistoryService;
            this.environmentService = environmentService;
        }

        /// <summary>
        /// Gets or sets the executionSequence
        /// </summary>
        private long ExecutionSequence { get; set; }

        /// <summary>
        /// process test data to generate testplan
        /// </summary>
        /// <param name="testDataDto">the test data object</param>
        /// <param name="testQueue">the test queue object</param>
        /// <param name="deepIndex">Index of the deep.</param>
        public void ProcessTestQueueExecutableData(IEnumerable<TblTestDataDto> testDataDto, TblTestQueueDto testQueue, int deepIndex = 0)
        {
            if (testDataDto != null && testDataDto.Any())
            {
                bool breakParentLoop = false;

                foreach (var item in testDataDto)
                {
                    if (item.ActionId == ActionConstants.Instance.IgnoreLoadNeUrlActionId && this.testPlan.Any() && this.testPlan.Last().ActionId.Value == ActionConstants.Instance.LoadNewUrlActionId)
                    {
                        this.testPlan.Remove(this.testPlan.Last());
                        this.ExecutionSequence--;
                        continue;
                    }

                    switch (item.LinkTestType)
                    {
                        case (int)LinkTestType.TestStep:
                            {
                                if (item.ActionId.Value != ActionConstants.Instance.TerminateTestActionId)
                                {
                                    item.ExecutionSequence = this.ExecutionSequence++;
                                    this.testPlan.Add(item);
                                }
                                else
                                {
                                    breakParentLoop = true;
                                }

                                break;
                            }

                        case (int)LinkTestType.ApiTestStep:
                            {
                                long environMentId;
                                if (testQueue.SchedulerId.HasValue)
                                {
                                    ResultMessage<TblSchedulerDto> schedulerDto = this.schedulerService.GetById(testQueue.SchedulerId.Value);
                                    environMentId = schedulerDto.Item.UrlId;
                                    if (environMentId == 0)
                                    {
                                        environMentId = this.environmentService.GetDefaultEnvironment().Item.Id;
                                    }
                                }
                                else
                                {
                                    environMentId = testQueue.Settings.UrlId;
                                    if (environMentId == 0)
                                    {
                                        environMentId = this.environmentService.GetDefaultEnvironment().Item.Id;
                                    }
                                }

                                ResultMessage<TblApiConnectionDto> apiConnectionDto = this.apiConncetionService.GetByEnvironmentAndCategoryId(environMentId, item.ApiTestData.ApiCategoryId.Value);
                                item.ApiUrl = (item.ApiTestData.ApiUrl.IsBlank() ? apiConnectionDto.Item.BaseUrl : item.ApiTestData.ApiUrl).Trim();

                                item.ApiUrl += ((item.ApiUrl.EndsWith("\\") || item.ApiUrl.EndsWith("/")) ? string.Empty : "/") + item.ApiTestData.EndPoint;

                                item.Headers = item.ApiTestData.Headers ?? new List<NameValuePair>();

                                if (apiConnectionDto.Item.Headers != null)
                                {
                                    foreach (var header in apiConnectionDto.Item.Headers)
                                    {
                                        if (!item.Headers.Any(x => x.Name.EqualsIgnoreCase(header.Name)))
                                        {
                                            item.Headers.Add(header);
                                        }
                                    }
                                }

                                item.IgnoreHeaders = item.ApiTestData.IgnoreHeaders;
                                item.RequestType = item.ApiTestData.RequestName;
                                item.RequestBody = item.ApiTestData.RequestBody;
                                item.ExecutionSequence = this.ExecutionSequence++;
                                this.testPlan.Add(item);
                                break;
                            }

                        case (int)LinkTestType.SqlTestStep:
                            {
                                item.ExecutionSequence = this.ExecutionSequence++;
                                this.testPlan.Add(item);
                                break;
                            }

                        case (int)LinkTestType.SharedTest:
                            {
                                var sharedSteps = item.SharedTest.SharedTestDataList.Where(x => !x.IsDeleted).OrderBy(x => x.ExecutionSequence).ToList();
                                foreach (var sharedStep in sharedSteps)
                                {
                                    var lnkSharedTestStep = item.SharedTestSteps.FirstOrDefault(x => x.SharedTestDataId == sharedStep.Id && x.IsDeleted != true);
                                    if (lnkSharedTestStep != null)
                                    {
                                        if (lnkSharedTestStep.NewOrder > 0)
                                        {
                                            sharedSteps.Where(x => x.ExecutionSequence > lnkSharedTestStep.NewOrder).ToList().ForEach(x => x.ExecutionSequence++);
                                            sharedStep.ExecutionSequence = lnkSharedTestStep.NewOrder + 1;
                                        }

                                        if (!string.IsNullOrEmpty(lnkSharedTestStep.NewValue))
                                        {
                                            sharedStep.Value = lnkSharedTestStep.NewValue;
                                        }

                                        if (!string.IsNullOrEmpty(lnkSharedTestStep.NewVariable))
                                        {
                                            sharedStep.VariableName = lnkSharedTestStep.NewVariable;
                                        }

                                        sharedStep.IsIgnored = lnkSharedTestStep.IsIgnored ?? false;
                                    }

                                    if (sharedStep.ActionId == ActionConstants.Instance.TerminateTestActionId && !sharedStep.IsIgnored)
                                    {
                                        breakParentLoop = true;
                                        break;
                                    }
                                }

                                sharedSteps.RemoveAll(m => m.IsIgnored);
                                int indx = sharedSteps.IndexOf(sharedSteps.FirstOrDefault(m => m.ActionId == ActionConstants.Instance.TerminateTestActionId));

                                if (indx >= 0)
                                {
                                    sharedSteps.RemoveRange(indx, sharedSteps.Count - indx);
                                }

                                var mapper = this.mapperFactory.GetMapper<TblSharedTestDataDto, TblTestDataDto>();
                                var sharedStepMappedWithTestData = sharedSteps.Select(mapper.Map).OrderBy(x => x.ExecutionSequence).ToList();

                                long es = this.ExecutionSequence;
                                sharedStepMappedWithTestData.ForEach(x => { x.ExecutionSequence = es++; x.IsStepBelongsToSharedComponent = true; x.Id = item.Id; });
                                this.ExecutionSequence = es;
                                this.testPlan.AddRange(sharedStepMappedWithTestData);
                                break;
                            }

                        case (int)LinkTestType.SharedWebsiteTest:
                            {
                                var testData = this.table.Find(x => x.TestId == item.SharedStepWebsiteTestId && !x.IsDeleted).OrderBy(x => x.ExecutionSequence).ToList();
                                var mapper = this.mapperFactory.GetMapper<TblTestData, TblTestDataDto>();
                                var testDataDtoForSharedWebsiteTest = testData.Select(mapper.Map).OrderBy(x => x.ExecutionSequence).ToList();
                                var website = this.mapperFactory.GetMapper<TblWebsite, TblWebsiteDto>().Map(testData[0].Test.Website);

                                WebsiteUrl urlData;
                                if (testQueue.SuiteId.HasValue)
                                {
                                    var schedular = this.schedulerService.GetById(testQueue.SchedulerId.Value);
                                    urlData = website.WebsiteUrlList.FirstOrDefault(m => m.Id == schedular.Item.UrlId) ?? new WebsiteUrl();
                                }
                                else
                                {
                                    urlData = website.WebsiteUrlList.FirstOrDefault(m => m.Id == testQueue.Settings.UrlId) ?? new WebsiteUrl();
                                }

                                if (!this.testPlan.Any() || (this.testPlan.Last().ActionId.Value != ActionConstants.Instance.IgnoreLoadNeUrlActionId))
                                {
                                    TblTestDataDto dummyStep = new TblTestDataDto
                                                               {
                                                                   ActionValue = this.actionService.GetById(ActionConstants.Instance.LoadNewUrlActionId).Item.Value,
                                                                   ActionId = ActionConstants.Instance.LoadNewUrlActionId,
                                                                   Value = urlData.Url,
                                                                   LinkTestType = (int)LinkTestType.TestStep
                                                               };
                                    testDataDtoForSharedWebsiteTest.Insert(0, dummyStep);
                                }

                                if (testDataDtoForSharedWebsiteTest.Any())
                                {
                                    testDataDtoForSharedWebsiteTest.Add(new TblTestDataDto
                                    {
                                        ActionValue = this.actionService.GetById(ActionConstants.Instance.LoadNewUrlActionId).Item.Value,
                                        ActionId = ActionConstants.Instance.LoadNewUrlActionId,
                                        Value = "ind#" + deepIndex
                                    });
                                }

                                this.ProcessTestQueueExecutableData(testDataDtoForSharedWebsiteTest, testQueue, deepIndex + 1);
                                break;
                            }
                    }

                    if (breakParentLoop)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the suite executable data by test case identifier.
        /// </summary>
        /// <param name="testQueueId">The test queue identifier.</param>
        /// <returns>GetTestQueueExecutableData object</returns>
        public ResultMessage<TestQueue_FullTestData> GetTestQueueExecutableData(long testQueueId)
        {
            var resultMessage = new ResultMessage<TestQueue_FullTestData>();
            this.ExecutionSequence = 1;
            var testQueue = this.testQueueService.GetById(testQueueId);
            if (testQueue.Item != null)
            {
                bool isCancelled = false;

                if (testQueue.Item.SchedulerId.HasValue)
                {
                    var schedulerHistory = this.schedulerHistoryService.GetByGroupName(testQueue.Item.GroupName);

                    if (!schedulerHistory.IsError)
                    {
                        isCancelled = schedulerHistory.Item.IsCancelled;
                    }
                }

                if (testQueue.Item.SuiteId.HasValue)
                {
                    var testSuite = this.suiteService.GetById(testQueue.Item.SuiteId.Value);
                    resultMessage.Messages.AddRange(testSuite.Messages);

                    if (!resultMessage.IsError)
                    {
                        this.autoGenArray = new List<AutoGenModel>();

                        resultMessage.Item = new TestQueue_FullTestData
                        {
                            Suite = testSuite.Item,
                            IsCancelled = isCancelled
                        };

                        var testData = this.table.Find(x => x.TestId == testQueue.Item.TestId && !x.IsDeleted).OrderBy(x => x.ExecutionSequence).ToList();

                        var mapper = this.mapperFactory.GetMapper<TblTestData, TblTestDataDto>();
                        var testDataDto = testData.Select(mapper.Map).OrderBy(x => x.ExecutionSequence).ToList();
                        this.ProcessTestQueueExecutableData(testDataDto, testQueue.Item);

                        var mapperExecutableTestData = this.mapperFactory.GetMapper<TblTestDataDto, ExecutableTestData>();
                        List<ExecutableTestData> exeTestData = this.testPlan.Select(mapperExecutableTestData.Map).OrderBy(x => x.ExecutionSequence).ToList();
                        resultMessage.Item.TestData = exeTestData;

                        if (resultMessage.Item.TestData.Count > 0)
                        {
                            var webSiteData = testData[0].Test.Website;

                            if (testQueue.Item.SchedulerId.HasValue)
                            {
                                var schedular = this.schedulerService.GetById(testQueue.Item.SchedulerId.Value);

                                if (!schedular.IsError)
                                {
                                    if (schedular.Item.Settings.TakeScreenShotOnUrlChangedTestId == testData[0].Test.Id)
                                    {
                                        resultMessage.Item.TakeScreenShot = true;
                                        ResultMessage<TblBrowsersDto> browser = this.browserService.GetById(schedular.Item.Settings.TakeScreenShotOnUrlChanged);
                                        resultMessage.Item.TakeScreenShotBrowser = browser.Item;
                                    }

                                    resultMessage.Item.UrlToTest = schedular.Item.Url;
                                    if (resultMessage.Item.UrlToTest.IsBlank())
                                    {
                                        resultMessage.Item.UrlToTest = (schedular.Item.Settings.CustomUrlToTest + string.Empty)
                                                .Replace("{target}", schedular.Item.Settings.Target)
                                                .Replace("{port}", schedular.Item.Settings.Port + string.Empty);
                                    }
                                }
                            }
                            else
                            {
                                resultMessage.Item.UrlToTest = testQueue.Item.Settings.CustomUrlToTest;

                                if (resultMessage.Item.UrlToTest.IsBlank() && webSiteData.Settings.BuildUrlTemplate.IsNotBlank())
                                {
                                    resultMessage.Item.UrlToTest = webSiteData.Settings.BuildUrlTemplate
                                            .Replace("{target}", testQueue.Item.Settings.Target)
                                            .Replace("{port}", testQueue.Item.Settings.Port + string.Empty);
                                }
                            }

                            resultMessage.Item.TestCase = this.mapperFactory.GetMapper<TblTest, TblTestDto>().Map(testData[0].Test);
                            resultMessage.Item.Website = this.mapperFactory.GetMapper<TblWebsite, TblWebsiteDto>().Map(webSiteData);
                        }

                        resultMessage.Item.TestData.ForEach(x => x.Value = this.IsAutoGenField(x.Value));
                        this.ResolveLocatorText(resultMessage.Item.TestData);
                    }
                }
                else
                {
                    var testData = this.table.Find(x => x.TestId == testQueue.Item.TestId && !x.IsDeleted).OrderBy(x => x.ExecutionSequence).ToList();
                    var mapper = this.mapperFactory.GetMapper<TblTestData, TblTestDataDto>();
                    var testDataDto = testData.Select(mapper.Map).OrderBy(x => x.ExecutionSequence).ToList();
                    this.ProcessTestQueueExecutableData(testDataDto, testQueue.Item);

                    var mapperExecutableTestData = this.mapperFactory.GetMapper<TblTestDataDto, ExecutableTestData>();
                    this.autoGenArray = new List<AutoGenModel>();
                    List<ExecutableTestData> exeTestData = this.testPlan.Select(mapperExecutableTestData.Map).OrderBy(x => x.ExecutionSequence).ToList();
                    resultMessage.Item = new TestQueue_FullTestData
                    {
                        TestData = exeTestData
                    };

                    if (testQueue.Item.Settings.TakeScreenShotOnUrlChanged > 0)
                    {
                        resultMessage.Item.TakeScreenShot = true;
                        ResultMessage<TblBrowsersDto> browser = this.browserService.GetById(testQueue.Item.Settings.TakeScreenShotOnUrlChanged.Value);
                        resultMessage.Item.TakeScreenShotBrowser = !browser.IsError ? browser.Item : null;
                    }

                    if (resultMessage.Item.TestData.Count > 0)
                    {
                        resultMessage.Item.TestCase = this.mapperFactory.GetMapper<TblTest, TblTestDto>().Map(testData[0].Test);
                        resultMessage.Item.Website = this.mapperFactory.GetMapper<TblWebsite, TblWebsiteDto>().Map(testData[0].Test.Website);
                    }

                    resultMessage.Item.TestData.ForEach(x => x.Value = this.IsAutoGenField(x.Value));

                    resultMessage.Item.UrlToTest = testQueue.Item.Settings.CustomUrlToTest;

                    this.ResolveLocatorText(resultMessage.Item.TestData);
                }

                if (resultMessage.Item != null && resultMessage.Item.Website != null && resultMessage.Item.UrlToTest.IsBlank())
                {
                    var urlData = resultMessage.Item.Website.WebsiteUrlList.FirstOrDefault(x => x.Id == testQueue.Item.Settings.UrlId);
                    if (urlData != null)
                    {
                        resultMessage.Item.UrlToTest = urlData.Url;
                    }
                }
            }

            return resultMessage;
        }

        /// <summary>
        /// Updates the automatic increment.
        /// </summary>
        /// <param name="executableTestData">The executable test data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>incremented value</returns>
        public async Task<ResultMessage<string>> UpdateAutoIncrement(ExecutableTestData executableTestData, long userId)
        {
            var result = new ResultMessage<string>();
            lock (this.thisLock)
            {
                if (executableTestData.SharedTestDataId > 0)
                {
                    ResultMessage<TblLnkTestDataSharedTestDataDto> lnkTestDataSharedTestdata = this.testDataSharedTestDataMapService.GetByTestDataIdAndSharedTestDataId(executableTestData.Id, executableTestData.SharedTestDataId);

                    if (!lnkTestDataSharedTestdata.IsError)
                    {
                        if (lnkTestDataSharedTestdata.Item.NewValue != string.Empty)
                        {
                            result.Item = this.GetAutoIncrementValue(lnkTestDataSharedTestdata.Item.NewValue);
                            lnkTestDataSharedTestdata.Item.NewValue = "#autoincrement#" + result.Item;
                            this.testDataSharedTestDataMapService.SaveOrUpdate(lnkTestDataSharedTestdata.Item, userId);
                        }
                        else
                        {
                            ResultMessage<TblSharedTestDataDto> sharedTestData = this.sharedTestDataService.GetById(executableTestData.SharedTestDataId);
                            if (!sharedTestData.IsError)
                            {
                                result.Item = this.GetAutoIncrementValue(sharedTestData.Item.Value);
                                lnkTestDataSharedTestdata.Item.NewValue = "#autoincrement#" + result.Item;
                                this.testDataSharedTestDataMapService.SaveOrUpdate(lnkTestDataSharedTestdata.Item, userId);
                            }
                        }
                    }
                    else
                    {
                        ResultMessage<TblSharedTestDataDto> sharedTestData = this.sharedTestDataService.GetById(executableTestData.SharedTestDataId);
                        if (!sharedTestData.IsError)
                        {
                            result.Item = this.GetAutoIncrementValue(sharedTestData.Item.Value);
                            TblLnkTestDataSharedTestDataDto testDataSharedTestData = new TblLnkTestDataSharedTestDataDto { NewValue = "#autoincrement#" + result.Item, SharedTestDataId = sharedTestData.Item.Id, TestDataId = executableTestData.Id, ModifiedBy = userId, CreatedBy = userId };
                            this.testDataSharedTestDataMapService.SaveOrUpdate(testDataSharedTestData, userId);
                        }
                    }
                }
                else
                {
                    TblTestData testData = this.table.Find(x => x.Id == executableTestData.Id).FirstOrDefault();
                    result.Item = this.GetAutoIncrementValue(testData.Value);
                    testData.Value = "#autoincrement#" + result.Item;
                    this.table.Update(testData);
                    this.table.Commit();
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the automatic incremented value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>auto incremented value</returns>
        private string GetAutoIncrementValue(string value)
        {
            string[] splittedValue = value.Split('#');
            if (splittedValue[1] == "autoincrement")
            {
                string strValueToIncrement = string.Empty;
                string valueToPrepend = string.Empty;
                char[] splittedString = splittedValue[2].ToCharArray().Reverse().ToArray();
                bool checkForDigit = true;
                foreach (char item in splittedString)
                {
                    if (char.IsDigit(item) && checkForDigit)
                    {
                        strValueToIncrement = item + strValueToIncrement;
                    }
                    else
                    {
                        checkForDigit = false;
                        valueToPrepend = item + valueToPrepend;
                    }
                }

                int incrementedValue = strValueToIncrement.ToInt32() + 1;
                return valueToPrepend + incrementedValue;
            }

            return value;
        }

        /// <summary>
        /// Determines whether [is automatic gen field] [the specified value].
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>string Auto-gen value</returns>
        private string IsAutoGenField(string val)
        {
            if (val.IsBlank())
            {
                return string.Empty;
            }

            string[] splittedValue = val.Split('#');

            long randomStringLength = splittedValue[0].ToInt32(10);

            if (splittedValue.Length > 1)
            {
                var splittedValueLwr = splittedValue[1].ToLower();

                if (splittedValueLwr == "autogen" || splittedValueLwr == "autogennum" || splittedValueLwr == "autogenalpha")
                {
                    var autoGenModel = new AutoGenModel();
                    if (splittedValue.Length < 4)
                    {
                        autoGenModel.AutoGenText = this.GenerateAutoString(string.Empty, splittedValueLwr, randomStringLength);
                    }
                    else
                    {
                        autoGenModel.AutoGenText = this.GenerateAutoString(splittedValue[3], splittedValueLwr, randomStringLength);
                        autoGenModel.Prefix = splittedValue[3];
                    }

                    autoGenModel.PreviousText = splittedValue[2];
                    this.autoGenArray.Add(autoGenModel);

                    return autoGenModel.AutoGenText;
                }

                return val;
            }

            return val;
        }

        /// <summary>
        /// Generates the automatic string.
        /// </summary>
        /// <param name="preFix">The pre fix.</param>
        /// <param name="type">The autogen type.</param>
        /// <param name="length">The lenth of auto gen string generated.</param>
        /// <returns>string Random value</returns>
        private string GenerateAutoString(string preFix, string type, long length)
        {
            char[] charArr;

            if (type.ToLower() == "autogennum")
            {
                charArr = "0123456789".ToCharArray();
            }
            else if (type.ToLower() == "autogenalpha")
            {
                charArr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            }
            else
            {
                charArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            }

            string randomString = string.Empty;

            Random objRandom = new Random();

            for (int i = 0; i < length; i++)
            {
                int x = objRandom.Next(1, charArr.Length);
                if (type.ToLower() == "autogennum")
                {
                    randomString += charArr.GetValue(x);
                }
                else
                {
                    if (!randomString.Contains(charArr.GetValue(x).ToString()))
                    {
                        randomString += charArr.GetValue(x);
                    }
                    else
                    {
                        i--;
                    }
                }
            }

            if (preFix.IsNotBlank())
            {
                randomString = preFix + randomString;
            }

            return randomString;
        }

        /// <summary>
        /// Resolves the locator text.
        /// </summary>
        /// <param name="modelList">The model list.</param>
        private void ResolveLocatorText(IEnumerable<ExecutableTestData> modelList)
        {
            foreach (var item in modelList)
            {
                if (!item.Value.StartsWith("#"))
                {
                    if (item.Value.IndexOf('~') >= 0 && item.LocatorIdentifier.IndexOf('{') >= 0)
                    {
                        var resolvedLocator = this.IsInAutoGenArray(item.Value).ToList();
                        item.Value = resolvedLocator[0];
                        resolvedLocator.RemoveAt(0);
                        item.LocatorIdentifier = string.Format(item.LocatorIdentifier, resolvedLocator.ToArray());
                    }
                    else
                    {
                        item.Value = item.Value.IndexOf('[') == 0 ? item.Value : this.IsInAutoGenArray(item.Value)[0].Trim();
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether [is in automatic gen array] [the specified value].
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>string new value</returns>
        private string[] IsInAutoGenArray(string val)
        {
            foreach (var autoGen in this.autoGenArray.Where(autoGen => val.IndexOf(autoGen.PreviousText, StringComparison.InvariantCultureIgnoreCase) > -1))
            {
                return val.Replace(autoGen.PreviousText, autoGen.AutoGenText).Split('~');
            }

            return val.Split('~');
        }
    }
}
