// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSchedulerHistoryDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-29</date>
// <summary>
//     The TblSchedulerHistoryDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using Elephant.Hank.Resources.Enum;

    /// <summary>
    /// The TblSchedulerHistoryDto class
    /// </summary>
    public class TblSchedulerHistoryDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the name of the scheduler.
        /// </summary>
        public string SchedulerName { get; set; }

        /// <summary>
        /// Gets or sets the scheduler identifier.
        /// </summary>
        public long SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public SchedulerExecutionStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        public string StatusText { get; set; }

        /// <summary>
        /// Gets or sets the email status.
        /// </summary>
        public SchedulerHistoryEmailStatus EmailStatus { get; set; }

        /// <summary>
        /// Gets or sets the email status text.
        /// </summary>
        public string EmailStatusText { get; set; }

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ForceExecute test].
        /// </summary>
        public bool ForceExecute { get; set; }
    }
}
