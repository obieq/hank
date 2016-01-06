// ---------------------------------------------------------------------------------------------------
// <copyright file="StructuremapWebApi.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The StructuremapWebApi class
// </summary>
// ---------------------------------------------------------------------------------------------------

// [assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Elephant.Hank.Api.App_Start.StructuremapWebApi), "Start")]

namespace Elephant.Hank.Api
{
    using System.Web.Http;

    using Elephant.Hank.Api.DependencyResolution;

    /// <summary>
    /// The StructuremapWebApi class
    /// </summary>
    public static class StructuremapWebApi
    {
        /// <summary>
        /// The start.
        /// </summary>
        public static void Start()
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
        }
    }
}