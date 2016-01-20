// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerHistoryController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-29</date>
// <summary>
//     The SchedulerHistoryController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SchedulerHistoryController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/scheduler/{schedulerId}/scheduler-history")]
    public class SchedulerHistoryController : BaseApiController
    {
        /// <summary>
        /// The scheduler history service
        /// </summary>
        private readonly ISchedulerHistoryService schedulerHistoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerHistoryController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="schedulerHistoryService">The scheduler history service.</param>
        public SchedulerHistoryController(ILoggerService loggerService, ISchedulerHistoryService schedulerHistoryService)
            : base(loggerService)
        {
            this.schedulerHistoryService = schedulerHistoryService;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// TblSchedulerHistoryDto objects
        /// </returns>
        [Route("status/{groupName}/{status}")]
        [HttpPost]
        public IHttpActionResult UpdateStatusByGroupName(string groupName, SchedulerExecutionStatus status)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerHistoryDto>>();
            try
            {
                result = this.schedulerHistoryService.UpdateStatusByGroupName(this.UserId, groupName, status, null);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Updates the name of the status by group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="status">The status.</param>
        /// <returns>TblSchedulerHistoryDto objects</returns>
        [Route("status/{groupName}/email/{status}")]
        [HttpPost]
        public IHttpActionResult UpdateEmailStatusByGroupName(string groupName, SchedulerHistoryEmailStatus status)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerHistoryDto>>();
            try
            {
                result = this.schedulerHistoryService.UpdateStatusByGroupName(this.UserId, groupName, null, status);
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