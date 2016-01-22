// ---------------------------------------------------------------------------------------------------
// <copyright file="TokenProvider.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The TokenProvider class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.CustomIdentity
{
    using System;
    using System.Threading.Tasks;

    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Extensions;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The TokenProvider class
    /// </summary>
    public class TokenProvider : IUserTokenProvider<CustomUser, long>
    {
        /// <summary>
        /// The reset password
        /// </summary>
        public const string ResetPassword = "ResetPassword";

        /// <summary>
        /// The email confirmation
        /// </summary>
        public const string EmailConfirmation = "Confirmation";
        
        /// <summary>
        /// Generates the asynchronous.
        /// </summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <returns>Token for reset</returns>
        public async Task<string> GenerateAsync(string purpose, UserManager<CustomUser, long> manager, CustomUser user)
        {
            Guid resetToken = Guid.NewGuid();

            if (purpose.EqualsIgnoreCase(ResetPassword))
            {
                user.ResetPasswordToken = resetToken.ToString();
                user.ResetPasswordSentAt = DateTime.Now;
                await manager.UpdateAsync(user);
            }
            else if (purpose.EqualsIgnoreCase(EmailConfirmation))
            {
                user.EmailConfirmationToken = resetToken.ToString();
                user.EmailConfirmationSentAt = DateTime.Now;
                await manager.UpdateAsync(user);
            }

            return resetToken.ToString();
        }

        /// <summary>
        /// Determines whether [is valid provider for user asynchronous] [the specified manager].
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <returns>Status as bool</returns>
        /// <exception cref="System.ArgumentNullException">returns argument exception</exception>
        public Task<bool> IsValidProviderForUserAsync(UserManager<CustomUser, long> manager, CustomUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException();
            }

            return Task.FromResult(manager.SupportsUserPassword);
        }

        /// <summary>
        /// Notifies the asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <returns>Task promise</returns>
        public Task NotifyAsync(string token, UserManager<CustomUser, long> manager, CustomUser user)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="token">The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">The user.</param>
        /// <returns>Token validation</returns>
        public Task<bool> ValidateAsync(string purpose, string token, UserManager<CustomUser, long> manager, CustomUser user)
        {
            bool result = false;

            if (purpose.EqualsIgnoreCase(ResetPassword))
            {
                result = user.ResetPasswordSentAt.HasValue
                             && DateTime.Now.Subtract(user.ResetPasswordSentAt.Value).TotalHours < AppSettings.Get(ConfigConstants.TokenExpirationTimeHours, 6)
                             && user.ResetPasswordToken == token;
            }
            else if (purpose.EqualsIgnoreCase(EmailConfirmation))
            {
                result = user.EmailConfirmationSentAt.HasValue
                             && DateTime.Now.Subtract(user.EmailConfirmationSentAt.Value).TotalHours < AppSettings.Get(ConfigConstants.TokenExpirationTimeHours, 6)
                             && user.EmailConfirmationToken == token;
            }

            return Task.FromResult(result);
        }
    }
}