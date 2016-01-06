// ---------------------------------------------------------------------------------------------------
// <copyright file="Scheduler.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The Scheduler class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System;

    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum;

    /// <summary>
    /// The Scheduler class
    /// </summary>
    public class Scheduler
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the UrlId
        /// </summary>
        public long UrlId { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the FrequencyType
        /// </summary>
        public long FrequencyType { get; set; }

        /// <summary>
        /// Gets or sets the StartDateTime
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the RepeatTaskFrequency
        /// </summary>
        public long RepeatTaskFrequency { get; set; }

        /// <summary>
        /// Gets or sets the RepeatTaskDuration
        /// </summary>
        public long RepeatTaskDuration { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteName
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets  RecurEvery
        /// </summary>
        public long? RecurEvery { get; set; }

        /// <summary>
        /// Gets or sets  StopIfLongerThan
        /// </summary>
        public long? StopIfLongerThan { get; set; }

        /// <summary>
        /// Gets or sets the ExpiresDateTime
        /// </summary>
        public DateTime? ExpirationDateTime { get; set; }

        /// <summary>
        /// Gets or sets the last executed.
        /// </summary>
        public DateTime? LastExecuted { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the setting.
        /// </summary>
        public SchedulerSettings Settings { get; set; }

        /// <summary>
        /// Gets the Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        public FrequencyEnum Frequency { get; set; }
    }
}
