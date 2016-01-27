// ---------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The UserController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.CustomIdentity;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The User controller class
    /// </summary>
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// The environment service
        /// </summary>
        private readonly IUserProfileService userProfileService;

        /// <summary>
        /// The environment service
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="userProfileService">The user profile service.</param>
        /// <param name="authRepository">The authRepository service.</param>
        public UserController(ILoggerService loggerService, IUserProfileService userProfileService, IAuthRepository authRepository)
            : base(loggerService)
        {
            this.userProfileService = userProfileService;
            this.authRepository = authRepository;
        }

        /// <summary>
        /// Get List off all users
        /// </summary>
        /// <returns>List of Users</returns>
        public IHttpActionResult GetAllUsers()
        {
            var result = new ResultMessage<IEnumerable<CustomUserDto>>();
            try
            {
                result = this.authRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Enable or disables the user
        /// </summary>
        /// <param name="activationId">activation id of user</param>
        /// <param name="enabled">true to enable false for disable</param>
        /// <returns>Returns list of users</returns>
        [Route("{activationId}/set-lockout-enabled/{enabled}")]
        [HttpGet]
        public IHttpActionResult SetLockoutEnabled(Guid activationId, bool enabled)
        {
            var result = new ResultMessage<IEnumerable<CustomUserDto>>();
            try
            {
                var lockOutResult = this.authRepository.ChangeActivationStatus(activationId, enabled);
                return this.GetAllUsers();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get the user profile details
        /// </summary>
        /// <returns>TblUserProfileDto object </returns>
        [Route("user-profile")]
        public IHttpActionResult GetByUserId()
        {
            var result = new ResultMessage<TblUserProfileDto>();
            try
            {
                result = this.userProfileService.GetByUserId(this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Update the user's profile
        /// </summary>
        /// <param name="userProfileDto">user profile object</param>
        /// <param name="id">profile identifier</param>
        /// <returns>updated user profile object</returns>
        [HttpPut]
        [Route("{id}/user-profile")]
        public IHttpActionResult Update([FromBody]TblUserProfileDto userProfileDto, long id)
        {
            userProfileDto.Id = this.UserId;
            return this.AddUpdate(userProfileDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="userProfileDto">The TblUserProfile dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblUserProfileDto userProfileDto)
        {
            var result = new ResultMessage<TblUserProfileDto>();
            try
            {
                result = this.userProfileService.SaveOrUpdate(userProfileDto, this.UserId);
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