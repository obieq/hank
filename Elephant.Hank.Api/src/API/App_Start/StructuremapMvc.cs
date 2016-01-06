// ---------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The StructuremapMvc class
// </summary>
// ---------------------------------------------------------------------------------------------------

using Elephant.Hank.Api;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace Elephant.Hank.Api
{
    using System.Web.Mvc;

    using Elephant.Hank.Api.DependencyResolution;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using StructureMap;

    /// <summary>
    /// The StructuremapMvc class
    /// </summary>
    public static class StructuremapMvc
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the structure map dependency scope.
        /// </summary>
        /// <value>
        /// The structure map dependency scope.
        /// </value>
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Ends this instance.
        /// </summary>
        public static void End()
        {
            StructureMapDependencyScope.Dispose();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public static void Start()
        {
            IContainer container = IoC.Initialize();
            StructureMapDependencyScope = new StructureMapDependencyScope(container);
            DependencyResolver.SetResolver(StructureMapDependencyScope);
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
        }

        #endregion
    }
}