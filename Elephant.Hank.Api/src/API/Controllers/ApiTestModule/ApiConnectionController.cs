// ---------------------------------------------------------------------------------------------------
// <copyright file="ApiConnectionController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The ApiConnectionController class
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
    /// The ApiConnectionController class
    /// </summary>
    /// <seealso cref="Elephant.Hank.Api.Controllers.BaseApiController" />
    [RoutePrefix("api/website/{websiteId}/api-categories/{categoryId}/api-connection")]
    public class ApiConnectionController : BaseApiController
    {
        /// <summary>
        /// The API connection service
        /// </summary>
        private readonly IApiConnectionService apiConnectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnectionController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="apiConnectionService">The API connection service.</param>
        public ApiConnectionController(ILoggerService loggerService, IApiConnectionService apiConnectionService)
            : base(loggerService)
        {
            this.apiConnectionService = apiConnectionService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>
        /// TblApiConnectionDto list object
        /// </returns>
        [Route]
        [HttpGet]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.ApiConnection)]
        public IHttpActionResult GetAll(long categoryId)
        {
            var result = new ResultMessage<IEnumerable<TblApiConnectionDto>>();
            try
            {
                result = this.apiConnectionService.GetByCategoryId(categoryId);
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
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns>
        /// TblApiConnectionDto objects
        /// </returns>
        [HttpGet]
        [Route("{connectionId}")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.ApiConnection)]
        public IHttpActionResult GetById(long connectionId)
        {
            var result = new ResultMessage<TblApiConnectionDto>();
            try
            {
                result = this.apiConnectionService.GetById(connectionId);
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
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns>
        /// Deleted object
        /// </returns>
        [Route("{connectionId}")]
        [HttpDelete]
        [CustomAuthorize(ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.ApiConnection)]
        public IHttpActionResult DeleteById(long connectionId)
        {
            var result = new ResultMessage<TblApiConnectionDto>();
            try
            {
                result = this.apiConnectionService.DeleteById(connectionId, this.UserId);
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
        /// <param name="apiConnectionDto">The API connection dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.ApiConnection)]
        public IHttpActionResult Add([FromBody]TblApiConnectionDto apiConnectionDto)
        {
            var data = this.apiConnectionService.GetByEnvironmentAndCategoryId(apiConnectionDto.EnvironmentId, apiConnectionDto.CategoryId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message("Api Connection already exists with same environment and category combination!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(apiConnectionDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="apiConnectionDto">The API connection dto.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{connectionId}")]
        [HttpPut]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.ApiConnection)]
        public IHttpActionResult Update([FromBody]TblApiConnectionDto apiConnectionDto, long connectionId)
        {
            var data = this.apiConnectionService.GetByEnvironmentAndCategoryId(apiConnectionDto.EnvironmentId, apiConnectionDto.CategoryId);

            if (!data.IsError && data.Item != null && connectionId != data.Item.Id)
            {
                data.Messages.Add(new Message("Api Connection already exists with same environment and category combination!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            apiConnectionDto.Id = connectionId;
            return this.AddUpdate(apiConnectionDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="apiConnectionDto">The API connection dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblApiConnectionDto apiConnectionDto)
        {
            var result = new ResultMessage<TblApiConnectionDto>();
            try
            {
                result = this.apiConnectionService.SaveOrUpdate(apiConnectionDto, this.UserId);
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