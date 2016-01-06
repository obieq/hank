// ---------------------------------------------------------------------------------------------------
// <copyright file="TblScheduler.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-20</date>
// <summary>
//     The TblScheduler class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Resources.Attributes;
    using Elephant.Hank.Resources.Json;

    using Newtonsoft.Json;

    /// <summary>
    /// the TblScheduler class
    /// </summary>
    public class TblScheduler : BaseTable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Force Execute or not.
        /// </summary>
        public bool ForceExecute { get; set; }

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
        /// Gets or sets a value indicating whether this instance is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets the RecurEvery
        /// </summary>
        public long? RecurEvery { get; set; }

        /// <summary>
        /// Gets or sets the StopIfLongerThan
        /// </summary>
        public long? StopIfLongerThan { get; set; }

        /// <summary>
        /// Gets or sets the ExpiresDateTime
        /// </summary>
        public DateTime? ExpirationDateTime { get; set; }

        /// <summary>
        /// Gets or sets the last executed.
        /// </summary>
        [EfIgnore]
        public DateTime? LastExecuted { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Website
        /// </summary>
        [ForeignKey("WebsiteId")]
        public virtual TblWebsite Website { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public string SettingsJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.Settings);
            }

            set
            {
                this.Settings = string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<SchedulerSettings>(value);
            }
        }

        /// <summary>
        /// Gets or sets the setting.
        /// </summary>
        [NotMapped]
        public SchedulerSettings Settings { get; set; }
     
        /// <summary>
        /// Gets or sets the User object
        /// </summary>
        [ForeignKey("ModifiedBy")]
        public virtual CustomUser User { get; set; }
    }
}