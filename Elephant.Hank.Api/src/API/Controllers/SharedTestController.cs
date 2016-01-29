// ---------------------------------------------------------------------------------------------------
// <copyright file="SharedTestController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharna</author>
// <date>2015-06-05</date>
// <summary>
//     The SharedTestController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SharedTestController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/shared-test")]
    public class SharedTestController : BaseApiController
    {
        /// <summary>
        /// The test service
        /// </summary>
        private readonly ISharedTestService sharedTestService;

        /// <summary>
        /// The test data service
        /// </summary>
        private readonly ISharedTestDataService sharedTestDataService;

        /// <summary>
        /// the test data shared test data service
        /// </summary>
        private readonly ITestDataSharedTestDataMapService testDataSharedTestDataMapService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedTestController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="sharedTestService">The test service.</param>
        /// <param name="sharedTestDataService">The shared test data service.</param>
        /// <param name="testDataSharedTestDataMapService">The test data map service.</param>
        public SharedTestController(ILoggerService loggerService, ISharedTestService sharedTestService, ISharedTestDataService sharedTestDataService, ITestDataSharedTestDataMapService testDataSharedTestDataMapService)
            : base(loggerService)
        {
            this.sharedTestService = sharedTestService;
            this.sharedTestDataService = sharedTestDataService;
            this.testDataSharedTestDataMapService = testDataSharedTestDataMapService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>List of TblTestDto objects</returns>
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult GetAll(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDto>>();
            try
            {
                result = this.sharedTestService.GetByWebSiteId(websiteId);
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
        /// <param name="sharedTestId">The identifier.</param>
        /// <returns>TblTestDto objects</returns>
        [Route("{sharedTestId}")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult GetById(long sharedTestId)
        {
            var result = new ResultMessage<TblSharedTestDto>();
            try
            {
                result = this.sharedTestService.GetById(sharedTestId);
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
        /// <param name="sharedTestId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{sharedTestId}")]
        [HttpDelete]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult DeleteById(long sharedTestId)
        {
            var result = new ResultMessage<TblSharedTestDto>();
            try
            {
                result = this.sharedTestService.DeleteById(sharedTestId, this.UserId);
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
        /// <param name="sharedTestDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult Add([FromBody]TblSharedTestDto sharedTestDto)
        {
            var data = this.sharedTestService.GetByName(sharedTestDto.TestName, sharedTestDto.WebsiteId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + sharedTestDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(sharedTestDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="sharedTestDto">The test dto.</param>
        /// <param name="sharedTestId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{sharedTestId}")]
        [HttpPut]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult Update([FromBody]TblSharedTestDto sharedTestDto, long sharedTestId)
        {
            var data = this.sharedTestService.GetByName(sharedTestDto.TestName, sharedTestDto.WebsiteId);

            if (!data.IsError && data.Item != null && sharedTestId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + sharedTestDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            sharedTestDto.Id = sharedTestId;
            return this.AddUpdate(sharedTestDto);
        }

        /// <summary>
        /// Get the list of variable type test steps
        /// </summary>
        /// <param name="sharedTestId">the shared test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{sharedTestId}/variable-test-steps")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult GetVariableTypeTestDataByTestCase(long sharedTestId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDataDto>>();
            try
            {
                result = this.sharedTestDataService.GetVariableTypeSharedTestDataBySharedTestCase(sharedTestId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #region All private
        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="sharedTestDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblSharedTestDto sharedTestDto)
        {
            var result = new ResultMessage<TblSharedTestDto>();
            try
            {
                result = this.sharedTestService.SaveOrUpdate(sharedTestDto, this.UserId);
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