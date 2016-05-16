// ---------------------------------------------------------------------------------------------------
// <copyright file="RequestTypesController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-05-16</date>
// <summary>
//     The RequestTypesController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The RequestTypesController classs
    /// </summary>
    [RoutePrefix("api/request-types")]
    public class RequestTypesController : BaseApiController
    {
        /// <summary>
        /// The actions service
        /// </summary>
        private readonly IRequestTypesService requestTypesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTypesController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="requestTypesService">The actions service.</param>
        public RequestTypesController(ILoggerService loggerService, IRequestTypesService requestTypesService)
            : base(loggerService)
        {
            this.requestTypesService = requestTypesService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblRequestTypesDto objects</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblRequestTypesDto>>();
            try
            {
                result = this.requestTypesService.GetAll();
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