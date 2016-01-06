// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSchedulerHistory.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-29</date>
// <summary>
//     The TblSchedulerHistory class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Attributes;
    using Elephant.Hank.Resources.Enum;

    /// <summary>
    /// The TblSchedulerHistory class
    /// </summary>
    [EfIgnoreDbLog]
    public class TblSchedulerHistory : BaseTable
    {
        /// <summary>
        /// Gets or sets the scheduler identifier.
        /// </summary>
        public long SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public SchedulerExecutionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the email status.
        /// </summary>
        public SchedulerHistoryEmailStatus? EmailStatus { get; set; }

        /// <summary>
        /// Gets or sets the scheduler.
        /// </summary>
        [ForeignKey("SchedulerId")]
        public virtual TblScheduler Scheduler { get; set; }
    }
}
