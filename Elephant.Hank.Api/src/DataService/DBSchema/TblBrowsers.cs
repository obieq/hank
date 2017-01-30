// ---------------------------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Models;

    using Newtonsoft.Json;

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
        /// Gets or sets a value indicating whether this instance is mobile.
        /// </summary>
        public bool IsMobile { get; set; }

        /// <summary>
        /// Gets or sets the maximum instances.
        /// </summary>
        public int MaxInstances { get; set; }

        /// <summary>
        /// Gets or sets the property json.
        /// </summary>
        public string PropertyJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.Properties);
            }

            set
            {
                this.Properties = string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<List<NameValuePair>>(value);
            }
        }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [NotMapped]
        public IEnumerable<NameValuePair> Properties { get; set; }
    }
}
