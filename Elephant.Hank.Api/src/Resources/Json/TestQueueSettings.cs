// ---------------------------------------------------------------------------------------------------
// <copyright file="TestQueueSettings.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-15</date>
// <summary>
//     The TestQueueSettings class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    using System.ComponentModel.DataAnnotations;

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
        [Required]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$")]
        public string SeleniumAddress { get; set; }
       
        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        [Required]
        public long[] Browsers { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotOnUrlChanged
        /// </summary>
        public long? TakeScreenShotOnUrlChanged { get; set; }
    }
}
