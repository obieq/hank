// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSchedulerDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-29</date>
// <summary>
//     The TblSchedulerDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System;   
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Json;

    /// <summary>
    /// the TblSchedulerDto class
    /// </summary>
    public class TblSchedulerDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
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
        public string Url
        {
            get
            {
                if (this.UrlList != null && this.UrlList.Any())
                {
                    var firstOrDefault = this.UrlList.FirstOrDefault(m => m.Id == this.UrlId);

                    if (firstOrDefault != null)
                    {
                        return firstOrDefault.Url;
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the Url List
        /// </summary>
        public IEnumerable<WebsiteUrl> UrlList { get; set; }

        /// <summary>
        /// Gets or sets Frequency
        /// </summary>
        public string Frequency
        {
            get
            {
                FrequencyEnum en = (FrequencyEnum)this.FrequencyType;
                return en.ToString();
            }

            set
            {
                FrequencyEnum en = (FrequencyEnum)Enum.Parse(typeof(FrequencyEnum), value);
                this.FrequencyType = (long)en;
            }
        }

        /// <summary>
        /// Gets or sets the LastUpdatedBy
        /// </summary>
        public string LastUpdatedBy { get; set; }
    }
}
