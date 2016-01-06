// ---------------------------------------------------------------------------------------------------
// <copyright file="TestQueueSettings.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-16</date>
// <summary>
//     The TestQueueSettings class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The TestQueueSettings class
    /// </summary>
    public class TestQueueSettings
    {
        /// <summary>
        /// Gets or sets the UrlId
        /// </summary>
        public int UrlId { get; set; }

        /// <summary>
        /// Gets or sets the selenium address.
        /// </summary>
        public string SeleniumAddress { get; set; }

        /// <summary>
        /// Gets or sets the base API URL.
        /// </summary>
        public string BaseApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        public List<long> Browsers { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotOnUrlChanged
        /// </summary>
        public long? TakeScreenShotOnUrlChanged { get; set; }
    }
}
