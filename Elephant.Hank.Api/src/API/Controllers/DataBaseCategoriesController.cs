﻿// ---------------------------------------------------------------------------------------------------
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
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Web.Http;

    using Common.DataService;
    using Common.LogService;

    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.CustomIdentity;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    using Newtonsoft.Json.Linq;

    using Resources.Models;

    /// <summary>
    /// The DataBaseCategoriesController class
    /// </summary>
    [Authorize]
    [RoutePrefix("api/data-base-categories")]
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
        /// <returns>List of TblDataBaseCategoriesDto objects</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseCategoriesDto>>();
            try
            {
                result = this.databaseCategoriesService.GetAll();
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
        /// <returns>TblDataBaseCategoriesDto objects</returns>
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblDataBaseCategoriesDto>();
            try
            {
                result = this.databaseCategoriesService.GetById(id);
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
            var result = new ResultMessage<TblDataBaseCategoriesDto>();
            try
            {
                result = this.databaseCategoriesService.DeleteById(id, this.UserId);
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
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblDataBaseCategoriesDto dataBaseCategoriesDto, long id)
        {
            var data = this.databaseCategoriesService.GetByName(dataBaseCategoriesDto.Name);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Action already exists with '" + dataBaseCategoriesDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            dataBaseCategoriesDto.Id = id;
            return this.AddUpdate(dataBaseCategoriesDto);
        }

        #region - Child DataBaseConnection

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="categoryId">The identifier.</param>
        /// <returns>TblDataBaseCategoriesDto objects</returns>
        [Route("{categoryId}/data-base-connection")]
        [HttpGet]
        public IHttpActionResult GetDataBaseConnectionByCategoryId(long categoryId)
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseConnectionDto>>();
            try
            {
                result = this.databaseConnectionService.GetByCategoryId(categoryId);
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