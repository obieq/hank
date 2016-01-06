// ---------------------------------------------------------------------------------------------------
// <copyright file="ActionController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ActionController class
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
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;    

    /// <summary>
    /// The ActionController class
    /// </summary>
    [RoutePrefix("api/action")]
    [Authorize]
    public class ActionController : BaseApiController
    {
        /// <summary>
        /// The actions service
        /// </summary>
        private readonly IActionsService actionsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="actionsService">The actions service.</param>
        public ActionController(ILoggerService loggerService, IActionsService actionsService)
            : base(loggerService)
        {
            this.actionsService = actionsService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblActionDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblActionDto>>();
            try
            {
                result = this.actionsService.GetAll();
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
        /// <returns>TblActionDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblActionDto>();
            try
            {
                result = this.actionsService.GetById(id);
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
            var result = new ResultMessage<TblActionDto>();
            try
            {
                result = this.actionsService.DeleteById(id, this.UserId);
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
        /// <param name="actionDto">The action dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblActionDto actionDto)
        {
            var data = this.actionsService.GetByValue(actionDto.Value);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Action already exists with '" + actionDto.Value + "' value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(actionDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="actionDto">The action dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblActionDto actionDto, long id)
        {
            var data = this.actionsService.GetByValue(actionDto.Value);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Action already exists with '" + actionDto.Value + "' value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            actionDto.Id = id;
            return this.AddUpdate(actionDto);
        }

        /// <summary>
        /// Get all action constants
        /// </summary>
        /// <returns>ActionConstants object</returns>
        [Route("action-constants")]
        public IHttpActionResult GetActionConstants()
        {
            var result = new ResultMessage<ActionConstants>();
            try
            {
                ActionConstants actionConstants = new ActionConstants();
                result.Item = actionConstants;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get all action constants
        /// </summary>
        /// <returns>ActionConstants object</returns>
        [Route("action-for-sql-test-step")]
        public IHttpActionResult GetActionForSqlTestStep()
        {
            var result = new ResultMessage<IEnumerable<TblActionDto>>();
            try
            {
                result = this.actionsService.GetActionForSqlTestStep();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="actionDto">The action dto.</param>
        /// <returns>Newly added object</returns>
        private IHttpActionResult AddUpdate(TblActionDto actionDto)
        {
            var result = new ResultMessage<TblActionDto>();
            try
            {
                result = this.actionsService.SaveOrUpdate(actionDto, this.UserId);
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