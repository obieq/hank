// ---------------------------------------------------------------------------------------------------
// <copyright file="StructureMapWebApiDependencyResolver.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The StructureMapWebApiDependencyResolver class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.DependencyResolution
{
    using System.Web.Http.Dependencies;
    using StructureMap;

    /// <summary>
    /// The structure map dependency resolver.
    /// </summary>
    public class StructureMapWebApiDependencyResolver : StructureMapWebApiDependencyScope, IDependencyResolver
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapWebApiDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public StructureMapWebApiDependencyResolver(IContainer container)
            : base(container)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The begin scope.
        /// </summary>
        /// <returns>
        /// The System.Web.Http.Dependencies.IDependencyScope.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            IContainer child = this.Container.GetNestedContainer();
            return new StructureMapWebApiDependencyResolver(child);
        }

        #endregion
    }
}