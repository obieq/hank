// ---------------------------------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The PaymentController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.Helper
{
    using System;
    using System.Configuration;

    /// <summary>
    /// All App Settings required
    /// </summary>
    public sealed class AppSettings
    {
        /// <summary>
        /// Gets the specified item key.
        /// </summary>
        /// <typeparam name="T">Type of property</typeparam>
        /// <param name="itemKey">The item key.</param>
        /// <param name="language">The language.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Property value</returns>
        public static T Get<T>(string itemKey, string language, T defaultValue)
        {
            var ret = Get(itemKey, defaultValue);
            ret = Get(itemKey + "_" + language, ret);
            return ret;
        }

        /// <summary>
        /// Gets the specified item key.
        /// </summary>
        /// <typeparam name="T">Type of property</typeparam>
        /// <param name="itemKey">The item key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Property value</returns>
        public static T Get<T>(string itemKey, T defaultValue)
        {
            T data;
            try
            {
                string setting = ConfigurationManager.AppSettings[itemKey];
                if (setting == null)
                {
                    data = defaultValue;
                }
                else
                {
                    data = (T)Convert.ChangeType(setting, typeof(T));
                }
            }
            catch
            {
                data = defaultValue;
            }

            return data;
        }

        /// <summary>
        /// Gets the specified item key.
        /// </summary>
        /// <typeparam name="T">Type of property</typeparam>
        /// <param name="itemKey">The item key.</param>
        /// <returns>Property value</returns>
        public static T Get<T>(string itemKey)
        {
            T data;
            try
            {
                string setting = ConfigurationManager.AppSettings[itemKey];
                if (setting == null)
                {
                    data = default(T);
                }
                else
                {
                    data = (T)Convert.ChangeType(setting, typeof(T));
                }
            }
            catch
            {
                data = default(T);
            }

            return data;
        }
    }
}