﻿// ---------------------------------------------------------------------------------------------------
// <copyright file="TblBrowsers.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-09</date>
// <summary>
//     The TblBrowsers class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblBrowsers class
    /// </summary>
    public class TblBrowsers : BaseTable
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the configuration.
        /// </summary>
        [Required]
        public string ConfigName { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        [Required]
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
