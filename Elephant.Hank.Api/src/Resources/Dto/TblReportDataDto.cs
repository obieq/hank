// ---------------------------------------------------------------------------------------------------
// <copyright file="TblReportDataDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-30</date>
// <summary>
//     The TblReportDataDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System;
    using System.Configuration;
    using System.Web;

    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Extensions;

    using Newtonsoft.Json;

    /// <summary>
    /// The TblReportDataDto class
    /// </summary>
    public class TblReportDataDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the test suite identifier.
        /// </summary>
        public long TestQueueId { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [JsonIgnore]
        public long Count { get; set; }

        /// <summary>
        /// Gets or sets the count passed.
        /// </summary>
        [JsonIgnore]
        public long CountPassed { get; set; }

        /// <summary>
        /// Gets or sets the count failed.
        /// </summary>
        [JsonIgnore]
        public long CountFailed { get; set; }

        /// <summary>
        /// Gets or sets the execution group.
        /// </summary>
        public string ExecutionGroup { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the Execution Status
        /// </summary>
        public ExecutionReportStatus Status { get; set; }

        /// <summary>
        /// Gets the execution status text.
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.Status.GetAttributeText();
            }
        }

        /// <summary>
        /// Gets or sets the SuiteId
        /// </summary>
        public int? SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the TestId
        /// </summary>
        public int TestId { get; set; }

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
        /// Gets or sets a value indicating whether this <see cref="TblReportDataDto"/> is passed.
        /// </summary>
        public bool? Passed { get; set; }

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
        public string FinishTime { get; set; }

        /// <summary>
        /// Gets the time taken.
        /// </summary>
        public string TimeTaken
        {
            get
            {
                var timeSpan = TimeSpan.FromMilliseconds(this.FinishTime.ToInt32());
                return timeSpan.ToString(@"hh\:mm\:ss");
            }
        }

        /// <summary>
        /// Gets or sets the finished at.
        /// </summary>
        public string FinishedAt { get; set; }

        /// <summary>
        /// Gets or sets the screen shot file.
        /// </summary>
        public string ScreenShotFile { get; set; }

        /// <summary>
        /// Gets the screen shot URL.
        /// </summary>
        public string ScreenShotUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigConstants.ImageViewUrl] + HttpUtility.UrlEncode(this.ScreenShotFile);
            }
        }

        /// <summary>
        /// Gets the screen shot thum200 URL.
        /// </summary>
        public string ScreenShotThum200Url
        {
            get
            {
                return this.ScreenShotUrl.Replace(".png", "-t200.png");
            }
        }

        /// <summary>
        /// Gets the ImageViewUrl
        /// </summary>
        public string ImageViewUrl
        {
            get
            {
                return ConfigurationManager.AppSettings[ConfigConstants.ImageViewUrl];
            }
        }

        /// <summary>
        /// Gets or sets the Last step executed
        /// </summary>
        public string LastStepExecuted { get; set; }

        /// <summary>
        /// Gets or sets the QueuedBy
        /// </summary>
        public string QueuedBy { get; set; }
    }
}