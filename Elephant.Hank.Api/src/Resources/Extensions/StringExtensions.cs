// ---------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-25</date>
// <summary>
//     The StringExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Extensions
{
    using System;
    using System.Globalization;

    /// <summary>
    /// The StringExtensions class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// To the date from mm-dd-yyyy.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Null able DateTime</returns>
        public static DateTime? ToDate(this string value)
        {
            if (value.IsBlank())
            {
                return null;
            }

            var ary = value.Split('-');

            if (ary.Length == 3)
            {
                return new DateTime(ary[2].ToInt32(), ary[0].ToInt32(), ary[1].ToInt32());
            }

            return null;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Int value</returns>
        public static int ToInt32(this string value)
        {
            return value.ToInt32(0);
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>int value</returns>
        public static int ToInt32(this string value, int defaultValue)
        {
            int retValue;

            if (!int.TryParse(value, out retValue))
            {
                retValue = defaultValue;
            }

            return retValue;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Long value</returns>
        public static long ToInt64(this string value)
        {
            long retValue;
            long.TryParse(value, out retValue);

            return retValue;
        }

        /// <summary>
        /// Determines whether the specified value is blank.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Is empty or not</returns>
        public static bool IsBlank(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Determines whether [is not blank] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string is blank or not</returns>
        public static bool IsNotBlank(this string value)
        {
            return !value.IsBlank();
        }

        /// <summary>
        /// Equals the ignore case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="matchTo">The match to.</param>
        /// <returns>Match two strings</returns>
        public static bool EqualsIgnoreCase(this string value, string matchTo)
        {
            return value.IsNotBlank() && matchTo.IsNotBlank() && value.Equals(matchTo, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// To the browser name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Extract browser name</returns>
        public static string ToBrowserName(this string value)
        {
            string retValue = null;

            if (value.IsNotBlank())
            {
                // as first name is Operating system
                retValue = value.Substring(value.IndexOf(" ", StringComparison.InvariantCultureIgnoreCase)).ToTitleCase();
            }

            return retValue;
        }

        /// <summary>
        /// To the name of the operating system.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>OS name</returns>
        public static string ToOperatingSystemName(this string value)
        {
            string retValue = null;

            if (value.IsNotBlank())
            {
                // as first name is Operating system
                retValue = value.Split(' ')[0].Trim().ToTitleCase();
            }

            return retValue;
        }

        /// <summary>
        /// To the execution slot.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Time slot</returns>
        public static string ToExecutionSlot(this string value)
        {
            string retValue = null;

            if (value.IsNotBlank())
            {
                var timeAry = value.Split('-');

                for (var i = 3; i < timeAry.Length; i++)
                {
                    retValue += timeAry[i] + ":";
                }

                retValue = retValue.Substring(0, retValue.Length - 1);
            }

            return retValue;
        }

        /// <summary>
        /// To the title case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Title cases string</returns>
        public static string ToTitleCase(this string value)
        {
            string retValue = null;

            if (value.IsNotBlank())
            {
                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

                retValue = myTI.ToTitleCase(value.Trim());
            }

            return retValue;
        }

        /// <summary>
        /// The to GUID.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Guid" />.
        /// </returns>
        public static Guid ToGuid(this string value)
        {
            Guid retValue = Guid.Empty;

            if (!string.IsNullOrWhiteSpace(value))
            {
                Guid.TryParse(value, out retValue);
            }

            return retValue;
        }
    }
}
