// ---------------------------------------------------------------------------------------------------
// <copyright file="LocatorController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The LocatorController class
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
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The LocatorController class
    /// </summary>
    [RoutePrefix("api/locator")]
    [CustomAuthorize(Roles = RoleName.TestAdminRole)]
    public class LocatorController : BaseApiController
    {
        /// <summary>
        /// The locator service
        /// </summary>
        private readonly ILocatorService locatorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatorController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="locatorService">The locator service.</param>
        public LocatorController(ILoggerService loggerService, ILocatorService locatorService)
            : base(loggerService)
        {
            this.locatorService = locatorService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblLocatorDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblLocatorDto>>();
            try
            {
                result = this.locatorService.GetAll();
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
        /// <param name="locatorId">The identifier.</param>
        /// <returns>TblLocatorDto objects</returns>
        [Route("{locatorId}")]
        public IHttpActionResult GetById(long locatorId)
        {
            var result = new ResultMessage<TblLocatorDto>();
            try
            {
                result = this.locatorService.GetById(locatorId);
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
        /// <param name="locatorId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{locatorId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long locatorId)
        {
            var result = new ResultMessage<TblLocatorDto>();
            try
            {
                result = this.locatorService.DeleteById(locatorId, this.UserId);
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
        /// <param name="locatorDto">The locator dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblLocatorDto locatorDto)
        {
            var data = this.locatorService.GetByValue(locatorDto.Value);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Locator already exists with '" + locatorDto.Value + "' value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(locatorDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="locatorDto">The locator dto.</param>
        /// <param name="locatorId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{locatorId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblLocatorDto locatorDto, long locatorId)
        {
            var data = this.locatorService.GetByValue(locatorDto.Value);

            if (!data.IsError && data.Item != null && locatorId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Locator already exists with '" + locatorDto.Value + "' value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            locatorDto.Id = locatorId;
            return this.AddUpdate(locatorDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="locatorDto">The locator dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblLocatorDto locatorDto)
        {
            var result = new ResultMessage<TblLocatorDto>();
            try
            {
                result = this.locatorService.SaveOrUpdate(locatorDto, this.UserId);
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