// ---------------------------------------------------------------------------------------------------
// <copyright file="ICacheProvider.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The ICacheProvider class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.Services
{
    using System;

    /// <summary>
    /// The ICacheProvider interface
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Sets the specified cache key.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="cacheItem">The cache item.</param>
        /// <param name="expMinutes">The expiration minutes.</param>
        void Set<T>(string cacheKey, T cacheItem, int expMinutes = 0);

        /// <summary>
        /// Sets the specified cache key.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="getData">The get data.</param>
        /// <param name="expMinutes">The exp minutes.</param>
        void Set<T>(string cacheKey, Func<T> getData, int expMinutes = 0);

        /// <summary>
        /// Tries the get and set.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="getData">The get data.</param>
        /// <param name="returnData">The return data.</param>
        /// <param name="expMinutes">The exp minutes.</param>
        /// <returns>
        /// Status of value exists or not
        /// </returns>
        bool TryGetAndSet<T>(string cacheKey, Func<T> getData, out T returnData, int expMinutes = 0);

        /// <summary>
        /// Tries the get.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="returnItem">The return item.</param>
        /// <returns>Status of value exists or not</returns>
        bool TryGet<T>(string cacheKey, out T returnItem);
    }
}
