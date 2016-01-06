// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportJson.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-25</date>
// <summary>
//     The ReportJson class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    using System;

    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The ReportJson class
    /// </summary>
    public class ReportJson
    {
        /// <summary>
        /// Gets or sets the test queue identifier.
        /// </summary>
        public long TestQueueId { get; set; }

        /// <summary>
        /// Gets or sets the execution group.
        /// </summary>
        public string ExecutionGroup { get; set; }

        /// <summary>
        /// Gets or sets the URL tested.
        /// </summary>
        public string UrlTested { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the trace.
        /// </summary>
        public string Trace { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReportJson"/> is passed.
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// Gets or sets the finish time.
        /// </summary>
        public long? FinishTime { get; set; }

        /// <summary>
        /// Gets or sets the finished at.
        /// </summary>
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        /// Gets or sets the OS.
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// Gets or sets the name of the browser.
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// Gets or sets the browser version.
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// Gets or sets the screen shot file.
        /// </summary>
        public string ScreenShotFile { get; set; }

        /// <summary>
        /// Gets or sets the trace full.
        /// </summary>
        public FullTraceJson[] TraceFull { get; set; }

        /// <summary>
        /// Gets or sets the log container.
        /// </summary>
        public NameValuePair[] LogContainer { get; set; }

        /// <summary>
        /// Gets or sets the variable container.
        /// </summary>
        public NameValuePair[] VariableStateContainer { get; set; }

        /// <summary>
        /// Gets or sets the screenShotArray
        /// </summary>
        public NameValuePair[] ScreenShotArray { get; set; }

        /// <summary>
        /// Gets or sets the report updation source
        /// </summary>
        public string ReportSource { get; set; }

        /// <summary>
        /// Gets or sets the Last step executed
        /// </summary>
        public string LastStepExecuted { get; set; }
    }
}
