// ---------------------------------------------------------------------------------------------------
// <copyright file="StructureMapWebApiDependencyScope.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The StructureMapWebApiDependencyScope class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.DependencyResolution
{
    using System.Web.Http.Dependencies;

    using StructureMap;

    /// <summary>
    /// The structure map web api dependency scope.
    /// </summary>
    public class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapWebApiDependencyScope" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapWebApiDependencyScope(IContainer container)
            : base(container)
        {
        }
    }
}