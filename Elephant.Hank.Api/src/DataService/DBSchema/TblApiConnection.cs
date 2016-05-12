// ---------------------------------------------------------------------------------------------------
// <copyright file="TblApiConnection.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The TblApiConnection class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Models;

    using Newtonsoft.Json;

    /// <summary>
    /// The tbl api connection.
    /// </summary>
    public class TblApiConnection : BaseTable
    {
        /// <summary>
        /// Gets or sets EnvironmentIds
        /// </summary>
        public long EnvironmentId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the base url.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public string HeadersJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.Headers);
            }

            set
            {
                this.Headers = string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<List<NameValuePair>>(value);
            }
        }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        [NotMapped]
        public IEnumerable<NameValuePair> Headers { get; set; }

        /// <summary>
        /// Gets or sets the Website
        /// </summary>
        [ForeignKey("WebsiteId")]
        public virtual TblWebsite Website { get; set; }

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        [ForeignKey("EnvironmentId")]
        public virtual TblEnvironment Environment { get; set; }

        /// <summary>
        /// Gets or sets the DataBaseCategories
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual TblApiCategories ApiCategories { get; set; }
    }
}
