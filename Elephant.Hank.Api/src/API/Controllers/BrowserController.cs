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

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The BrowserController class
    /// </summary>
    [RoutePrefix("api/browser")]
    [Authorize]
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
        /// <param name="id">The identifier.</param>
        /// <returns>TblBrowsersDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblBrowsersDto>();
            try
            {
                result = this.browserService.GetById(id);
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
            var result = new ResultMessage<TblBrowsersDto>();
            try
            {
                result = this.browserService.DeleteById(id, this.UserId);
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
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblBrowsersDto browsersDto, long id)
        {
            browsersDto.Id = id;
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