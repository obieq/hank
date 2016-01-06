// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLnkSchedulerSuite.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-20</date>
// <summary>
//     The TblLnkSchedulerSuite class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.Linking
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// the TblSchedulerSuite class
    /// </summary>
    public class TblLnkSchedulerSuite : BaseTable
    {
        /// <summary>
        /// Gets or sets the SuiteId
        /// </summary>
        public long SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the SchedulerId
        /// </summary>
        public long SchedulerId { get; set; }
       
        /// <summary>
        /// Gets or sets the Suite
        /// </summary>
        [ForeignKey("SuiteId")]
        public TblSuite Suite { get; set; }

        /// <summary>
        /// Gets or sets the Scheduler
        /// </summary>
        [ForeignKey("SchedulerId")]
        public TblScheduler Scheduler { get; set; }
    }
}
