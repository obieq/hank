// ---------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The IoC class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.DependencyResolution
{
    using StructureMap;

    /// <summary>
    /// The IoC class
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="IContainer"/>.
        /// </returns>
        public static IContainer Initialize()
        {
            return new Container(c => c.AddRegistry<DefaultRegistry>());
        }
    }
}