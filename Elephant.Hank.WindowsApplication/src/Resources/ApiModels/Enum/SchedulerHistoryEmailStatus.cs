// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerHistoryEmailStatus.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The SchedulerHistoryEmailStatus class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum
{
    /// <summary>
    /// The SchedulerHistoryEmailStatus enum
    /// </summary>
    public enum SchedulerHistoryEmailStatus
    {
        /// <summary>
        /// The not applicable
        /// </summary>
        NA,

        /// <summary>
        /// The not sent
        /// </summary>
        NotSent,

        /// <summary>
        /// The sent
        /// </summary>
        Sent,

        /// <summary>
        /// The sent partially
        /// </summary>
        SentPartially,

        /// <summary>
        /// The send exception
        /// </summary>
        SendException,

        /// <summary>
        /// The no valid recipient found
        /// </summary>
        NoValidRecipientFound,

        /// <summary>
        /// The no recipient found
        /// </summary>
        NoRecipientFound
    }
}
