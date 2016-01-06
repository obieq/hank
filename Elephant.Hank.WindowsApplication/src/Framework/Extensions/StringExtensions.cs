// ---------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The StringExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Extensions
{
    using System;

    /// <summary>
    /// The StringExtensions class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// To the int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>int value</returns>
        public static int ToInt(this string value)
        {
            int result;
            Int32.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// To the int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>int value</returns>
        public static int ToInt(this string value, int defaultValue)
        {
            int result;

            if (!Int32.TryParse(value, out result))
            {
                result = defaultValue;
            }

            return result;
        }
    }
}
