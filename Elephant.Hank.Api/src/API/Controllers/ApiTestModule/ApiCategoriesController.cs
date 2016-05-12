// ---------------------------------------------------------------------------------------------------
// <copyright file="ApiCategoriesController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The ApiCategoriesController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers.ApiTestModule
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ApiCategoriesController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/api-categories")]
    public class ApiCategoriesController : BaseApiController
    {
        /// <summary>
        /// The api categories service.
        /// </summary>
        private readonly IApiCategoriesService apiCategoriesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCategoriesController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="apiCategoriesService">The API categories service.</param>
        public ApiCategoriesController(ILoggerService loggerService, IApiCategoriesService apiCategoriesService)
            : base(loggerService)
        {
            this.apiCategoriesService = apiCategoriesService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>List of TblApiCategoriesDto objects</returns>
        [HttpGet]
        [Route]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.ApiCategories)]
        public IHttpActionResult GetAll(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblApiCategoriesDto>>();
            try
            {
                result = this.apiCategoriesService.GetByWebsiteId(websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>TblApiCategoriesDto object</returns>
        [Route("{categoryId}")]
        [HttpGet]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.ApiCategories)]
        public IHttpActionResult GetById(long categoryId)
        {
            var result = new ResultMessage<TblApiCategoriesDto>();
            try
            {
                result = this.apiCategoriesService.GetById(categoryId);
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
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// Deleted object
        /// </returns>
        [Route("{categoryId}")]
        [HttpDelete]
        [CustomAuthorize(ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.ApiCategories)]
        public IHttpActionResult DeleteById(long categoryId)
        {
            var result = new ResultMessage<TblApiCategoriesDto>();
            try
            {
                result = this.apiCategoriesService.DeleteById(categoryId, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Adds the specified API categories dto.
        /// </summary>
        /// <param name="apiCategoriesDto">The API categories dto.</param>
        /// <returns>TblApiCategoriesDto object</returns>
        [HttpPost]
        [Route]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.ApiCategories)]
        public IHttpActionResult Add([FromBody]TblApiCategoriesDto apiCategoriesDto)
        {
            var data = this.apiCategoriesService.GetByName(apiCategoriesDto.Name);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Api Categorie already exists with '" + apiCategoriesDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(apiCategoriesDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="apiCategoriesDto">The API categories dto.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{categoryId}")]
        [HttpPut]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.ApiCategories)]
        public IHttpActionResult Update([FromBody]TblApiCategoriesDto apiCategoriesDto, long categoryId)
        {
            var data = this.apiCategoriesService.GetByName(apiCategoriesDto.Name);

            if (!data.IsError && data.Item != null && categoryId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Action already exists with '" + apiCategoriesDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            apiCategoriesDto.Id = categoryId;
            return this.AddUpdate(apiCategoriesDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="apiCategoriesDto">The API categories dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblApiCategoriesDto apiCategoriesDto)
        {
            var result = new ResultMessage<TblApiCategoriesDto>();
            try
            {
                result = this.apiCategoriesService.SaveOrUpdate(apiCategoriesDto, this.UserId);
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