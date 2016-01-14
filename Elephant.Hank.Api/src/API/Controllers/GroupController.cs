// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The GroupController class
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
    /// The GroupsController class
    /// </summary>
    [RoutePrefix("api/group")]
    [Authorize]
    public class GroupController : BaseApiController
    {
        /// <summary>
        /// The group service
        /// </summary>
        private readonly IGroupService groupService;

        /// <summary>
        /// The group module access service
        /// </summary>
        private readonly IGroupModuleAccessService groupModuleAccessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController"/> class.
        /// </summary>
        /// <param name="loggerService">the logger service</param>
        /// <param name="groupService">the group service</param>
        public GroupController(ILoggerService loggerService, IGroupService groupService, IGroupModuleAccessService groupModuleAccessService)
            : base(loggerService)
        {
            this.groupService = groupService;
            this.groupModuleAccessService = groupModuleAccessService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblGroupDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblGroupDto>>();
            try
            {
                result = this.groupService.GetAll();
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
        /// <returns>TblGroupDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblGroupDto>();
            try
            {
                result = this.groupService.GetById(id);
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
            var result = new ResultMessage<TblGroupDto>();
            try
            {
                result = this.groupService.DeleteById(id, this.UserId);
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
        /// <param name="groupDto">The group dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblGroupDto groupDto)
        {
            return this.AddUpdate(groupDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="groupDto">The group dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblGroupDto groupDto, long id)
        {
            groupDto.Id = id;
            return this.AddUpdate(groupDto);
        }

        /// <summary>
        /// Add/Delete the permissions on website
        /// </summary>
        /// <returns></returns>
        [Route("{id}/add-website-to-group")]
        [HttpPost]
        public IHttpActionResult AddWebsiteToGroup(long id, long[] WebsiteIdList)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            try
            {
                result = this.groupModuleAccessService.AddUpdateWebsiteToGroup(id, WebsiteIdList);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        [Route("{groupId}/group-module-access")]
        [HttpGet]
        public IHttpActionResult GetByGroupId(long groupId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            try
            {
                result = this.groupModuleAccessService.GetByGroupId(groupId);
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
        /// <param name="groupDto">The browser dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblGroupDto groupDto)
        {
            var result = new ResultMessage<TblGroupDto>();
            try
            {
                if (groupDto.Id == 0)
                {
                    this.groupService.AddNewGroup(groupDto, this.UserId);
                }
                else
                {
                    result = this.groupService.SaveOrUpdate(groupDto, this.UserId);
                }
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