// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportsController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-25</date>
// <summary>
//     The ReportsController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Elephant.Hank.Resources.ViewModal;

    using Newtonsoft.Json;

    /// <summary>
    /// The ReportsController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/report")]
    public class ReportsController : BaseApiController
    {
        /// <summary>
        /// The report data service
        /// </summary>
        private readonly IReportDataService reportDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportsController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="reportDataService">The report data service.</param>
        public ReportsController(ILoggerService loggerService, IReportDataService reportDataService)
            : base(loggerService)
        {
            this.reportDataService = reportDataService;
        }

        /// <summary>
        /// Returns the protractor report
        /// </summary>
        /// <param name="searchReportObject">the searchReportObject</param>
        /// <returns>ReportData object</returns>
        [Route("SearchReport")]
        [HttpPost]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Reports)]
        public IHttpActionResult SearchReport(SearchReportObject searchReportObject)
        {
            var result = new ResultMessage<IEnumerable<TblReportDataDto>>();
            try
            {
                result = this.reportDataService.GetReportData(searchReportObject);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="reportId">The identifier.</param>
        /// <returns>TblReportDataDto objects</returns>
        [Route("{reportId}")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Reports)]
        public IHttpActionResult GetById(long reportId)
        {
            var result = new ResultMessage<TblReportDataDto>();
            try
            {
                result = this.reportDataService.GetReportDataById(reportId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets by Group Name.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>
        /// TblReportDataDto objects
        /// </returns>
        [Route("execution-group/{groupName}")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Reports)]
        public IHttpActionResult GetByGroupName(string groupName)
        {
            var result = new ResultMessage<IEnumerable<TblReportDataDto>>();
            try
            {
                result = this.reportDataService.GetByGroupName(groupName);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets by Group Name.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>
        /// TblReportDataDto objects
        /// </returns>
        [Route("execution-group/{groupName}/screen-shot-array")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Reports)]
        public IHttpActionResult GetByGroupNameWhereScreenShotArrayExist(string groupName)
        {
            var result = new ResultMessage<TblReportDataDto>();
            try
            {
                result = this.reportDataService.GetByGroupNameWhereScreenShotArrayExist(groupName);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message)); 
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the group status data.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>GroupStatusReport object</returns>
        [Route("execution-group/{groupName}/status")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Reports)]
        public IHttpActionResult GetExecutionGroupStatusData(string groupName)
        {
            var result = new ResultMessage<GroupStatusReport>();
            try
            {
                result = this.reportDataService.GetExecutionGroupStatus(groupName);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="reportJson">The report json.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [Route]
        [HttpPost]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.Reports)]
        public IHttpActionResult Add([FromBody] ReportJson reportJson)
        {
            var result = new ResultMessage<TblReportDataDto>();
            try
            {
                var reportDataDto = new TblReportDataDto
                                    {
                                        TestQueueId = reportJson.TestQueueId,
                                        ExecutionGroup = reportJson.ExecutionGroup,
                                        Value = JsonConvert.SerializeObject(reportJson)
                                    };

                result = this.reportDataService.SaveOrUpdate(reportDataDto, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// get all unprocessed data item for group
        /// </summary>
        /// <param name="groupName">group identifier</param>
        /// <returns>list of report data</returns>
        [Route("get-all-unprocessed-for-group/{groupName}")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Reports)]
        public IHttpActionResult GetAllUnprocessedForGroup(string groupName)
        {
            var result = new ResultMessage<IEnumerable<TblReportDataDto>>();
            try
            {
                result = this.reportDataService.GetAllUnprocessedForGroup(groupName);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }
    }
}