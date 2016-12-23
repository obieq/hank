// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportLinkDataController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-12-23</date>
// <summary>
//     The ReportLinkDataController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.CustomValidationService;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// the ReportLinkDataController class
    /// </summary>
    public class ReportLinkDataController : BaseApiController
    {
        /// <summary>
        /// The report data service
        /// </summary>
        private readonly IReportLinkDataService reportLinkDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportLinkDataController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="reportLinkDataService">The report link data service.</param>
        public ReportLinkDataController(ILoggerService loggerService, IReportLinkDataService reportLinkDataService)
            : base(loggerService)
        {
            this.reportLinkDataService = reportLinkDataService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblReportLinkDataDto objects</returns>
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblReportLinkDataDto>>();
            try
            {
                result = this.reportLinkDataService.GetAll();
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
        /// <param name="reportLinkId">The identifier.</param>
        /// <returns>TblReportLinkDataDto objects</returns>
        [Route("{reportLinkId}")]
        [AllowAnonymous]
        public IHttpActionResult GetById(long reportLinkId)
        {
            var result = new ResultMessage<TblReportLinkDataDto>();
            try
            {
                result = this.reportLinkDataService.GetById(reportLinkId);
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
        /// <param name="reportLinkDatadto">The report link dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblReportLinkDataDto reportLinkDatadto)
        {           
            return this.AddUpdate(reportLinkDatadto);
        }

        /// <summary>
        /// Updates the specified environment dto.
        /// </summary>
        /// <param name="reportLinkDatadto">The report link dto.</param>
        /// <param name="reportLinkId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{reportLinkId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblReportLinkDataDto reportLinkDatadto, long reportLinkId)
        {          
            reportLinkDatadto.Id = reportLinkId;
            return this.AddUpdate(reportLinkDatadto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="reportLinkDatadto">The report link  dto.</param>
        /// <returns>Newly added object</returns>
        private IHttpActionResult AddUpdate(TblReportLinkDataDto reportLinkDatadto)
        {
            var result = new ResultMessage<TblReportLinkDataDto>();
            try
            {
                result = this.reportLinkDataService.SaveOrUpdate(reportLinkDatadto, this.UserId);
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