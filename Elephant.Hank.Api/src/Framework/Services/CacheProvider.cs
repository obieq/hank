// ---------------------------------------------------------------------------------------------------
// <copyright file="CacheProvider.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The CacheProvider class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Services
{
    using System;
    using System.Web;
    using System.Web.Caching;

    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.Common.Services;
    using Elephant.Hank.Resources.Constants;

    using CacheItemPriority = System.Web.Caching.CacheItemPriority;

    /// <summary>
    /// The CacheProvider class
    /// </summary>
    public class CacheProvider : ICacheProvider
    {
        /// <summary>
        /// The expiration minutes
        /// </summary>
        private readonly int expirationMinutes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheProvider"/> class.
        /// </summary>
        public CacheProvider()
        {
            this.expirationMinutes = AppSettings.Get(ConfigConstants.CacheExpMinutes, 10);
        }

        /// <summary>
        /// Sets the specified cache key.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="cacheItem">The cache item.</param>
        /// <param name="expMinutes">The expiration minutes.</param>
        public void Set<T>(string cacheKey, T cacheItem, int expMinutes = 0)
        {
            expMinutes = expMinutes <= 0 ? this.expirationMinutes : expMinutes;

            HttpContext.Current.Cache.Insert(cacheKey, cacheItem, null, DateTime.Now.AddMinutes(expMinutes), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// Sets the specified cache key.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="getData">The get data.</param>
        /// <param name="expMinutes">The exp minutes.</param>
        public void Set<T>(string cacheKey, Func<T> getData, int expMinutes = 0)
        {
            this.Set(cacheKey, getData(), expMinutes);
        }

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
        public bool TryGetAndSet<T>(string cacheKey, Func<T> getData, out T returnData, int expMinutes = 0)
        {
            if (this.TryGet(cacheKey, out returnData))
            {
                return true;
            }

            returnData = getData();
            this.Set(cacheKey, returnData, expMinutes);
            return returnData != null;
        }

        /// <summary>
        /// Tries the get.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="returnItem">The return item.</param>
        /// <returns>Status of value exists or not</returns>
        public bool TryGet<T>(string cacheKey, out T returnItem)
        {
            returnItem = (T)HttpContext.Current.Cache[cacheKey];
            return returnItem != null;
        }
    }
}
