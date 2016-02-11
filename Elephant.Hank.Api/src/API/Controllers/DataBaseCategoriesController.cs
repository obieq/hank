// ---------------------------------------------------------------------------------------------------
// <copyright file="DataBaseCategoriesController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The DataBaseCategoriesController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Common.LogService;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The DataBaseCategoriesController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/data-base-categories")]
    public class DataBaseCategoriesController : BaseApiController
    {
        /// <summary>
        /// The DataBaseCategories service
        /// </summary>
        private readonly IDataBaseCategoriesService databaseCategoriesService;

        /// <summary>
        /// The DataBaseCategories service
        /// </summary>
        private readonly IDataBaseConnectionService databaseConnectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseCategoriesController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="databaseCategoriesService">The databaseCategories service.</param>
        /// <param name="databaseConnectionService">The databaseConnection service.</param>
        public DataBaseCategoriesController(ILoggerService loggerService, IDataBaseCategoriesService databaseCategoriesService, IDataBaseConnectionService databaseConnectionService)
            : base(loggerService)
        {
            this.databaseCategoriesService = databaseCategoriesService;
            this.databaseConnectionService = databaseConnectionService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>List of TblDataBaseCategoriesDto objects</returns>
        [HttpGet]
        [Route("")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.DataBaseCategories)]
        public IHttpActionResult GetAll(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseCategoriesDto>>();
            try
            {
                result = this.databaseCategoriesService.GetByWebsiteId(websiteId);
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
        /// <param name="databaseCategoryId">The identifier.</param>
        /// <returns>TblDataBaseCategoriesDto objects</returns>
        [Route("{databaseCategoryId}")]
        [HttpGet]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.DataBaseCategories)]
        public IHttpActionResult GetById(long databaseCategoryId)
        {
            var result = new ResultMessage<TblDataBaseCategoriesDto>();
            try
            {
                result = this.databaseCategoriesService.GetById(databaseCategoryId);
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
        /// <param name="databaseCategoryId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{databaseCategoryId}")]
        [HttpDelete]
        [CustomAuthorize(ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.DataBaseCategories)]
        public IHttpActionResult DeleteById(long databaseCategoryId)
        {
            var result = new ResultMessage<TblDataBaseCategoriesDto>();
            try
            {
                result = this.databaseCategoriesService.DeleteById(databaseCategoryId, this.UserId);
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
        /// <param name="dataBaseCategoriesDto">The DataBaseCategories dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.DataBaseCategories)]
        public IHttpActionResult Add([FromBody]TblDataBaseCategoriesDto dataBaseCategoriesDto)
        {
            var data = this.databaseCategoriesService.GetByName(dataBaseCategoriesDto.Name);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "DataBase Categorie already exists with '" + dataBaseCategoriesDto.Name + "' Name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(dataBaseCategoriesDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="dataBaseCategoriesDto">The data base categories dto.</param>
        /// <param name="databaseCategoryId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{databaseCategoryId}")]
        [HttpPut]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.DataBaseCategories)]
        public IHttpActionResult Update([FromBody]TblDataBaseCategoriesDto dataBaseCategoriesDto, long databaseCategoryId)
        {
            var data = this.databaseCategoriesService.GetByName(dataBaseCategoriesDto.Name);

            if (!data.IsError && data.Item != null && databaseCategoryId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Action already exists with '" + dataBaseCategoriesDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            dataBaseCategoriesDto.Id = databaseCategoryId;
            return this.AddUpdate(dataBaseCategoriesDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="dataBaseCategoriesDto">The database categories dto.</param>
        /// <returns>Newly added object</returns>
        private IHttpActionResult AddUpdate(TblDataBaseCategoriesDto dataBaseCategoriesDto)
        {
            var result = new ResultMessage<TblDataBaseCategoriesDto>();
            try
            {
                result = this.databaseCategoriesService.SaveOrUpdate(dataBaseCategoriesDto, this.UserId);
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