// ---------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The AccountController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Common.DataService;
    using Common.LogService;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;

    using Microsoft.AspNet.Identity;

    using Resources.Models;

    /// <summary>
    /// The AccountController class
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// The authentication repository
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// The group module service
        /// </summary>
        private readonly IGroupModuleAccessService groupModuleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="authRepository">The authentication repository.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="groupModuleService">The group Module service.</param>
        public AccountController(IAuthRepository authRepository, ILoggerService loggerService, IGroupModuleAccessService groupModuleService)
            : base(loggerService)
        {
            this.authRepository = authRepository;
            this.groupModuleService = groupModuleService;
        }

        /// <summary>
        /// Check User is Autheorised for module or not
        /// </summary>
        /// <returns>True if user is Authenticated</returns>
        [Route("GetModuleAuthenticatedToUser")]
        [HttpGet]
        public IHttpActionResult IsModuleAuthenticated()
        {
            ResultMessage<IEnumerable<ModuleAuthenticationModel>> result = new ResultMessage<IEnumerable<ModuleAuthenticationModel>>();
            try
            {
                result = this.groupModuleService.GetModuleAuthenticatedToUser(this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// The activate.
        /// </summary>
        /// <param name="activationId">The activation id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("activate/{activationId}")]
        public async Task<IHttpActionResult> Activate(string activationId)
        {
            Guid activationIdGuid = activationId.ToGuid();

            try
            {
                if (activationIdGuid != Guid.Empty)
                {
                    var result = await this.authRepository.ChangeActivationStatus(activationIdGuid);

                    if (result)
                    {
                        return this.Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Activate: " + ex.Message);
            }

            this.ModelState.AddModelError(string.Empty, "Invalid activation request!");

            return this.BadRequest(this.ModelState);
        }

        /// <summary>
        /// Registers the specified user model. POST api/Account/Register
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>Registration status</returns>
        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            var result = new ResultMessage<bool>();

            if (!this.ModelState.IsValid)
            {
                result.Messages.AddRange(this.ModelStateToMessage());
            }
            else
            {
                result = await this.RegisterUser(userModel);
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>
        /// Registration result message
        /// </returns>
        public async Task<ResultMessage<bool>> RegisterUser(UserModel userModel)
        {
            var result = new ResultMessage<bool>();

            IdentityResult regResult = null;
            try
            {
                regResult = await this.authRepository.RegisterUser(userModel);
                result.Item = true;
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("Register: " + ex.Message);
                this.ModelState.AddModelError("Exception", ex.Message);
            }

            result.Messages.AddRange(regResult.ToMessages());

            return result;
        }

        /// <summary>
        /// Chane user password
        /// </summary>
        /// <param name="model">change password model</param>
        /// <returns>message for result</returns>
        [Authorize(Roles = RoleName.TestAdminRole)]
        [Route("change-password")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            var result = new ResultMessage<bool>();

            if (!this.ModelState.IsValid)
            {
                result.Messages.AddRange(this.ModelStateToMessage());
                return this.CreateCustomResponse(result);
            }

            try
            {
                result = await this.authRepository.ChangePassword(this.UserId, model.CurrentPassword, model.NewPassword);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("ChangePassword: " + ex.Message);
                this.ModelState.AddModelError("Exception", ex.Message);
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Chane user password
        /// </summary>
        /// <param name="model">change password model</param>
        /// <returns>message for result</returns>
        [Authorize]
        [Route("reset-password")]
        [HttpPost]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordModel model)
        {
            var result = new ResultMessage<bool>();

            if (!this.ModelState.IsValid)
            {
                result.Messages.AddRange(this.ModelStateToMessage());
                return this.CreateCustomResponse(result);
            }

            try
            {
                result = await this.authRepository.ResetPassword(model.UserId, model.NewPassword);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("New Password: " + ex.Message);
                this.ModelState.AddModelError("Exception", ex.Message);
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.authRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
