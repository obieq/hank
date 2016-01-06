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

namespace Elephant.Hank.WindowsApplication.Resources.Extensions
{
    using System;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// The StringExtensions class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// To the date.
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
                return new DateTime(ary[2].ToInt32(), ary[1].ToInt32(), ary[0].ToInt32());
            }

            return null;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Null able DateTime</returns>
        public static DateTime? ToDateTime(this string value)
        {
            DateTime? retValue = null;

            if (value.IsNotBlank())
            {
                DateTime outValue;

                if (DateTime.TryParse(value, out outValue))
                {
                    retValue = outValue;
                }
            }

            return retValue;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Int value</returns>
        public static int ToInt32(this string value)
        {
            int retValue;
            int.TryParse(value, out retValue);

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
        /// To the name of the thumb file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Thumb FileName</returns>
        public static string ToJpgThumbFileName(this string path)
        {
            return path.IsNotBlank()
                ? Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "-t200.jpg"
                : path;
        }

        /// <summary>
        /// To the name of the JPG image file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>JPEG Image FileName</returns>
        public static string ToJpgImageFileName(this string path)
        {
            return path.IsNotBlank()
                ? Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".jpg"
                : path;
        }
    }
}
