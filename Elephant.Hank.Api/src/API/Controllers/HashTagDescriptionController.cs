// ---------------------------------------------------------------------------------------------------
// <copyright file="HashTagDescriptionController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-05-12</date>
// <summary>
//     The HashTagDescriptionController class
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
    /// The HashTagDescriptionController class
    /// </summary>
    [RoutePrefix("api/hash-tag-description")]
    public class HashTagDescriptionController : BaseApiController
    {
        /// <summary>
        /// The hash tag description service
        /// </summary>
        private readonly IHashTagDescriptionService hashTagDescriptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashTagDescriptionController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="hashTagDescriptionService">The hash tag description service.</param>
        public HashTagDescriptionController(ILoggerService loggerService, IHashTagDescriptionService hashTagDescriptionService)
            : base(loggerService)
        {
            this.hashTagDescriptionService = hashTagDescriptionService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>TblHashTagDescriptionDto List object</returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblHashTagDescriptionDto>>();
            try
            {
                result = this.hashTagDescriptionService.GetAll();
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
        /// <returns>TblHashTagDescriptionDto object by id</returns>
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblHashTagDescriptionDto>();
            try
            {
                result = this.hashTagDescriptionService.GetById(id);
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
        /// <returns>the deleted TblHashTagDescriptionDto object</returns>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long id)
        {
            var result = new ResultMessage<TblHashTagDescriptionDto>();
            try
            {
                result = this.hashTagDescriptionService.DeleteById(id, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Adds the specified hash tag description dto.
        /// </summary>
        /// <param name="hashTagDescriptionDto">The hash tag description dto.</param>
        /// <returns>TblHashTagDescriptionDto added object</returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblHashTagDescriptionDto hashTagDescriptionDto)
        {
            return this.AddUpdate(hashTagDescriptionDto);
        }

        /// <summary>
        /// Updates the specified hash tag description dto.
        /// </summary>
        /// <param name="hashTagDescriptionDto">The hash tag description dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>TblHashTagDescriptionDto updated object</returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblHashTagDescriptionDto hashTagDescriptionDto, long id)
        {
            hashTagDescriptionDto.Id = id;
            return this.AddUpdate(hashTagDescriptionDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="hashTagDescriptionDto">The hash tag description dto.</param>
        /// <returns>TblHashTagDescriptionDto added/updated object</returns>
        private IHttpActionResult AddUpdate(TblHashTagDescriptionDto hashTagDescriptionDto)
        {
            var result = new ResultMessage<TblHashTagDescriptionDto>();
            try
            {
                result = this.hashTagDescriptionService.SaveOrUpdate(hashTagDescriptionDto, this.UserId);
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