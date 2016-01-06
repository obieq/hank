// ---------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The WebApiConfig class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api
{
    using System.Web.Http;

    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.Resources.Constants;

    using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
    using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;

    /// <summary>
    /// The WebApiConfig class
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.MessageHandlers.Insert(0, new ServerCompressionHandler(AppSettings.Get(ConfigConstants.ContentSizeThresholdInBytes, 2048), new GZipCompressor(), new DeflateCompressor()));
        }
    }
}
