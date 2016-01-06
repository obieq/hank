// ---------------------------------------------------------------------------------------------------
// <copyright file="DateExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-04</date>
// <summary>
//     The DateExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Extensions
{
    using System;

    /// <summary>
    /// The DateExtensions class
    /// </summary>
    public static class DateExtensions
    {
        /// <summary>
        /// To the name of the group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Group name</returns>
        public static string ToGroupName(this DateTime value)
        {
            return value.ToString("dd-MM-yyyy-HH-mm-ss");
        }
    }
}