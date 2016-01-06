// ---------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperMapper.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The AutoMapperMapper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Mapper
{
    /// <summary>
    /// The auto mapper mapper.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    public class AutoMapperMapper<TSource, TDestination> : MapperBase<TSource, TDestination>
    {
        /// <summary>
        /// The get destination instance.
        /// </summary>
        /// <returns>
        /// The <see cref="TDestination"/>.
        /// </returns>
        protected override TDestination GetDestinationInstance()
        {
            return default(TDestination);
        }

        /// <summary>
        /// The on map.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        protected override void OnMap(TSource source, ref TDestination destination)
        {
            destination = AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}
