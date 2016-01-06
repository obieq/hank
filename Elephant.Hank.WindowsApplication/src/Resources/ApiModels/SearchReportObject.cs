// ---------------------------------------------------------------------------------------------------
// <copyright file="SearchReportObject.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The SearchReportObject class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System;

    /// <summary>
    /// The SearchReportObject class
    /// </summary>
    public class SearchReportObject
    {
        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the Execution group
        /// </summary>
        public string ExecutionGroup { get; set; }
    }
}
