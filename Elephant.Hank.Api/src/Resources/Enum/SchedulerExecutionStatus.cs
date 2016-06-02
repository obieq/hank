// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerExecutionStatus.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The SchedulerExecutionStatus enum
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Enum
{
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The SchedulerExecutionStatus enum
    /// </summary>
    public enum SchedulerExecutionStatus
    {
        /// <summary>
        /// The NA
        /// </summary>
        [DisplayText("NA")]
        NA = -1,

        /// <summary>
        /// The in queue
        /// </summary>
        [DisplayText("In queue")]
        InQueue,

        /// <summary>
        /// The in progress
        /// </summary>
        [DisplayText("In progress")]
        InProgress,

        /// <summary>
        /// The completed
        /// </summary>
        [DisplayText("Complete")]
        Completed,

        /// <summary>
        /// The error while executing
        /// </summary>
        [DisplayText("Error at runtime")]
        ErrorWhileExecuting,

        /// <summary>
        /// The forcily terminated at time out
        /// </summary>
        [DisplayText("Ran out of time")]
        ForcilyTerminatedAtTimeOut
    }
}
