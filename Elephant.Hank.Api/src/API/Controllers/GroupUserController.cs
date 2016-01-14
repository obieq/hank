// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupUserController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The GroupUserController class
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
    /// The GroupUserController class
    /// </summary>
    [RoutePrefix("api/group-user")]
    [Authorize]
    public class GroupUserController : BaseApiController
    {
        /// <summary>
        /// The group module access service
        /// </summary>
        private readonly IGroupUserService groupUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupUserController"/> class.
        /// </summary>
        /// <param name="loggerService">the logger service</param>
        /// <param name="groupUserService">the group user service</param>
        public GroupUserController(ILoggerService loggerService, IGroupUserService groupUserService)
            : base(loggerService)
        {
            this.groupUserService = groupUserService;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="actionDto">The action dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TblGroupUserDto actionDto)
        {
            var data = this.groupUserService.GetByGroupIdAndUserIdEitherDeletedOrNonDeleted(actionDto.GroupId, actionDto.UserId);

            if (!data.IsError && data.Item.IsDeleted)
            {
                data.Item.IsDeleted = false;
                return this.AddUpdate(data.Item);
            }

            return this.AddUpdate(actionDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="groupUserDto">The action dto.</param>
        /// <returns>Newly added object</returns>
        private IHttpActionResult AddUpdate(TblGroupUserDto groupUserDto)
        {
            var result = new ResultMessage<TblGroupUserDto>();
            try
            {
                result = this.groupUserService.SaveOrUpdate(groupUserDto, this.UserId);
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