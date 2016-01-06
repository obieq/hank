// ---------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-26</date>
// <summary>
//     The DateTimeExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.Extensions
{
    using System;

    /// <summary>
    /// The DateTimeExtensions class
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// To the date format dd-MM-yyyy.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Formated date</returns>
        public static string ToDateApiFormat(this DateTime value)
        {
            return value.ToString("MM-dd-yyyy");
        }

        /// <summary>
        /// To the date est format.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// To Est time format
        /// </returns>
        public static string ToDateEstFormat(this DateTime? date)
        {
            string retValue = null;
            if (date.HasValue)
            {
                var value = date.Value;

                var utcdate = value.ToUniversalTime();

                var estdate = TimeZoneInfo.ConvertTimeFromUtc(utcdate, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                retValue = estdate.ToString("MM-dd-yyyy HH:mm:ss tt");
            }
            return retValue;
        }
    }
}
