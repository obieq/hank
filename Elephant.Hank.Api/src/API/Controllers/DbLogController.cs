// ---------------------------------------------------------------------------------------------------
// <copyright file="DbLogController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-08-24</date>
// <summary>
//     The DbLogController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The DbLogController class
    /// </summary>
    [RoutePrefix("api/dblog")]
    [Authorize]
    public class DbLogController : BaseApiController
    {
        /// <summary>
        /// The DbLog service
        /// </summary>
        private readonly IDbLogService dbLogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbLogController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="dbLogService">The dbLog service.</param>
        public DbLogController(ILoggerService loggerService, IDbLogService dbLogService)
            : base(loggerService)
        {
            this.dbLogService = dbLogService;
        }

        /// <summary>
        /// Get Data in chunks of 50
        /// </summary>
        /// <param name="chunk">last chunk identifier</param>
        /// <param name="tableType">Table type filter</param>
        /// <param name="model">date range filter</param>
        /// <returns>TblDbLog List filtered object</returns>
        [Route("{chunk}/{tableType}")]
        [HttpPost]
        public IHttpActionResult GetChunk(long chunk, string tableType, SearchLogModel model)
        {
            var result = new ResultMessage<IEnumerable<TblDbLogDto>>();
            try
            {
                result = this.dbLogService.GetChunk(chunk, tableType, model);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Roll back/forward the data
        /// </summary>
        /// <param name="rollData">roll data object</param>
        /// <returns>updated rolled value</returns>
        [HttpPost]
        [Route("roll-data")]
        public IHttpActionResult RollData(RollDataDto rollData)
        {
            var result = new ResultMessage<TblDbLogDto>();
            try
            {
                this.dbLogService.RollData(rollData.LogId, rollData.ToOldValue);
                result = this.dbLogService.GetById(rollData.LogId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get Data with in the time range
        /// </summary>
        /// <param name="model">search parameters</param>
        /// <returns>TblDBLog Object</returns>
        [HttpPost]
        [Route("get-data-range")]
        public IHttpActionResult GetDataWithInDateTimeRange(SearchLogModel model)
        {
            var result = new ResultMessage<IEnumerable<TblDbLogDto>>();
            try
            {
                result = this.dbLogService.GetDataWithInDateTimeRange(model.StartDate, model.EndDate);
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