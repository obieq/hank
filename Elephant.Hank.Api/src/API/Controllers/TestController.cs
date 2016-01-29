// ---------------------------------------------------------------------------------------------------
// <copyright file="TestController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TestController class
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
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TestController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/test-cat/{testCategoryId}/test")]
    public class TestController : BaseApiController
    {
        /// <summary>
        /// The test service
        /// </summary>
        private readonly ITestService testService;

        /// <summary>
        /// The test data service
        /// </summary>
        private readonly ITestDataService testDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="testService">The test service.</param>
        /// <param name="testDataService">The test data service.</param>
        public TestController(ILoggerService loggerService, ITestService testService, ITestDataService testDataService)
            : base(loggerService)
        {
            this.testService = testService;
            this.testDataService = testDataService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <param name="testCategoryId">The test categorytestCategoryId identifier.</param>
        /// <returns>List of TblTestDto objects</returns>
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.TestScripts)]
        public IHttpActionResult GetAll(long websiteId, long testCategoryId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDto>>();
            try
            {
                result = testCategoryId == 0
                     ? this.testService.GetByWebSiteId(websiteId)
                     : this.testService.GetByCategory(testCategoryId);
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
        /// <param name="testId">The identifier.</param>
        /// <returns>TblTestDto objects</returns>
        [Route("{testId}")]
        public IHttpActionResult GetById(long testId)
        {
            var result = new ResultMessage<TblTestDto>();
            try
            {
                result = this.testService.GetById(testId);
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
        /// <param name="testId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{testId}")]
        [HttpDelete]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.TestScripts)]
        public IHttpActionResult DeleteById(long testId)
        {
            var result = new ResultMessage<TblTestDto>();
            try
            {
                result = this.testService.DeleteById(testId, this.UserId);
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
        /// <param name="testDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.TestScripts)]
        public IHttpActionResult Add([FromBody]TblTestDto testDto)
        {
            var data = this.testService.GetByName(testDto.TestName, testDto.WebsiteId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + testDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(testDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="testDto">The test dto.</param>
        /// <param name="testId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{testId}")]
        [HttpPut]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.TestScripts)]
        public IHttpActionResult Update([FromBody]TblTestDto testDto, long testId)
        {
            var data = this.testService.GetByName(testDto.TestName, testDto.WebsiteId);

            if (!data.IsError && data.Item != null && testId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + testDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            testDto.Id = testId;
            return this.AddUpdate(testDto);
        }

        /// <summary>
        /// Get the list of variable type test steps
        /// </summary>
        /// <param name="testId">the test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{testId}/variable-test-steps")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.TestScripts)]
        public IHttpActionResult GetVariableTypeTestDataByTestCase(long testId)
        {
            var result = new ResultMessage<IEnumerable<ProtractorVariableModel>>();
            try
            {
                result = this.testDataService.GetVariableTypeTestDataByTestCase(testId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get the list of variable type test steps
        /// </summary>
        /// <param name="testId">the test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{testId}/variable-for-autocomplete")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.TestScripts)]
        public IHttpActionResult GetAllVariableByTestIdForAutoComplete(long testId)
        {
            var result = new ResultMessage<IEnumerable<string>>();
            try
            {
                result = this.testDataService.GetAllVariableByTestIdForAutoComplete(testId);
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
        /// <param name="testDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblTestDto testDto)
        {
            var result = new ResultMessage<TblTestDto>();
            try
            {
                result = this.testService.SaveOrUpdate(testDto, this.UserId);
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