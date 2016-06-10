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

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The search report result.
    /// </summary>
    public class SearchReportResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public List<ReportData> Data { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public long? CountFailed { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public long? CountPassed { get; set; }
    }
}
