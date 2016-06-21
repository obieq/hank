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

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum
{
    /// <summary>
    /// The SchedulerExecutionStatus enum
    /// </summary>
    public enum SchedulerExecutionStatus
    {
        /// <summary>
        /// The NA
        /// </summary>
        NA = -1,

        /// <summary>
        /// The in queue
        /// </summary>
        InQueue,

        /// <summary>
        /// The in progress
        /// </summary>
        InProgress,

        /// <summary>
        /// The completed
        /// </summary>
        Completed,

        /// <summary>
        /// The error while executing
        /// </summary>
        ErrorWhileExecuting,

        /// <summary>
        /// The forcily terminated at time out
        /// </summary>
        ForcilyTerminatedAtTimeOut,

        /// <summary>
        /// The cancelled
        /// </summary>
        Cancelled
    }
}
