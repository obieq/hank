// ---------------------------------------------------------------------------------------------------
// <copyright file="DataBaseConnectionController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The DataBaseConnectionController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Common.LogService;

    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The DataBaseConnectionController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/data-base-categories/{databaseCategoryId}/data-base-connection")]
    public class DataBaseConnectionController : BaseApiController
    {
        /// <summary>
        /// The DataBaseConnection service
        /// </summary>
        private readonly IDataBaseConnectionService databaseConnectionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseConnectionController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="databaseConnectionService">The actions service.</param>
        public DataBaseConnectionController(ILoggerService loggerService, IDataBaseConnectionService databaseConnectionService)
            : base(loggerService)
        {
            this.databaseConnectionService = databaseConnectionService;
        }

        /// <summary>
        /// Get all data base connection by category identifier
        /// </summary>
        /// <param name="databaseCategoryId">database category identifier</param>
        /// <returns>TblDataBaseConnectionDto list object</returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll(long databaseCategoryId)
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseConnectionDto>>();
            try
            {
                result = this.databaseConnectionService.GetByCategoryId(databaseCategoryId);
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
        /// <param name="databaseConnectionId">The identifier.</param>
        /// <returns>TblDataBaseConnectionDto objects</returns>
        [HttpGet]
        [Route("{databaseConnectionId}")]
        public IHttpActionResult GetById(long databaseConnectionId)
        {
            var result = new ResultMessage<TblDataBaseConnectionDto>();
            try
            {
                result = this.databaseConnectionService.GetById(databaseConnectionId);
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
        /// <param name="databaseConnectionId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{databaseConnectionId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long databaseConnectionId)
        {
            var result = new ResultMessage<TblDataBaseConnectionDto>();
            try
            {
                result = this.databaseConnectionService.DeleteById(databaseConnectionId, this.UserId);
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
        /// <param name="dataBaseConnectionDto">The DataBaseConnection dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TblDataBaseConnectionDto dataBaseConnectionDto)
        {
            var data = this.databaseConnectionService.GetSensitiveDataByEnvironmentAndCategoryId(dataBaseConnectionDto.EnvironmentId, dataBaseConnectionDto.CategoryId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "DataBase Connection already exists with same environment and category combination"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(dataBaseConnectionDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="dataBaseConnectionDto">The data base connection dto.</param>
        /// <param name="databaseConnectionId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{databaseConnectionId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblDataBaseConnectionDto dataBaseConnectionDto, long databaseConnectionId)
        {
            var data = this.databaseConnectionService.GetSensitiveDataByEnvironmentAndCategoryId(dataBaseConnectionDto.EnvironmentId, dataBaseConnectionDto.CategoryId);

            if (!data.IsError && data.Item != null && databaseConnectionId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "DataBase Connection already exists with same environment and category combination"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            dataBaseConnectionDto.Id = databaseConnectionId;
            return this.AddUpdate(dataBaseConnectionDto);
        }

        /// <summary>
        /// Get the list of all data bases
        /// </summary>
        /// <param name="dataBaseConnectionDto">dataBaseConnectionDto object</param>
        /// <returns>string list with all database name</returns>
        [HttpPost]
        [Route("get-database-list")]
        public IHttpActionResult GetAllDataBaseList(TblDataBaseConnectionDto dataBaseConnectionDto)
        {
            var result = new ResultMessage<List<string>>();
            try
            {
                result = this.databaseConnectionService.GetAllDataBaseList(dataBaseConnectionDto);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="dataBaseConnectionDto">The database connection dto.</param>
        /// <returns>Newly added object</returns>
        private IHttpActionResult AddUpdate(TblDataBaseConnectionDto dataBaseConnectionDto)
        {
            var result = new ResultMessage<TblDataBaseConnectionDto>();
            try
            {
                result = this.databaseConnectionService.SaveOrUpdate(dataBaseConnectionDto, this.UserId);
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