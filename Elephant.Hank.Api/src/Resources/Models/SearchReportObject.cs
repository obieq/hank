// ---------------------------------------------------------------------------------------------------
// <copyright file="SearchReportObject.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-28</date>
// <summary>
//     The SearchReportObject class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System;

    using Elephant.Hank.Resources.Enum;

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
        /// Gets or sets the suite identifier.
        /// </summary>
        public long? SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        public long? TestId { get; set; }

        /// <summary>
        /// Gets or sets the name of the os.
        /// </summary>
        public string OsName { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the test status.
        /// </summary>
        public ExecutionReportStatus? TestStatus { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the page num.
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the Execution group
        /// </summary>
        public string ExecutionGroup { get; set; }
    }
}
