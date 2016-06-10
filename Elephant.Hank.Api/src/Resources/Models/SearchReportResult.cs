// ---------------------------------------------------------------------------------------------------
// <copyright file="SearchReportResult.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-06-10</date>
// <summary>
//     The SearchReportResult class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System.Collections.Generic;

    using Elephant.Hank.Resources.Dto;

    using Newtonsoft.Json;

    /// <summary>
    /// The search report result.
    /// </summary>
    public class SearchReportResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public IEnumerable<TblReportDataDto> Data { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? CountFailed { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? CountPassed { get; set; }
    }
}
