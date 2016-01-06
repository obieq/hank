// ---------------------------------------------------------------------------------------------------
// <copyright file="WebsiteUrl.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-18</date>
// <summary>
//     The WebsiteUrl class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    /// <summary>
    /// The WebsiteUrl class
    /// </summary>
    public class WebsiteUrl
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
