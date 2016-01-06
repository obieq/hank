// ---------------------------------------------------------------------------------------------------
// <copyright file="WebsiteController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The WebsiteController class
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
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The WebsiteController class
    /// </summary>
    [RoutePrefix("api/website")]
    [Authorize]
    public class WebsiteController : BaseApiController
    {
        /// <summary>
        /// The website service
        /// </summary>
        private readonly IWebsiteService websiteService;

        /// <summary>
        /// The display name service
        /// </summary>
        private readonly IPagesService displayNameService;

        /// <summary>
        /// The test service
        /// </summary>
        private readonly ITestService testService;

        /// <summary>
        /// The suite service
        /// </summary>
        private readonly ISuiteService suiteService;

        /// <summary>
        /// The suite service
        /// </summary>
        private readonly ISharedTestService sharedTestService;

        /// <summary>
        /// The suite service
        /// </summary>
        private readonly ISchedulerService schedulerService;

        /// <summary>
        /// The dbCategory service
        /// </summary>
        private readonly IDataBaseCategoriesService dataBaseCategoriesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebsiteController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="websiteService">The website service.</param>
        /// <param name="displayNameService">The display name service.</param>
        /// <param name="testService">The test service.</param>
        /// <param name="suiteService">The suite service.</param>
        /// <param name="schedulerService">The Scheduler service.</param>
        /// <param name="sharedTestService">The shared test service.</param>
        /// <param name="dataBaseCategoriesService">The data base Categories service.</param>
        public WebsiteController(
            ILoggerService loggerService,
            IWebsiteService websiteService,
            IPagesService displayNameService,
            ITestService testService,
            ISuiteService suiteService,
            ISchedulerService schedulerService,
            ISharedTestService sharedTestService,
            IDataBaseCategoriesService dataBaseCategoriesService)
            : base(loggerService)
        {
            this.websiteService = websiteService;
            this.displayNameService = displayNameService;
            this.testService = testService;
            this.suiteService = suiteService;
            this.schedulerService = schedulerService;
            this.sharedTestService = sharedTestService;
            this.dataBaseCategoriesService = dataBaseCategoriesService;
        }

        #region WebSite

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblActionDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblWebsiteDto>>();
            try
            {
                result = this.websiteService.GetAll();
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
        /// <returns>TblActionDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblWebsiteDto>();
            try
            {
                result = this.websiteService.GetById(id);
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
            var result = new ResultMessage<TblWebsiteDto>();
            try
            {
                result = this.websiteService.DeleteById(id, this.UserId);
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
        /// <param name="websiteDto">The website dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblWebsiteDto websiteDto)
        {
            var data = this.websiteService.GetByName(websiteDto.Name);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + websiteDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(websiteDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="websiteDto">The website dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblWebsiteDto websiteDto, long id)
        {
            var data = this.websiteService.GetByName(websiteDto.Name);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + websiteDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            websiteDto.Id = id;

            return this.AddUpdate(websiteDto);
        }

        #endregion

        #region WebSite Child - Pages

        /// <summary>
        /// Gets the pages.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// List of TblActionDto objects
        /// </returns>
        [Route("{websiteId}/pages")]
        public IHttpActionResult GetPages(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblPagesDto>>();
            try
            {
                result = this.displayNameService.GetByWebSiteId(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region WebSite Child - TestCases

        /// <summary>
        /// Gets the test case.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>TblTestDto object</returns>
        [Route("{websiteId}/test-case")]
        public IHttpActionResult GetTestCase(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDto>>();
            try
            {
                result = this.testService.GetByWebSiteId(websiteId);
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
        /// <param name="websiteId">the test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{websiteId}/variable-for-autocomplete")]
        public IHttpActionResult GetAllVariableByTestIdForAutoComplete(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<string>>();
            try
            {
                result = this.websiteService.GetAllVariableByWebsiteIdForAutoComplete(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region WebSite Child - Suite

        /// <summary>
        /// Gets the test case.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>TblTestDto object</returns>
        [Route("{websiteId}/suite")]
        public IHttpActionResult GetSuites(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblSuiteDto>>();
            try
            {
                result = this.suiteService.GetByWebsiteId(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region Website Child - Scheduler

        /// <summary>
        /// Get the schedulers list by websiteId
        /// </summary>
        /// <param name="websiteId">website identifier</param>
        /// <returns>List object of TblSchedulerDto</returns>
        [Route("{websiteId}/scheduler")]
        public IHttpActionResult GetScheduler(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerDto>>();
            try
            {
                result = this.schedulerService.GetByWebsiteId(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region WebSite Child - SharedTestCases

        /// <summary>
        /// Gets the test case.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>TblTestDto object</returns>
        [Route("{websiteId}/shared-test-case")]
        public IHttpActionResult GetSharedTestCase(long websiteId)
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

        #endregion

        #region Website Child - DataBaseCategories

        /// <summary>
        /// Get DBCategories by websiteid
        /// </summary>
        /// <param name="websiteId">website identifier</param>
        /// <returns>
        /// TblDBCategoriesDto object list
        /// </returns>
        [Route("{websiteId}/data-base-categories")]
        public IHttpActionResult GetDBCategoriesByWebsiteId(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseCategoriesDto>>();
            try
            {
                result = this.dataBaseCategoriesService.GetByWebsiteId(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region All private

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="websiteDto">The website dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblWebsiteDto websiteDto)
        {
            var result = new ResultMessage<TblWebsiteDto>();
            try
            {
                result = this.websiteService.SaveOrUpdate(websiteDto, this.UserId);
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