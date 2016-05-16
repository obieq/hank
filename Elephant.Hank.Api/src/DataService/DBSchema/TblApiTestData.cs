// ---------------------------------------------------------------------------------------------------
// <copyright file="TblApiTestData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-05-13</date>
// <summary>
//     The TblApiTestData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Models;
    using Newtonsoft.Json;    

    /// <summary>
    /// The TblApiTestData class
    /// </summary>
    public class TblApiTestData : BaseTable
    {
        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public string EndPoint { get; set; }
        
        /// <summary>
        /// Gets or sets the API URL.
        /// </summary>
        /// <value>
        /// The API URL.
        /// </value>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the API category identifier.
        /// </summary>
        /// <value>
        /// The API category identifier.
        /// </value>
        public long? ApiCategoryId { get; set; }

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
        /// Gets or sets the headers.
        /// </summary>
        public string IgnoreHeadersJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.IgnoreHeaders);
            }

            set
            {
                this.IgnoreHeaders = string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<List<NameValuePair>>(value);
            }
        }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        [NotMapped]
        public IEnumerable<NameValuePair> IgnoreHeaders { get; set; }

        /// <summary>
        /// Gets or sets the type of the request.
        /// </summary>
        /// <value>
        /// The type of the request.
        /// </value>
        public long? RequestTypeId { get; set; }
       
        /// <summary>
        /// Gets or sets the request types.
        /// </summary>
        /// <value>
        /// The request types.
        /// </value>
        [ForeignKey("RequestTypeId")]
        public virtual TblRequestTypes RequestTypes { get; set; }

        /// <summary>
        /// Gets or sets the API categories.
        /// </summary>
        /// <value>
        /// The API categories.
        /// </value>
        [ForeignKey("ApiCategoryId")]
        public virtual TblApiCategories ApiCategories { get; set; }
    }
}
