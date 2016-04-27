// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestQueue.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-06</date>
// <summary>
//     The TblTestQueue class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Attributes;
    using Elephant.Hank.Resources.Json;
    using Newtonsoft.Json; 

    /// <summary>
    /// The TblTestQueue class
    /// </summary>
    [EfIgnoreDbLog]
    public class TblTestQueue : BaseTable
    {
        /// <summary>
        /// Gets or sets the test identifier
        /// </summary>
        [Required]
        public long TestId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of Suite
        /// </summary>
        public long? SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of Scheduler
        /// </summary>
        public long? SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the Processed status of TestQueue
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the execution group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the SettingsJson
        /// </summary>
        public string SettingsJson 
        {
            get
            {
                return JsonConvert.SerializeObject(this.Settings);
            }

            set
            {
                this.Settings = string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<TestQueueSettings>(value);
            }
        }

        /// <summary>
        /// Gets or sets the Settings
        /// </summary>
        [NotMapped]
        public TestQueueSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        [ForeignKey("TestId")]
        public virtual TblTest TestCase { get; set; }
    }
}
