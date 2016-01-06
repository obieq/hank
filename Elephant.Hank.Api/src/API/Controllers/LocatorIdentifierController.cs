// ---------------------------------------------------------------------------------------------------
// <copyright file="LocatorIdentifierController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The LocatorIdentifierController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The LocatorIdentifierController class
    /// </summary>
    [RoutePrefix("api/locator-identifier")]
    [Authorize]
    public class LocatorIdentifierController : BaseApiController
    {
        /// <summary>
        /// The locator identifier service
        /// </summary>
        private readonly ILocatorIdentifierService locatorIdentifierService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatorIdentifierController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="locatorIdentifierService">The locator identifier service.</param>
        public LocatorIdentifierController(ILoggerService loggerService, ILocatorIdentifierService locatorIdentifierService)
            : base(loggerService)
        {
            this.locatorIdentifierService = locatorIdentifierService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblLocatorIdentifierDto objects</returns>
        [Route]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblLocatorIdentifierDto>>();
            try
            {
                result = this.locatorIdentifierService.GetAll();
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
        /// <returns>TblLocatorIdentifierDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblLocatorIdentifierDto>();
            try
            {
                result = this.locatorIdentifierService.GetById(id);
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
            var result = new ResultMessage<TblLocatorIdentifierDto>();
            try
            {
                result = this.locatorIdentifierService.DeleteById(id, this.UserId);
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
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [Route]
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblLocatorIdentifierDto locatorIdentifierDto)
        {
            var data = this.locatorIdentifierService.IsExisting(locatorIdentifierDto);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Page already has '" + locatorIdentifierDto.DisplayName + "' as display name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(locatorIdentifierDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblLocatorIdentifierDto locatorIdentifierDto, long id)
        {
            var data = this.locatorIdentifierService.IsExisting(locatorIdentifierDto);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Page already has '" + locatorIdentifierDto.DisplayName + "' as display name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            locatorIdentifierDto.Id = id;
            return this.AddUpdate(locatorIdentifierDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblLocatorIdentifierDto locatorIdentifierDto)
        {
            var result = new ResultMessage<TblLocatorIdentifierDto>();
            try
            {
                result = this.locatorIdentifierService.SaveOrUpdate(locatorIdentifierDto, this.UserId);
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