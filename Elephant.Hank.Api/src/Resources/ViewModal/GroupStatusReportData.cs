// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupStatusReportData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-04-26</date>
// <summary>
//     The GroupStatusReportData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.ViewModal
{
    using Elephant.Hank.Resources.Enum;

    /// <summary>
    /// The group status report data.
    /// </summary>
    public class GroupStatusReportData
    {
        /// <summary>
        /// Gets or sets the execution status.
        /// </summary>
        public ExecutionReportStatus ExecutionStatus { get; set; }

        /// <summary>
        /// Gets or sets the status count.
        /// </summary>
        public long StatusCount { get; set; }

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
