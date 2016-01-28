// ---------------------------------------------------------------------------------------------------
// <copyright file="EnvironmentController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-04</date>
// <summary>
//     The EnvironmentController class
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
    /// The EnvironmentController class
    /// </summary>
    [RoutePrefix("api/environment")]
    [CustomAuthorize(Roles = RoleName.TestAdminRole)]
    public class EnvironmentController : BaseApiController
    {
        /// <summary>
        /// The environment service
        /// </summary>
        private readonly IEnvironmentService environmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="environmentService">The environment service.</param>
        public EnvironmentController(ILoggerService loggerService, IEnvironmentService environmentService)
            : base(loggerService)
        {
            this.environmentService = environmentService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblEnvironmentDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblEnvironmentDto>>();
            try
            {
                result = this.environmentService.GetAll();
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
        /// <param name="environmentId">The identifier.</param>
        /// <returns>TblEnvironmentDto objects</returns>
        [Route("{environmentId}")]
        public IHttpActionResult GetById(long environmentId)
        {
            var result = new ResultMessage<TblEnvironmentDto>();
            try
            {
                result = this.environmentService.GetById(environmentId);
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
        /// <param name="environmentId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{environmentId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long environmentId)
        {
            var result = new ResultMessage<TblEnvironmentDto>();
            try
            {
                result = this.environmentService.DeleteById(environmentId, this.UserId);
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
        /// <param name="environmentDto">The environment dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblEnvironmentDto environmentDto)
        {
            var data = this.environmentService.GetByName(environmentDto.Name);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Environment already exists with '" + environmentDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(environmentDto);
        }

        /// <summary>
        /// Updates the specified environment dto.
        /// </summary>
        /// <param name="environmentDto">The environment dto.</param>
        /// <param name="environmentId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{environmentId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblEnvironmentDto environmentDto, long environmentId)
        {
            var data = this.environmentService.GetByName(environmentDto.Name);

            if (!data.IsError && data.Item != null && environmentId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Environment already exists with '" + environmentDto.Name + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            environmentDto.Id = environmentId;
            return this.AddUpdate(environmentDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="environmentDto">The environment dto.</param>
        /// <returns>Newly added object</returns>
        private IHttpActionResult AddUpdate(TblEnvironmentDto environmentDto)
        {
            var result = new ResultMessage<TblEnvironmentDto>();
            try
            {
                result = this.environmentService.SaveOrUpdate(environmentDto, this.UserId);
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