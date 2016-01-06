// ---------------------------------------------------------------------------------------------------
// <copyright file="UserProfileSettings.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The UserProfileSettings class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The UserProfileSettings class
    /// </summary>
    public class UserProfileSettings
    {
        /// <summary>
        /// Gets or sets the selenium address.
        /// </summary>
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$")]
        public string SeleniumAddress { get; set; }

        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        public long[] Browsers { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotOnUrlChanged
        /// </summary>
        public long TakeScreenShotOnUrlChanged { get; set; }
    }
}
