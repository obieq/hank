// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthRepository.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-11-10</date>
// <summary>
//     The AuthRepository class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Framework.TestDataServices;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.CustomIdentity;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The AuthRepository class
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly IRepository<TblAuthClients> client;

        /// <summary>
        /// The refresh token
        /// </summary>
        private readonly IRepository<TblRefreshAuthTokens> refreshToken;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly CustomUserManager userManager;

        /// <summary>
        /// The mapper factory.
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The roles
        /// </summary>
        private readonly IRepository<CustomRole> roles;

        /// <summary>
        /// The client
        /// </summary>
        private readonly UserProfileService userProfileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthRepository" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="userProfileService">The user profile service.</param>
        /// <param name="mapperFactory">The mapper factory.</param>
        public AuthRepository(
            IRepository<TblAuthClients> client,
            IRepository<TblRefreshAuthTokens> refreshToken,
            CustomUserManager userManager,
            IRepository<CustomRole> roles,
            UserProfileService userProfileService,
            IMapperFactory mapperFactory)
        {
            this.client = client;
            this.refreshToken = refreshToken;
            this.userManager = userManager;
            this.roles = roles;
            this.userProfileService = userProfileService;
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Role name</returns>
        public string GetRoleName(long roleId)
        {
            var result = this.roles.Find(x => x.Id == roleId).FirstOrDefault();

            return result == null ? null : result.Name;
        }

        /// <summary>
        /// The change activation status.
        /// </summary>
        /// <param name="activationId">The activation id.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public async Task<bool> ChangeActivationStatus(Guid activationId, bool status = true)
        {
            var existingUser = this.userManager.Users.FirstOrDefault(x => x.ActivationId == activationId.ToString());

            if (existingUser != null && existingUser.IsActive != status)
            {
                existingUser.IsActive = status;
                if (status)
                {
                    existingUser.ActivatedOn = DateTime.Now;
                }

                return this.userManager.Update(existingUser) != null;
            }

            return false;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            CustomUser user = new CustomUser
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserName = userModel.UserName,
                Email = userModel.UserName,
                IsActive = true
            };

            var existingUser = this.userManager.FindByName(userModel.UserName);

            if (existingUser != null)
            {
                var errorResult = new IdentityResult(new[] { string.Format("User name {0} already exists!", userModel.UserName) });
                return errorResult;
            }

            var result = await this.userManager.CreateAsync(user, userModel.Password);

            if (result != null && result.Succeeded)
            {
                this.userManager.AddToRole(user.Id, RoleName.TestUserRole);
                var newUser = this.mapperFactory.GetMapper<CustomUser, CustomUserDto>().Map(user);
                newUser.RoleName = RoleName.TestUserRole;
                this.userProfileService.SaveOrUpdate(new TblUserProfileDto { UserId = user.Id, CreatedBy = user.CreatedBy, ModifiedBy = user.ModifiedBy, IsDeleted = false, ModifiedOn = DateTime.Now, Designation = userModel.Designation }, user.Id);
            }

            return result;
        }

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<CustomUserDto> FindUser(string userName, string password)
        {
            try
            {
                var user = await this.userManager.FindAsync(userName, password);
                if (user == null)
                {
                    return null;
                }

                return this.mapperFactory.GetMapper<CustomUser, CustomUserDto>().Map(user);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Finds the name of the user by.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<CustomUserDto> FindUserByName(string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            return this.mapperFactory.GetMapper<CustomUser, CustomUserDto>().Map(user);
        }

        /// <summary>
        /// The find user by id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task<CustomUserDto> FindUserById(long userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return this.mapperFactory.GetMapper<CustomUser, CustomUserDto>().Map(user);
        }

        /// <summary>
        /// Finds the client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Identity object</returns>
        public ClientDto FindClient(string clientId)
        {
            var clientObj = this.client.Find(x => x.ClientId == clientId).FirstOrDefault();

            if (clientObj != null)
            {
                return this.mapperFactory.GetMapper<TblAuthClients, ClientDto>().Map(clientObj);
            }

            return null;
        }

        /// <summary>
        /// Adds the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<bool> AddRefreshToken(RefreshTokenDto token)
        {
            var existingToken = this.refreshToken.Find(r => r.Subject == token.Subject && r.ClientId == token.ClientId && !r.IsDeleted).FirstOrDefault();

            if (existingToken != null)
            {
                await this.RemoveRefreshToken(existingToken.Token);
            }

            var newToken = this.mapperFactory.GetMapper<RefreshTokenDto, TblRefreshAuthTokens>().Map(token);

            this.refreshToken.Insert(newToken);

            return this.refreshToken.Commit() > 0;
        }

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshTokenObj = this.refreshToken.Find(x => x.Token == refreshTokenId).FirstOrDefault();

            if (refreshTokenObj != null)
            {
                refreshTokenObj.IsDeleted = true;

                return this.refreshToken.Commit() > 0;
            }

            return false;
        }

        /// <summary>
        /// Finds the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        public async Task<RefreshTokenDto> FindRefreshToken(string refreshTokenId)
        {
            var refreshTokenObj = this.refreshToken.Find(x => x.Token == refreshTokenId && !x.IsDeleted).FirstOrDefault();

            if (refreshTokenObj != null)
            {
                return this.mapperFactory.GetMapper<TblRefreshAuthTokens, RefreshTokenDto>().Map(refreshTokenObj);
            }

            return null;
        }

        /// <summary>
        /// Gets all refresh tokens.
        /// </summary>
        /// <returns>
        /// Identity object
        /// </returns>
        public List<RefreshTokenDto> GetAllRefreshTokens()
        {
            var list = this.refreshToken.Find(x => !x.IsDeleted).ToList();
            var mapper = this.mapperFactory.GetMapper<TblRefreshAuthTokens, RefreshTokenDto>();
            return list.Select(mapper.Map).ToList();
        }

        /// <summary>
        /// Change the user password
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <param name="currentPassword">user current password</param>
        /// <param name="newPassword">user new password</param>
        /// <returns>
        /// IdentityResult object
        /// </returns>
        public async Task<ResultMessage<bool>> ChangePassword(long userId, string currentPassword, string newPassword)
        {
            var result = new ResultMessage<bool>();

            var data = await this.userManager.ChangePasswordAsync(userId, currentPassword, newPassword);

            result.Item = data.Succeeded;

            if (!data.Succeeded)
            {
                result.Messages.AddRange(data.ToMessages());
            }

            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.refreshToken.Dispose();
            this.client.Dispose();
        }

        /// <summary>
        /// Get List of all users in the system
        /// </summary>
        /// <returns>returns list of users</returns>
        public ResultMessage<IEnumerable<CustomUserDto>> GetAllUsers()
        {
            var result = new ResultMessage<IEnumerable<CustomUserDto>>();

            var entities = this.userManager.Users.ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<CustomUser, CustomUserDto>();
                result.Item = entities.Select(mapper.Map);
            }

            return result;
        }

        /// <summary>
        /// Activate and deactivate the user
        /// </summary>
        /// <param name="userId">user to be activated/deactivated</param>
        /// <param name="enabled">true for activating and false for deactivating</param>
        /// <returns>Identity result with status of the operation</returns>
        public async Task<IdentityResult> SetUserLockOut(long userId, bool enabled)
        {
            return await this.userManager.SetLockoutEnabledAsync(userId, enabled);
        }

        /// <summary>
        /// Reset password for the user
        /// </summary>
        /// <param name="userId">id of the user for which u want to reset the password</param>
        /// <param name="newPassword">new password for the user</param>
        /// <returns>Identity result with status of the operation</returns>
        public async Task<ResultMessage<bool>> ResetPassword(long userId, string newPassword)
        {
            var result = new ResultMessage<bool>();

            var token = await this.userManager.GeneratePasswordResetTokenAsync(userId);

            var data = await this.userManager.ResetPasswordAsync(userId, token, newPassword);

            result.Item = data.Succeeded;

            if (!data.Succeeded)
            {
                result.Messages.AddRange(data.ToMessages());
            }

            return result;
        }
    }
}