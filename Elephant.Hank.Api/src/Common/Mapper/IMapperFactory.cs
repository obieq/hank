// ---------------------------------------------------------------------------------------------------
// <copyright file="IMapperFactory.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The IMapperFactory interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.Mapper
{
    /// <summary>
    /// The MapperFactory interface.
    /// </summary>
    public interface IMapperFactory
    {
        /// <summary>
        /// The get mapper.
        /// </summary>
        /// <typeparam name="TSource">The source object.</typeparam>
        /// <typeparam name="TDestination">The destination object.</typeparam>
        /// <returns>
        /// The Mapper.
        /// </returns>
        IMapper<TSource, TDestination> GetMapper<TSource, TDestination>();
    }
}
