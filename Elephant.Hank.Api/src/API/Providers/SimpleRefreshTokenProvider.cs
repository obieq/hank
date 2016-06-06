// ---------------------------------------------------------------------------------------------------
// <copyright file="SimpleRefreshTokenProvider.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The SimpleRefreshTokenProvider class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Providers
{
    using System;
    using System.Threading.Tasks;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Extensions;

    using Microsoft.Owin.Security.Infrastructure;

    /// <summary>
    /// The SimpleRefreshTokenProvider class
    /// </summary>
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider, IDisposable
    {
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshTokenDto
                            {
                                Token = DbHelper.GetHash(refreshTokenId),
                                ClientId = clientid,
                                Subject = context.Ticket.Identity.Name,
                                IssuedUtc = DateTime.UtcNow,
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(refreshTokenLifeTime.ToInt64())
                            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var authRepo = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IAuthRepository>();
            var result = await authRepo.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        /// <summary>
        /// Receives the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="Task" />.
        /// </returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = DbHelper.GetHash(context.Token);

            var authRepo = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IAuthRepository>();
            var refreshToken = await authRepo.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                // Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await authRepo.RemoveRefreshToken(hashedTokenId);
            }
        }

        /// <summary>
        /// Creates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receives the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}