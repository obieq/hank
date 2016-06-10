// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportResultData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The ReportResultData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.WindowsApplication.Resources.ApiModels;

    /// <summary>
    /// The ReportResultData class
    /// </summary>
    public class ReportResultData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportResultData" /> class.
        /// </summary>
        /// <param name="reportData">The report data.</param>
        /// <param name="scheduler">The scheduler.</param>
        /// <param name="groupName">Name of the group.</param>
        public ReportResultData(IEnumerable<ReportData> reportData, Scheduler scheduler, string groupName)
        {
            this.ReportData = reportData;

            this.UrlTested = scheduler.Url;
            this.PostFixToSubject = "Scheduler: " + scheduler.Name;
            this.WebsiteId = scheduler.WebsiteId;
            this.DoSendEmail = scheduler.Settings.DoEmailReport;
            this.ToEmailIds = scheduler.Settings.ToEmailIds;

            this.GroupName = groupName;

            if (this.ReportData != null)
            {
                this.PassedCount = this.ReportData.Count(x => x.Passed ?? false);
                this.FaultCount = this.ReportData.Count(x => x.Passed.HasValue && !x.Passed.Value);
                this.UnProcessedCount = this.ReportData.Count() - (this.PassedCount + this.FaultCount);
            }
        }

        /// <summary>
        /// Gets or sets the report data.
        /// </summary>
        public IEnumerable<ReportData> ReportData { get; set; }

        /// <summary>
        /// Gets or sets the post fix to subject.
        /// </summary>
        public string PostFixToSubject { get; set; }

        /// <summary>
        /// Gets or sets the URL tested.
        /// </summary>
        public string UrlTested { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the website identifier.
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [do send email].
        /// </summary>
        public bool DoSendEmail { get; set; }

        /// <summary>
        /// Gets or sets to email ids.
        /// </summary>
        public string ToEmailIds { get; set; }

        /// <summary>
        /// Gets or sets the passed count.
        /// </summary>
        public int PassedCount { get; set; }

        /// <summary>
        /// Gets or sets the fault count.
        /// </summary>
        public int FaultCount { get; set; }

        /// <summary>
        /// Gets or sets the un processed count.
        /// </summary>
        public int UnProcessedCount { get; set; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        public int TotalCount 
        {
            get
            {
                return this.PassedCount + this.FaultCount + this.UnProcessedCount;
            }
        }
    }
}
