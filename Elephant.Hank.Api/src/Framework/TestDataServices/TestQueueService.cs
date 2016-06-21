// ---------------------------------------------------------------------------------------------------
// <copyright file="TestQueueService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-06</date>
// <summary>
//     The TestQueueService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;

    using Npgsql;

    /// <summary>
    /// The TestQueueService class
    /// </summary>
    public class TestQueueService : GlobalService<TblTestQueueDto, TblTestQueue>, ITestQueueService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The browser service
        /// </summary>
        private readonly IBrowserService browserService;

        /// <summary>
        /// The scheduler service
        /// </summary>
        private readonly ISchedulerService schedulerService;

        /// <summary>
        /// The table scheduler history
        /// </summary>
        private readonly IRepository<TblSchedulerHistory> tblSchedulerHistory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestQueueService" /> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="browserService">The browser service.</param>
        /// <param name="schedulerService">The scheduler service.</param>
        /// <param name="tblSchedulerHistory">The table scheduler history.</param>
        public TestQueueService(
            IMapperFactory mapperFactory,
            IRepository<TblTestQueue> table,
            IBrowserService browserService,
            ISchedulerService schedulerService, 
            IRepository<TblSchedulerHistory> tblSchedulerHistory)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.browserService = browserService;
            this.schedulerService = schedulerService;
            this.tblSchedulerHistory = tblSchedulerHistory;
        }

        /// <summary>
        /// Updates the name of the test queue status by group.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// Status update
        /// </returns>
        public ResultMessage<bool> UpdateTestQueueStatusByGroupName(long userId, string groupName, ExecutionReportStatus status)
        {
            var sqlQuery = "update \"TblTestQueue\" set \"Status\" = @Status, \"ModifiedOn\" = current_timestamp, \"ModifiedBy\" = @UserId where \"GroupName\" = @groupName";

            if (status == ExecutionReportStatus.Cancelled)
            {
                sqlQuery = "update \"TblTestQueue\" set \"Status\" = @Status, \"ModifiedOn\" = current_timestamp, \"ModifiedBy\" = @UserId where \"GroupName\" = @groupName and \"Status\" in (0, 1)";
            }

            var result = new ResultMessage<bool>
                             {
                                 Item =
                                     this.Table.ExecuteSqlCommand(
                                         sqlQuery,
                                         new NpgsqlParameter("@groupName", groupName),
                                         new NpgsqlParameter("@UserId", userId),
                                         new NpgsqlParameter("@Status", (int)status)) > 0
                             };

            return result;
        }

        /// <summary>
        /// Updates the test queue processing flag.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="isProcessed">if set to <c>true</c> [is processed].</param>
        /// <returns>
        /// Update status
        /// </returns>
        public ResultMessage<bool> UpdateTestQueueProcessingFlag(long userId, string groupName, bool isProcessed)
        {
            var result = new ResultMessage<bool>
            {
                Item =
                    this.Table.ExecuteSqlCommand(
                        "update \"TblTestQueue\" set \"IsProcessed\" = @IsProcessed, \"ModifiedOn\" = current_timestamp, \"ModifiedBy\" = @UserId where \"GroupName\" = @groupName",
                        new NpgsqlParameter("@groupName", groupName),
                        new NpgsqlParameter("@UserId", userId),
                        new NpgsqlParameter("@IsProcessed", isProcessed)) > 0
            };

            return result;
        }

        /// <summary>
        /// Get All unprocessed tests 
        /// </summary>
        /// <returns>List TblTestQueueDto</returns>
        public ResultMessage<IEnumerable<TblTestQueueDto>> GetAllUnProcessed()
        {
            var result = new ResultMessage<IEnumerable<TblTestQueueDto>>();

            var baseRecord = this.Table.Find(x => x.Status == 0 && x.IsDeleted != true).OrderBy(x => x.Id).FirstOrDefault();

            if (baseRecord != null)
            {
                var entities = this.Table.Find(x => x.Status == 0 && x.GroupName == baseRecord.GroupName && x.IsDeleted != true).ToList();

                if (!entities.Any())
                {
                    result.Messages.Add(new Message(null, "Record not found!"));
                }
                else
                {
                    var mapper = this.mapperFactory.GetMapper<TblTestQueue, TblTestQueueDto>();
                    var items = new List<TblTestQueueDto>();

                    var browsers = this.browserService.GetAll();

                    foreach (var testQueue in entities)
                    {
                        var item = mapper.Map(testQueue);
                        items.Add(item);

                        this.ProcessForScheduler(testQueue, item, browsers.Item);
                        this.ProcessForTestQueue(testQueue, item, browsers.Item);

                        this.ProcessForOtherWise(testQueue, item, browsers.Item);
                    }

                    result.Item = items;
                }
            }
            else
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }

            return result;
        }

        /// <summary>
        /// Processes for scheduler.
        /// </summary>
        /// <param name="testQueueSrc">The test queue source.</param>
        /// <param name="testQueueDest">The test queue dest.</param>
        /// <param name="browsers">The browsers.</param>
        private void ProcessForScheduler(TblTestQueue testQueueSrc, TblTestQueueDto testQueueDest, IEnumerable<TblBrowsersDto> browsers)
        {
            if (browsers != null && testQueueSrc.SchedulerId.HasValue && (testQueueDest.Browsers == null || !testQueueDest.Browsers.Any()))
            {
                ResultMessage<TblSchedulerDto> schedulerDto = this.schedulerService.GetById(testQueueSrc.SchedulerId.Value);
                var schedulerHistory = this.tblSchedulerHistory.Find(x => x.GroupName == testQueueSrc.GroupName).FirstOrDefault();

                testQueueDest.Settings = new TestQueueSettings
                                             {
                                                 SeleniumAddress = schedulerDto.Item.Settings.SeleniumAddress,
                                                 Browsers = schedulerDto.Item.Settings.Browsers,
                                                 IsCancelled = schedulerHistory != null && schedulerHistory.IsCancelled
                                             };
                if (schedulerDto.Item.Settings.Browsers != null)
                {
                    testQueueDest.Browsers = browsers.Where(x => schedulerDto.Item.Settings.Browsers.Contains(x.Id)).ToList();
                }
            }
        }

        /// <summary>
        /// Processes for ProcessForTestQueue.
        /// </summary>
        /// <param name="testQueueSrc">The test queue source.</param>
        /// <param name="testQueueDest">The test queue dest.</param>
        /// <param name="browsers">The browsers.</param>
        private void ProcessForTestQueue(TblTestQueue testQueueSrc, TblTestQueueDto testQueueDest, IEnumerable<TblBrowsersDto> browsers)
        {
            if (browsers != null && (testQueueDest.Browsers == null || !testQueueDest.Browsers.Any()))
            {
                if (testQueueSrc.Settings != null)
                {
                    testQueueDest.Settings.SeleniumAddress = testQueueSrc.Settings.SeleniumAddress;
                    testQueueDest.Settings.Browsers = testQueueSrc.Settings.Browsers;
                    if (testQueueSrc.Settings.Browsers != null)
                    {
                        testQueueDest.Browsers = browsers.Where(x => testQueueSrc.Settings.Browsers.Contains(x.Id)).ToList();
                    }
                }
            }
        }

        /// <summary>
        /// Processes for other wise.
        /// </summary>
        /// <param name="testQueueSrc">The test queue source.</param>
        /// <param name="testQueueDest">The test queue dest.</param>
        /// <param name="browsers">The browsers.</param>
        private void ProcessForOtherWise(TblTestQueue testQueueSrc, TblTestQueueDto testQueueDest, IEnumerable<TblBrowsersDto> browsers)
        {
            if (browsers != null && (testQueueDest.Browsers == null || !testQueueDest.Browsers.Any()))
            {
                if (testQueueSrc.Settings != null)
                {
                    testQueueDest.Settings.SeleniumAddress = testQueueSrc.TestCase.Website.Settings.SeleniumAddress;
                    testQueueDest.Settings.Browsers = testQueueSrc.TestCase.Website.Settings.Browsers;
                    if (testQueueDest.Settings.Browsers != null)
                    {
                        testQueueDest.Browsers = browsers.Where(x => testQueueSrc.TestCase.Website.Settings.Browsers.Contains(x.Id)).ToList();
                    }
                }
            }
        }
    }
}