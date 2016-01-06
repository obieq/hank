// ---------------------------------------------------------------------------------------------------
// <copyright file="IAuthRepository.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The IAuthRepository interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.CustomIdentity;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    using Microsoft.AspNet.Identity;    

    /// <summary>
    /// The IAuthRepository interface
    /// </summary>
    public interface IAuthRepository : IDisposable
    {
        /// <summary>
        /// The change activation status.
        /// </summary>
        /// <param name="activationId">The activation id.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<bool> ChangeActivationStatus(Guid activationId, bool status = true);

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>Identity object</returns>
        Task<IdentityResult> RegisterUser(UserModel userModel);

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Identity object</returns>
        Task<CustomUserDto> FindUser(string userName, string password);

        /// <summary>
        /// The find user by id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        Task<CustomUserDto> FindUserById(long userId);

        /// <summary>
        /// Finds the client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Identity object</returns>
        ClientDto FindClient(string clientId);

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        /// <param name="roleId">The role identifier.</param>
        /// <returns>Role name</returns>
        string GetRoleName(long roleId);

        /// <summary>
        /// Adds the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Identity object</returns>
        Task<bool> AddRefreshToken(RefreshTokenDto token);

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>
        /// Identity object
        /// </returns>
        Task<bool> RemoveRefreshToken(string refreshTokenId);

        /// <summary>
        /// Finds the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>Identity object</returns>
        Task<RefreshTokenDto> FindRefreshToken(string refreshTokenId);

        /// <summary>
        /// Gets all refresh tokens.
        /// </summary>
        /// <returns>Identity object</returns>
        List<RefreshTokenDto> GetAllRefreshTokens();

        /// <summary>
        /// Change the user passsword
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <param name="currentPassword">user current password</param>
        /// <param name="newPassword">user new password</param>
        /// <returns>Result for password change </returns>
        Task<IdentityResult> ChangePassword(long userId, string currentPassword, string newPassword);

        /// <summary>
        /// Get List of all users in the system
        /// </summary>
        /// <returns>resturns list of users</returns>
        ResultMessage<IEnumerable<CustomUserDto>> GetAllUsers();

        /// <summary>
        /// Activate and deactivate the user
        /// </summary>
        /// <param name="userId">user to be activated/deactivated</param>
        /// <param name="enabled">true for activating and false for deactivating</param>
        /// <returns>Identity result with status of the operation</returns>
        Task<IdentityResult> SetUserLockOut(long userId, bool enabled);

        /// <summary>
        /// Reset password for the user
        /// </summary>
        /// <param name="userId">id of the user for which u want to reset the password</param>
        /// <param name="newPassword">new password for the user</param>
        /// <returns>Identity result with status of the operation</returns>
        Task<IdentityResult> ResetPassword(long userId, string newPassword);
    }
}