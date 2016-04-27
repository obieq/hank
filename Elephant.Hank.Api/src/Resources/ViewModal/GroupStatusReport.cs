// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupStatusReport.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-04-26</date>
// <summary>
//     The GroupStatusReport class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.ViewModal
{
    using System.Collections.Generic;

    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The group status report.
    /// </summary>
    public class GroupStatusReport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupStatusReport"/> class.
        /// </summary>
        public GroupStatusReport()
        {
            this.CountByStatus = new List<NameValuePair>();    
        }

        /// <summary>
        /// Gets or sets the count by status.
        /// </summary>
        public List<NameValuePair> CountByStatus { get; set; }

        /// <summary>
        /// Gets or sets the test count.
        /// </summary>
        public long TestCount { get; set; }

        /// <summary>
        /// Gets or sets the processed count.
        /// </summary>
        public long ProcessedCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is complete.
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
