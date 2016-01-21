// ---------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The Startup class
// </summary>
// ---------------------------------------------------------------------------------------------------

using Elephant.Hank.Api;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Elephant.Hank.Api
{
    using System;
    using System.Data.Entity;
    using System.Web.Http;

    using Elephant.Hank.Api.DependencyResolution;
    using Elephant.Hank.Api.Providers;
    using Elephant.Hank.Api.Security;
    using Elephant.Hank.DataService;
    using Elephant.Hank.Framework.Mapper;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    /// <summary>
    /// The Start-up class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            app.Use(typeof(AuthorizationMiddleware));

            this.ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            config.DependencyResolver = new StructureMapWebApiDependencyResolver(container);

            app.UseWebApi(config);
            AutoMapperBootstraper.Initialize();

            Database.SetInitializer<AuthContext>(null);
        }

        /// <summary>
        /// Configures the o authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions authServerOptions = new OAuthAuthorizationServerOptions 
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(authServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}