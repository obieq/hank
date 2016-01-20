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
        /// The group module access service
        /// </summary>
        private readonly IGroupUserService groupUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupController"/> class.
        /// </summary>
        /// <param name="loggerService">the logger service</param>
        /// <param name="groupService">the group service</param>
        /// <param name="groupModuleAccessService">the group module access service</param>
        /// <param name="groupUserService">the group user service</param>
        public GroupController(ILoggerService loggerService, IGroupService groupService, IGroupModuleAccessService groupModuleAccessService, IGroupUserService groupUserService)
            : base(loggerService)
        {
            this.groupService = groupService;
            this.groupModuleAccessService = groupModuleAccessService;
            this.groupUserService = groupUserService;
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
        /// <param name="groupId">The identifier.</param>
        /// <returns>TblGroupDto objects</returns>
        [Route("{groupId}")]
        public IHttpActionResult GetById(long groupId)
        {
            var result = new ResultMessage<TblGroupDto>();
            try
            {
                result = this.groupService.GetById(groupId);
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
        /// <param name="groupId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{groupId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long groupId)
        {
            var result = new ResultMessage<TblGroupDto>();
            try
            {
                result = this.groupService.DeleteById(groupId, this.UserId);
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
        /// <param name="groupId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{groupId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblGroupDto groupDto, long groupId)
        {
            groupDto.Id = groupId;
            return this.AddUpdate(groupDto);
        }

        /// <summary>
        /// Add/Delete the permissions on website
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <param name="websiteIdList">wibsite id list</param>
        /// <returns>TblGroupModuleAccessDto list objects</returns>
        [Route("{groupId}/add-website-to-group")]
        [HttpPost]
        public IHttpActionResult AddWebsiteToGroup(long groupId, long[] websiteIdList)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            try
            {
                result = this.groupModuleAccessService.AddUpdateWebsiteToGroup(groupId, websiteIdList);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get the GroupModule Access entry by group id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <returns>List of TblGroupModuleAccessDto</returns>
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
        /// Get the GroupUser by groupId
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <returns>TblGroupUserDto object list</returns>
        [Route("{groupId}/user")]
        [HttpGet]
        public IHttpActionResult GetUserByGroupId(long groupId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupUserDto>>();
            try
            {
                result = this.groupUserService.GetByGroupId(groupId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get the GroupModule Access entry by group id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <param name="websiteId">the website identifier</param>
        /// <returns>List of TblGroupModuleAccessDto</returns>
        [Route("{groupId}/website/{websiteId}")]
        [HttpGet]
        public IHttpActionResult GetByGroupId(long groupId, long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            try
            {
                result = this.groupModuleAccessService.GetByGroupIdAndWebsiteId(groupId, websiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Deletes the TblGroupUserDto entry by group id and userId
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <param name="userId">the user identifier</param>
        /// <returns>TblGroupUserDto object</returns>
        [HttpGet]
        [Route("{groupId}/user/{userid}/remove-from-group")]
        public IHttpActionResult DeleteByGroupIdAndUserId(long groupId, long userId)
        {
            var result = new ResultMessage<TblGroupUserDto>();
            try
            {
                result = this.groupUserService.DeleteByGroupIdAndUserId(groupId, userId, this.UserId);
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