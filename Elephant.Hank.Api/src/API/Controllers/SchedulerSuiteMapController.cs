// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerSuiteMapController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-20</date>
// <summary>
//     The SchedulerSuiteMapController class
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
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Enum;    
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SchedulerSuiteMapController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/scheduler-suite")]
    public class SchedulerSuiteMapController : BaseApiController
    {
        /// <summary>
        /// The locator service
        /// </summary>
        private readonly ISchedulerSuiteMapService schedulerSuiteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerSuiteMapController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="schedulerSuiteService">The schedulerSuite Service</param>
        public SchedulerSuiteMapController(ILoggerService loggerService, ISchedulerSuiteMapService schedulerSuiteService)
            : base(loggerService)
        {
            this.schedulerSuiteService = schedulerSuiteService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblSchedulerDto objects</returns>
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>>();
            try
            {
                result = this.schedulerSuiteService.GetAll();
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
        /// <param name="schedulerSuiteId">The identifier.</param>
        /// <returns>TblSchedulerDto objects</returns>
        [Route("{schedulerSuiteId}")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult GetById(long schedulerSuiteId)
        {
            var result = new ResultMessage<TblLnkSchedulerSuiteDto>();
            try
            {
                result = this.schedulerSuiteService.GetById(schedulerSuiteId);
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
        /// <param name="schedulerSuiteId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{schedulerSuiteId}")]
        [HttpDelete]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult DeleteById(long schedulerSuiteId)
        {
            var result = new ResultMessage<TblLnkSchedulerSuiteDto>();
            try
            {
                result = this.schedulerSuiteService.DeleteById(schedulerSuiteId, this.UserId);
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
        /// <param name="schedulerSuiteDto">The scheduler dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult Add([FromBody]TblLnkSchedulerSuiteDto schedulerSuiteDto)
        {
            return this.AddUpdate(schedulerSuiteDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="schedulerSuiteDto">The locator dto.</param>
        /// <param name="schedulerSuiteId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{schedulerSuiteId}")]
        [HttpPut]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.Scheduler)]
        public IHttpActionResult Update([FromBody]TblLnkSchedulerSuiteDto schedulerSuiteDto, long schedulerSuiteId)
        {
            schedulerSuiteDto.Id = schedulerSuiteId;
            return this.AddUpdate(schedulerSuiteDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="schedulerSuiteDto">The scheduler dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblLnkSchedulerSuiteDto schedulerSuiteDto)
        {
            var result = new ResultMessage<TblLnkSchedulerSuiteDto>();
            try
            {
                result = this.schedulerSuiteService.SaveOrUpdate(schedulerSuiteDto, this.UserId);
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