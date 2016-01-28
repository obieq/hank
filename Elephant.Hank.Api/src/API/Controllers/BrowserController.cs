// ---------------------------------------------------------------------------------------------------
// <copyright file="BrowserController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-09</date>
// <summary>
//     The BrowserController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The BrowserController class
    /// </summary>
    [RoutePrefix("api/browser")]
    [CustomAuthorize(Roles = RoleName.TestAdminRole)]
    public class BrowserController : BaseApiController
    {
        /// <summary>
        /// The browser service
        /// </summary>
        private readonly IBrowserService browserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="browserService">The browser service.</param>
        public BrowserController(ILoggerService loggerService, IBrowserService browserService)
            : base(loggerService)
        {
            this.browserService = browserService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblBrowsersDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblBrowsersDto>>();
            try
            {
                result = this.browserService.GetAll();
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
        /// <param name="browserId">The identifier.</param>
        /// <returns>TblBrowsersDto objects</returns>
        [Route("{browserId}")]
        public IHttpActionResult GetById(long browserId)
        {
            var result = new ResultMessage<TblBrowsersDto>();
            try
            {
                result = this.browserService.GetById(browserId);
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
        /// <param name="browserId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{browserId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long browserId)
        {
            var result = new ResultMessage<TblBrowsersDto>();
            try
            {
                result = this.browserService.DeleteById(browserId, this.UserId);
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
        /// <param name="browsersDto">The browser dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblBrowsersDto browsersDto)
        {
            return this.AddUpdate(browsersDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="browsersDto">The browser dto.</param>
        /// <param name="browserId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblBrowsersDto browsersDto, long browserId)
        {
            browsersDto.Id = browserId;
            return this.AddUpdate(browsersDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="browsersDto">The browser dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblBrowsersDto browsersDto)
        {
            var result = new ResultMessage<TblBrowsersDto>();
            try
            {
                result = this.browserService.SaveOrUpdate(browsersDto, this.UserId);
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