// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The SchedulerController class
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
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The LocatorController class
    /// </summary>
    [RoutePrefix("api/scheduler")]
    [Authorize]
    public class SchedulerController : BaseApiController
    {
        /// <summary>
        /// The locator service
        /// </summary>
        private readonly ISchedulerService schedulerService;

        /// <summary>
        /// The scheduler suite service
        /// </summary>
        private readonly ISchedulerSuiteMapService schedulerSuiteMapService;

        /// <summary>
        /// The scheduler history service
        /// </summary>
        private readonly ISchedulerHistoryService schedulerHistoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="schedulerService">The scheduler service.</param>
        /// <param name="schedulerSuiteMapService">The schedulerSuite service.</param>
        /// <param name="schedulerHistoryService">The scheduler history service.</param>
        public SchedulerController(ILoggerService loggerService, ISchedulerService schedulerService, ISchedulerSuiteMapService schedulerSuiteMapService, ISchedulerHistoryService schedulerHistoryService)
            : base(loggerService)
        {
            this.schedulerService = schedulerService;
            this.schedulerSuiteMapService = schedulerSuiteMapService;
            this.schedulerHistoryService = schedulerHistoryService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblSchedulerDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerDto>>();
            try
            {
                result = this.schedulerService.GetAll();
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
        /// <param name="id">The identifier.</param>
        /// <returns>TblSchedulerDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblSchedulerDto>();
            try
            {
                result = this.schedulerService.GetById(id);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long id)
        {
            var result = new ResultMessage<TblSchedulerDto>();
            try
            {
                result = this.schedulerService.DeleteById(id, this.UserId);
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
        /// <param name="schedulerDto">The scheduler dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblSchedulerDto schedulerDto)
        {
            return this.AddUpdate(schedulerDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="schedulerDto">The locator dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblSchedulerDto schedulerDto, long id)
        {
            schedulerDto.Id = id;
            schedulerDto.LastExecuted = null;
            return this.AddUpdate(schedulerDto);
        }

        #region Scheduler - Child History

        /// <summary>
        /// Get Scheduler Suites
        /// </summary>
        /// <param name="schedulerId">the scheduler identifier</param>
        /// <returns>TblSchedulerSuiteDto objects</returns>
        [Route("{schedulerId}/scheduler-history")]
        public IHttpActionResult GetSchedulerHistoryById(long schedulerId)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerHistoryDto>>();
            try
            {
                result = this.schedulerHistoryService.GetBySchedulerId(schedulerId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// sets the force execute flag to true
        /// </summary>
        /// <param name="schedulerId">the scheduler identifier</param>
        /// <returns>TblSchedulerSuiteDto objects</returns>
        [HttpPost]
        [Route("{schedulerId}/force-execute")]
        public IHttpActionResult ForceExecute(long schedulerId)
        {
            var result = new ResultMessage<TblSchedulerDto>();
            try
            {
                result = this.schedulerService.ForceExecute(this.UserId, schedulerId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region Scheduler - Mapping

        /// <summary>
        /// Adds the multiple suite links.
        /// </summary>
        /// <param name="schedulerSuiteDto">The suite test dto.</param>
        /// <param name="schedulerId">The scheduler identifier.</param>
        /// <returns>
        /// TblLnkSuiteTestDto objects
        /// </returns>
        [Route("{schedulerId}/scheduler-suite-map")]
        [HttpPost]
        public IHttpActionResult AddMultipleSuiteLinks([FromBody]List<TblLnkSchedulerSuiteDto> schedulerSuiteDto, long schedulerId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>>();
            try
            {
                result = this.schedulerSuiteMapService.SaveOrUpdate(this.UserId, schedulerId, schedulerSuiteDto);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get Scheduler Suites
        /// </summary>
        /// <param name="schedulerId">the scheduler identifier</param>
        /// <returns>TblSchedulerSuiteDto objects</returns>
        [Route("{schedulerId}/scheduler-suite-map")]
        public IHttpActionResult GetSchedulerSuite(long schedulerId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>>();
            try
            {
                result = this.schedulerSuiteMapService.GetBySchedulerId(schedulerId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region Scheduler - All Private

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="schedulerDto">The scheduler dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblSchedulerDto schedulerDto)
        {
            var result = new ResultMessage<TblSchedulerDto>();
            try
            {
                result = this.schedulerService.SaveOrUpdate(schedulerDto, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion
    }
}