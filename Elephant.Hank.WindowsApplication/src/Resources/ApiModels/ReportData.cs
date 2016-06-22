// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The ReportData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System;

    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum;
    using Elephant.Hank.WindowsApplication.Resources.Extensions;

    /// <summary>
    /// The ReportData class
    /// </summary>
    public class ReportData
    {
        /// <summary>
        /// Gets or sets the test suite identifier.
        /// </summary>
        public long TestQueueId { get; set; }

        /// <summary>
        /// Gets or sets the Execution Status
        /// </summary>
        public string ExecutionStatusText { get; set; }

        /// <summary>
        /// Gets or sets the ReportDataId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ExecutionGroup
        /// </summary>
        public string ExecutionGroup { get; set; }

        /// <summary>
        /// Gets or sets the SuiteId
        /// </summary>
        public int? SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the TestId
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public ExecutionReportStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the SuiteName
        /// </summary>
        public string SuiteName { get; set; }

        /// <summary>
        /// Gets or sets the TestName
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReportData"/> is passed.
        /// </summary>
        public bool? Passed { get; set; }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the Os
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// Gets or sets the BrowserName
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// Gets or sets the Browser version
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// Gets or sets the FinishTime
        /// </summary>
        public int? FinishTime { get; set; }

        /// <summary>
        /// Gets or sets the time taken.
        /// </summary>
        public string TimeTaken { get; set; }

        /// <summary>
        /// Gets or sets the finished at.
        /// </summary>
        public string FinishedAt { get; set; }

        /// <summary>
        /// Gets or sets the finished at.
        /// </summary>
        public DateTime? FinishedAtDateTime
        {
            get
            {
                return this.FinishedAt.ToDateTime();
            }
        }
    }
}
