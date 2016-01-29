// ---------------------------------------------------------------------------------------------------
// <copyright file="SuiteTestMapController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The SuiteTestMapController class
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
    /// The SuiteTestMapController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/suite-test-map")]
    public class SuiteTestMapController : BaseApiController
    {
        /// <summary>
        /// The suite test map service
        /// </summary>
        private readonly ISuiteTestMapService suiteTestMapService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuiteTestMapController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="suiteTestMapService">The suite test map service.</param>
        public SuiteTestMapController(ILoggerService loggerService, ISuiteTestMapService suiteTestMapService)
            : base(loggerService)
        {
            this.suiteTestMapService = suiteTestMapService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblLnkSuiteTestDto objects</returns>
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblLnkSuiteTestDto>>();
            try
            {
                result = this.suiteTestMapService.GetAll();
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
        /// <returns>TblLnkSuiteTestDto objects</returns>
        [Route("{id}")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblLnkSuiteTestDto>();
            try
            {
                result = this.suiteTestMapService.GetById(id);
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
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult DeleteById(long id)
        {
            var result = new ResultMessage<TblLnkSuiteTestDto>();
            try
            {
                result = this.suiteTestMapService.DeleteById(id, this.UserId);
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
        /// <param name="suiteTestDto">The suite test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPut]
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult Add([FromBody]TblLnkSuiteTestDto suiteTestDto)
        {
            return this.AddUpdate(suiteTestDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="suiteTestDto">The suite test dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.Suites)]
        public IHttpActionResult Update([FromBody]TblLnkSuiteTestDto suiteTestDto, long id)
        {
            suiteTestDto.Id = id;
            return this.AddUpdate(suiteTestDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="suiteTestDto">The suite test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblLnkSuiteTestDto suiteTestDto)
        {
            var result = new ResultMessage<TblLnkSuiteTestDto>();
            try
            {
                result = this.suiteTestMapService.SaveOrUpdate(suiteTestDto, this.UserId);
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