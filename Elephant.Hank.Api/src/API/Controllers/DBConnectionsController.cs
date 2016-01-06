//// ---------------------------------------------------------------------------------------------------
//// <copyright file="DBConnectionsController.cs" company="Elephant Insurance Services, LLC">
////     Copyright (c) 2015 All Right Reserved
//// </copyright>
//// <author>Vyom Sharma</author>
//// <date>2015-12-11</date>
//// <summary>
////     The DBConnectionsController class
//// </summary>
//// ---------------------------------------------------------------------------------------------------

//namespace Elephant.Protractor.Testing.Api.Controllers
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Web;
//    using System.Web.Http;

//    using Elephant.Protractor.Testing.Common.LogService;
//    using Elephant.Protractor.Testing.Common.TestDataServices;
//    using Elephant.Protractor.Testing.Framework.Extensions;
//    using Elephant.Protractor.Testing.Resources.Dto;
//    using Elephant.Protractor.Testing.Resources.Messages;

//    /// <summary>
//    /// The DBConnectionsController class
//    /// </summary>
//    [RoutePrefix("api/dbconnections")]
//    [Authorize]
//    public class DBConnectionsController : BaseApiController
//    {
//        /// <summary>
//        /// The test data service
//        /// </summary>
//        private readonly IDBConnectionsService dbConnectionService;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DBConnectionsController"/> class.
//        /// </summary>
//        /// <param name="loggerService">The logger service.</param>
//        /// <param name="dbConnectionService">The db connection service.</param>
//        public DBConnectionsController(ILoggerService loggerService, IDBConnectionsService dbConnectionService)
//            : base(loggerService)
//        {
//            this.dbConnectionService = dbConnectionService;
//        }

//        /// <summary>
//        /// Add DBConnection entry in the database
//        /// </summary>
//        /// <param name="dbConnectionDto">dbConnectionDto object</param>
//        /// <returns>
//        /// Added dbConnectionDto Object
//        /// </returns>
//        [HttpPost]
//        public IHttpActionResult Add([FromBody]TblDBConnectionsDto dbConnectionDto)
//        {
//            return this.AddUpdate(dbConnectionDto);
//        }

//        /// <summary>
//        /// Updates the specified DBConnection dto.
//        /// </summary>
//        /// <param name="dbConnectionDto">The dbConnection dto.</param>
//        /// <param name="id">The identifier.</param>
//        /// <returns>
//        /// Newly updated object
//        /// </returns>
//        [Route("{id}")]
//        [HttpPut]
//        public IHttpActionResult Update([FromBody]TblDBConnectionsDto dbConnectionDto, long id)
//        {
//            dbConnectionDto.Id = id;
//            return this.AddUpdate(dbConnectionDto);
//        }

//        /// <summary>
//        /// get the list of all data base
//        /// </summary>
//        /// <param name="dbConnectionDto">database identifier details</param>
//        /// <returns>list of data base</returns>
//        [Route("get-database-list")]
//        [HttpPost]
//        public IHttpActionResult GetDatabaseList([FromBody]TblDBConnectionsDto dbConnectionDto)
//        {
//            var result = new ResultMessage<List<string>>();
//            try
//            {
//                result = this.dbConnectionService.GetDatabaseList(dbConnectionDto);
//            }
//            catch (Exception ex)
//            {
//                this.LoggerService.LogException(ex);
//                result.Messages.Add(new Message(null, ex.Message));
//            }

//            return this.CreateCustomResponse(result);
//        }

//        #region All Private

//        /// <summary>
//        /// Add/Update the DBConnection Object
//        /// </summary>
//        /// <param name="dbConnectionDto">DBConnectionDto object</param>
//        /// <returns>
//        /// Added/Updated DBConnectionDto Object
//        /// </returns>
//        private IHttpActionResult AddUpdate(TblDBConnectionsDto dbConnectionDto)
//        {
//            var result = new ResultMessage<TblDBConnectionsDto>();
//            try
//            {
//                result = this.dbConnectionService.SaveOrUpdate(dbConnectionDto, this.UserId);
//            }
//            catch (Exception ex)
//            {
//                this.LoggerService.LogException(ex);
//                result.Messages.Add(new Message(null, ex.Message));
//            }

//            return this.CreateCustomResponse(result);
//        }

//        #endregion
//    }
//}