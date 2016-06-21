// ---------------------------------------------------------------------------------------------------
// <copyright file="ExecutionReportStatus.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-06-21</date>
// <summary>
//     The ExecutionReportStatus class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum
{
    /// <summary>
    /// The ExecutionReportStatus enum
    /// </summary>
    public enum ExecutionReportStatus
    {
        /// <summary>
        /// The in queue
        /// </summary>
        InQueue = 0,

        /// <summary>
        /// The in progress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The loading data
        /// </summary>
        LoadingData = 2,

        /// <summary>
        /// The in execution
        /// </summary>
        InExecution = 3,

        /// <summary>
        /// The completed
        /// </summary>
        Completed = 4,

        /// <summary>
        /// The completed
        /// </summary>
        Cancelled = 5,

        /// <summary>
        /// The passed
        /// </summary>
        Passed = 8,

        /// <summary>
        /// The failed
        /// </summary>
        Failed = 9,
    }
}
