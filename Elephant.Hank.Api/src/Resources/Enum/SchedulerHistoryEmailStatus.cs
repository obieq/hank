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

namespace Elephant.Hank.Resources.Enum
{
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The SchedulerHistoryEmailStatus enum
    /// </summary>
    public enum SchedulerHistoryEmailStatus
    {
        /// <summary>
        /// The not applicable
        /// </summary>
        [DisplayText("NA")]
        NA,

        /// <summary>
        /// The not sent
        /// </summary>
        [DisplayText("Not sent")]
        NotSent,

        /// <summary>
        /// The sent
        /// </summary>
        [DisplayText("Sent")]
        Sent,

        /// <summary>
        /// The sent partially
        /// </summary>
        [DisplayText("Sent partially")]
        SentPartially,

        /// <summary>
        /// The send exception
        /// </summary>
        [DisplayText("Exception occurred")]
        SendException,

        /// <summary>
        /// The no valid recipient found
        /// </summary>
        [DisplayText("No valid recipient found")]
        NoValidRecipientFound,

        /// <summary>
        /// The no recipient found
        /// </summary>
        [DisplayText("No recipient found")]
        NoRecipientFound
    }
}
