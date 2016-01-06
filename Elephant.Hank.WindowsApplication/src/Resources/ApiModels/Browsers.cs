// ---------------------------------------------------------------------------------------------------
// <copyright file="Browsers.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-10</date>
// <summary>
//     The Browsers class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    /// <summary>
    /// The Browsers class
    /// </summary>
    public class Browsers
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the configuration.
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [shard test files].
        /// </summary>
        public bool ShardTestFiles { get; set; }

        /// <summary>
        /// Gets or sets the maximum instances.
        /// </summary>
        public int MaxInstances { get; set; }
    }
}