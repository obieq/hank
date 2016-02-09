// ---------------------------------------------------------------------------------------------------
// <copyright file="TestCategoryController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-01</date>
// <summary>
//     The TestCategoryController class
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
    /// The TestCategoryController class
    /// </summary>
    [RoutePrefix(RouteConstants.TestCatRoot)]
    [CustomAuthorize(ModuleType = FrameworkModules.TestScripts, ActionType = ActionTypes.Read)]
    public class TestCategoryController : BaseApiController
    {
        /// <summary>
        /// The test category service
        /// </summary>
        private readonly ITestCategoryService testCategoryService;

        /// <summary>
        /// The test service
        /// </summary>
        private readonly ITestService testService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCategoryController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="testCategoryService">The test category service.</param>
        /// <param name="testService">The test service.</param>
        public TestCategoryController(ILoggerService loggerService, ITestCategoryService testCategoryService, ITestService testService)
            : base(loggerService)
        {
            this.testCategoryService = testCategoryService;
            this.testService = testService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="websiteId">The web site identifier.</param>
        /// <returns>
        /// List of TblTestCategoriesDto objects
        /// </returns>
        [Route]
        public IHttpActionResult GetAll(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblTestCategoriesDto>>();
            try
            {
                result = this.testCategoryService.GetByWebSiteId(websiteId);
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
        /// <param name="websiteId">The web site identifier.</param>
        /// <param name="testCategoryId">The identifier.</param>
        /// <returns>
        /// TblTestCategoriesDto objects
        /// </returns>
        [Route("{testCategoryId}")]
        public IHttpActionResult GetById(long websiteId, long testCategoryId)
        {
            var result = new ResultMessage<TblTestCategoriesDto>();
            try
            {
                result = this.testCategoryService.GetById(testCategoryId);
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
        /// <param name="websiteId">The web site identifier.</param>
        /// <param name="testCategoryId">The identifier.</param>
        /// <returns>
        /// Deleted object
        /// </returns>
        [Route("{testCategoryId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long websiteId, long testCategoryId)
        {
            var result = new ResultMessage<TblTestCategoriesDto>();
            try
            {
                result = this.testCategoryService.DeleteById(testCategoryId, this.UserId);
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
        /// <param name="websiteId">The web site identifier.</param>
        /// <param name="testCatDto">The display name dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(long websiteId, [FromBody]TblTestCategoriesDto testCatDto)
        {
            testCatDto.WebsiteId = websiteId;

            var data = this.testCategoryService.GetByName(testCatDto.Name, testCatDto.WebsiteId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Category already exists with '" + testCatDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(testCatDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="websiteId">The web site identifier.</param>
        /// <param name="testCatDto">The display name dto.</param>
        /// <param name="testCategoryId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{testCategoryId}")]
        [HttpPut]
        public IHttpActionResult Update(long websiteId, [FromBody]TblTestCategoriesDto testCatDto, long testCategoryId)
        {
            testCatDto.WebsiteId = websiteId;

            var data = this.testCategoryService.GetByName(testCatDto.Name, testCatDto.WebsiteId);

            if (!data.IsError && data.Item != null && testCategoryId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Category already exists with '" + testCatDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            testCatDto.Id = testCategoryId;
            return this.AddUpdate(testCatDto);
        }

        #region Test category child - Test cases

        /// <summary>
        /// Gets the test scripts by category identifier.
        /// </summary>
        /// <param name="websiteId">The web site identifier.</param>
        /// <param name="testCategoryId">The category identifier.</param>
        /// <returns>
        /// List of TblTestDto objects
        /// </returns>
        [Route("{testCategoryId}/test-scripts")]
        public IHttpActionResult GetTestScriptsByCatId(long websiteId, long testCategoryId)
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

        #endregion

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="testCatDto">The display name dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblTestCategoriesDto testCatDto)
        {
            var result = new ResultMessage<TblTestCategoriesDto>();
            try
            {
                result = this.testCategoryService.SaveOrUpdate(testCatDto, this.UserId);
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