// ---------------------------------------------------------------------------------------------------
// <copyright file="StructureMapDependencyScope.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The StructureMapDependencyScope class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.DependencyResolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Microsoft.Practices.ServiceLocation;

    using StructureMap;

    /// <summary>
    /// The structure map dependency scope.
    /// </summary>
    public class StructureMapDependencyScope : ServiceLocatorImplBase
    {
        #region Constants and Fields

        /// <summary>
        /// The nested container key
        /// </summary>
        private const string NestedContainerKey = "Nested.Container.Key";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapDependencyScope"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.Container = container;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public IContainer Container { get; set; }

        /// <summary>
        /// Gets or sets the current nested container.
        /// </summary>
        /// <value>
        /// The current nested container.
        /// </value>
        public IContainer CurrentNestedContainer
        {
            get
            {
                return (IContainer)this.HttpContext.Items[NestedContainerKey];
            }

            set
            {
                this.HttpContext.Items[NestedContainerKey] = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        /// <value>
        /// The HTTP context.
        /// </value>
        private HttpContextBase HttpContext
        {
            get
            {
                var ctx = this.Container.TryGetInstance<HttpContextBase>();
                
                return ctx ?? new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates the nested container.
        /// </summary>
        public void CreateNestedContainer()
        {
            if (this.CurrentNestedContainer != null)
            {
                return;
            }

            this.CurrentNestedContainer = this.Container.GetNestedContainer();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            if (this.CurrentNestedContainer != null)
            {
                this.CurrentNestedContainer.Dispose();
            }

            this.Container.Dispose();
        }

        /// <summary>
        /// Disposes the nested container.
        /// </summary>
        public void DisposeNestedContainer()
        {
            if (this.CurrentNestedContainer != null)
            {
                this.CurrentNestedContainer.Dispose();
            }
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        /// The <see><cref>IEnumerable</cref></see>.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.DoGetAllInstances(serviceType);
        }

        #endregion

        #region Methods

        /// <summary>
        /// When implemented by inheriting classes, this method will do the actual work of
        /// resolving all the requested service instances.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>
        /// Sequence of service instance objects.
        /// </returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (this.CurrentNestedContainer ?? this.Container).GetAllInstances(serviceType).Cast<object>();
        }

        /// <summary>
        /// When implemented by inheriting classes, this method will do the actual work of resolving
        /// the requested service instance.
        /// </summary>
        /// <param name="serviceType">Type of instance requested.</param>
        /// <param name="key">Name of registered service you want. May be null.</param>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            IContainer container = this.CurrentNestedContainer ?? this.Container;

            if (string.IsNullOrEmpty(key))
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                           ? container.TryGetInstance(serviceType)
                           : container.GetInstance(serviceType);
            }

            return container.GetInstance(serviceType, key);
        }

        #endregion
    }
}