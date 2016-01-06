// ---------------------------------------------------------------------------------------------------
// <copyright file="UserProfileController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The UserProfileController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ProfileController class
    /// </summary>
    [RoutePrefix("api/profile")]
    [Authorize]
    public class UserProfileController : BaseApiController
    {
        /// <summary>
        /// The User profile service
        /// </summary>
        private readonly IUserProfileService userProfileService;

        /// <summary>
        /// The User profile service
        /// </summary>
        private readonly CustomUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="userProfileService">The user profile service.</param>
        public UserProfileController(ILoggerService loggerService, IUserProfileService userProfileService)
            : base(loggerService)
        {
            this.userProfileService = userProfileService;
        }

        /// <summary>
        /// Get the user profile details
        /// </summary>
        /// <param name="id">the user profile identifier</param>
        /// <returns>TblUserProfileDto object </returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblUserProfileDto>();
            try
            {
                result = this.userProfileService.GetById(id);
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
        /// <param name="id">user identifier</param>
        /// <returns>updated user profile object</returns>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update([FromBody]TblUserProfileDto userProfileDto, long id)
        {
            userProfileDto.Id = id;
            return this.AddUpdate(userProfileDto);
        }

        /// <summary>
        /// Get the user profile details
        /// </summary>
        /// <param name="id">the user profile identifier</param>
        /// <returns>TblUserProfileDto object </returns>
        [Route("get-user-profile/{id}")]
        public IHttpActionResult GetByUserId(long id)
        {
            var result = new ResultMessage<TblUserProfileDto>();
            try
            {
                result = this.userProfileService.GetById(id);
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
        /// <param name="userProfileDto">The TblUserProfile dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblUserProfileDto userProfileDto)
        {
            var result = new ResultMessage<TblUserProfileDto>();
            try
            {
                result = this.userProfileService.SaveUpdateCustom(userProfileDto, this.UserId);
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