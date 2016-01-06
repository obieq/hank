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

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The TestCategoryController class
    /// </summary>
    [RoutePrefix(RouteConstants.TestCatRoot)]
    [Authorize]
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
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>
        /// List of TblTestCategoriesDto objects
        /// </returns>
        [Route]
        public IHttpActionResult GetAll(long webSiteId)
        {
            var result = new ResultMessage<IEnumerable<TblTestCategoriesDto>>();
            try
            {
                result = this.testCategoryService.GetByWebSiteId(webSiteId);
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
        /// <param name="webSiteId">The web site identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// TblTestCategoriesDto objects
        /// </returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long webSiteId, long id)
        {
            var result = new ResultMessage<TblTestCategoriesDto>();
            try
            {
                result = this.testCategoryService.GetById(id);
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
        /// <param name="webSiteId">The web site identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Deleted object
        /// </returns>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long webSiteId, long id)
        {
            var result = new ResultMessage<TblTestCategoriesDto>();
            try
            {
                result = this.testCategoryService.DeleteById(id, this.UserId);
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
        /// <param name="webSiteId">The web site identifier.</param>
        /// <param name="testCatDto">The display name dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [Route]
        [HttpPost]
        public IHttpActionResult Add(long webSiteId, [FromBody]TblTestCategoriesDto testCatDto)
        {
            testCatDto.WebsiteId = webSiteId;

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
        /// <param name="webSiteId">The web site identifier.</param>
        /// <param name="testCatDto">The display name dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(long webSiteId, [FromBody]TblTestCategoriesDto testCatDto, long id)
        {
            testCatDto.WebsiteId = webSiteId;

            var data = this.testCategoryService.GetByName(testCatDto.Name, testCatDto.WebsiteId);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Category already exists with '" + testCatDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            testCatDto.Id = id;
            return this.AddUpdate(testCatDto);
        }

        #region Test category child - Test cases

        /// <summary>
        /// Gets the test scripts by category identifier.
        /// </summary>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <param name="catId">The category identifier.</param>
        /// <returns>
        /// List of TblTestDto objects
        /// </returns>
        [Route("{catId}/test-scripts")]
        public IHttpActionResult GetTestScriptsByCatId(long webSiteId, long catId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDto>>();
            try
            {
                result = catId == 0 
                    ? this.testService.GetByWebSiteId(webSiteId)
                    : this.testService.GetByCategory(catId);
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