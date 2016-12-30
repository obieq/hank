// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportLinkDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-12-23</date>
// <summary>
//     The ReportLinkDataService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// ReportLinkDataService class
    /// </summary>
    public class ReportLinkDataService : GlobalService<TblReportExecutionLinkDataDto, TblReportExecutionLinkData>, IReportLinkDataService
    {
        /// <summary>
        /// The test data service
        /// </summary>
        private readonly ITestDataService testDataService;

        /// <summary>
        /// The shared test data service
        /// </summary>
        private readonly ISharedTestDataService sharedTestDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportLinkDataService" /> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="testDataService">The test data service.</param>
        /// <param name="sharedTestDataService">The shared test data service.</param>
        public ReportLinkDataService(
            IMapperFactory mapperFactory,
            IRepository<TblReportExecutionLinkData> table,
            ITestDataService testDataService,
            ISharedTestDataService sharedTestDataService)
            : base(mapperFactory, table)
        {
            this.testDataService = testDataService;
            this.sharedTestDataService = sharedTestDataService;
        }

        /// <summary>
        /// Adds the specified report link data dto.
        /// </summary>
        /// <param name="reportLinkDataDto">The report link data dto.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>added object</returns>
        public ResultMessage<TblReportExecutionLinkDataDto> AddOrUpdate(TblReportExecutionLinkDataDto reportLinkDataDto, long userId)
        {
            ResultMessage<TblReportExecutionLinkDataDto> result = new ResultMessage<TblReportExecutionLinkDataDto>();
            result.Item = this.MapperFactory.GetMapper<TblReportExecutionLinkData, TblReportExecutionLinkDataDto>().Map(this.Table.First(x => x.ReportDataId == reportLinkDataDto.ReportDataId && x.TestId == reportLinkDataDto.TestId));
            if (result.Item == null)
            {
                result = this.SaveOrUpdate(reportLinkDataDto, userId);
            }
            else
            {
                result.Messages.Add(new Message(string.Format("Entry already exist with reportId= {0} and testId= {1}", reportLinkDataDto.ReportDataId, reportLinkDataDto.TestId)));
            }

            return result;
        }

        /// <summary>
        /// Gets the report link data.
        /// </summary>
        /// <param name="dayTillPastByDateCbx">if set to <c>true</c> [day till past by date CBX].</param>
        /// <param name="dayTillPast">The day till past.</param>
        /// <param name="testId">The test identifier.</param>
        /// <param name="dayTillPastDate">The day till past date.</param>
        /// <returns>
        /// Unused report data
        /// </returns>
        public ResultMessage<IEnumerable<TblReportExecutionLinkDataDto>> GetReportLinkData(bool dayTillPastByDateCbx, long dayTillPast, long testId, DateTime dayTillPastDate)
        {
            var result = new ResultMessage<IEnumerable<TblReportExecutionLinkDataDto>>();
            dayTillPastDate = dayTillPastByDateCbx ? dayTillPastDate.Date : DateTime.Now.Subtract(TimeSpan.FromDays(dayTillPast)).Date;
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "DayTillPastDate", dayTillPastDate }, { "TestId", testId } };
            var entities = this.Table.SqlQuery<TblReportExecutionLinkDataDto>("Select * from procgetreportlinkdata(@DayTillPastDate,@TestId);", dictionary).ToList();
            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = entities;
            }

            return result;
        }

        /// <summary>
        /// Gets the report link data by test identifier.
        /// </summary>
        /// <param name="testDataId">The test data identifier.</param>
        /// <param name="isSharedTestData">if set to <c>true</c> [is shared test data].</param>
        /// <returns>TblReportExecutionLinkDataDto list</returns>
        public ResultMessage<TblReportExecutionLinkDataDto> GetReportLinkDataByTestDataId(long testDataId, bool isSharedTestData)
        {
            var result = new ResultMessage<TblReportExecutionLinkDataDto>();
            dynamic testData;
            DateTime dayTillPastDate = DateTime.Now;
            if (!isSharedTestData)
            {
                testData = this.testDataService.GetById(testDataId);
                if (testData.Item.DayTillPast != null)
                {
                    dayTillPastDate = DateTime.Now.Subtract(TimeSpan.FromDays(testData.Item.DayTillPast)).Date;
                }
                else if (testData.Item.DayTillPastByDate != null)
                {
                    dayTillPastDate = testData.Item.DayTillPastByDate;
                }
            }
            else
            {
                testData = this.sharedTestDataService.GetById(testDataId);

                if (testData.Item.DayTillPast != null)
                {
                    dayTillPastDate = DateTime.Now.Subtract(TimeSpan.FromDays(testData.Item.DayTillPast)).Date;
                }
                else if (testData.Item.DayTillPastByDate != null)
                {
                    dayTillPastDate = testData.Item.DayTillPastByDate;
                }
            }

            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "DayTillPastDate", dayTillPastDate }, { "TestId", isSharedTestData ? testData.Item.ReportDataTestId : testData.Item.SharedStepWebsiteTestId } };
            var entities = this.Table.SqlQuery<TblReportExecutionLinkDataDto>("Select * from procgetreportlinkdata(@DayTillPastDate,@TestId);", dictionary).ToList();
            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = entities.FirstOrDefault();
                result.Item.VariableStates = this.GetUniqueVariableStates(entities.FirstOrDefault().Value);
            }

            return result;
        }

        /// <summary>
        /// Gets the unique variable states.
        /// </summary>
        /// <param name="variable">The variable.</param>
        /// <returns>Unique Variable State container list</returns>
        private List<NameValuePair> GetUniqueVariableStates(string variable)
        {
            List<NameValuePair> uniqueVariableStateContainer = new List<NameValuePair>();
            List<NameValuePair> variableStateContainer = JsonConvert.DeserializeObject<List<NameValuePair>>(variable);
            foreach (var item in variableStateContainer)
            {
                NameValuePair variableExistInUniqueList = uniqueVariableStateContainer.Where(x => x.Name == item.Name).FirstOrDefault();
                if (variableExistInUniqueList != null)
                {
                    variableExistInUniqueList.Value = item.Value;
                }
                else
                {
                    uniqueVariableStateContainer.Add(item);
                }
            }

            return uniqueVariableStateContainer;
        }
    }
}
