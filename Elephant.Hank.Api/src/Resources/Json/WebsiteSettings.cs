// ---------------------------------------------------------------------------------------------------
// <copyright file="WebsiteSettings.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-09</date>
// <summary>
//     The WebsiteSettings class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    /// <summary>
    /// The WebsiteSettings class
    /// </summary>
    public class WebsiteSettings
    {
        /// <summary>
        /// Gets or sets the selenium address.
        /// </summary>
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
