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

    using Newtonsoft.Json;

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
        /// Gets or sets a value indicating whether this instance is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the custom URL to test.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CustomUrlToTest { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the selenium address.
        /// </summary>
        [Required]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SeleniumAddress { get; set; }
       
        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        [Required]
        public long[] Browsers { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotOnUrlChanged
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? TakeScreenShotOnUrlChanged { get; set; }

        /// <summary>
        /// Gets or sets the repeat times.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? RepeatTimes { get; set; }
    }
}
