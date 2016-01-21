// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationMiddleware.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The AuthorizationMiddleware class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Security
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.Services;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Json;

    using Microsoft.Owin;

    /// <summary>
    /// The AuthorizationMiddleware class
    /// </summary>
    public class AuthorizationMiddleware : OwinMiddleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationMiddleware" /> class.
        /// </summary>
        /// <param name="next">next pointer</param>
        public AuthorizationMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        #region Overrides of OwinMiddleware

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context">request context</param>
        /// <returns>
        /// response context
        /// </returns>
        public override async Task Invoke(IOwinContext context)
        {
            await this.ProcessAuthLoading(context);

            if (context.Response.StatusCode != 401)
            {
                await this.Next.Invoke(context);
            }
        }

        #endregion

        /// <summary>
        /// Processes the authentication loading.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// Task promise
        /// </returns>
        private async Task ProcessAuthLoading(IOwinContext context)
        {
            var header = context.Request.Headers.FirstOrDefault(m => m.Key.ToLower() == "authorization");

            if (!string.IsNullOrWhiteSpace(header.Key))
            {
                string headerValue = header.Value.First().Trim();
                if (!headerValue.StartsWith("Bearer"))
                {
                    try
                    {
                        var cacheProvider = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<ICacheProvider>();

                        AuthToken authData;

                        if (!cacheProvider.TryGet(headerValue, out authData) || authData == null)
                        {
                            string[] credientials = Encoding.UTF8.GetString(Convert.FromBase64String(headerValue)).Split(':');

                            if (credientials.Length == 2)
                            {
                                string[] separatingChars = { "/api" };

                                var apiClient = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IApiClient>();
                                apiClient.BaseUrl = context.Request.Uri.AbsoluteUri.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries)[0];

                                var data = await apiClient.Post<AuthToken, string>("api/token", string.Format("grant_type=password&username={0}&password={1}", credientials[0], credientials[1]), "application/x-www-form-urlencoded");

                                if (data.IsError || data.Item == null || data.Item.AccessToken.IsBlank())
                                {
                                    context.Response.StatusCode = 401;
                                }
                                else
                                {
                                    authData = data.Item;
                                    int expMinutes = authData.ExpiresInSeconds <= 0 ? 0 : authData.ExpiresInSeconds / 60;
                                    cacheProvider.Set(headerValue, authData, expMinutes);
                                }
                            }
                        }

                        if (authData != null)
                        {
                            context.Request.Headers.Remove("Authorization");
                            context.Request.Headers.Add("Authorization", new[] { "Bearer" + " " + authData.AccessToken });
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<ILoggerService>();
                        logger.LogException("ProcessAuthLoading: " + ex.Message);
                    }
                }
            }
        }
    }
}