// ---------------------------------------------------------------------------------------------------
// <copyright file="ISchedulerHistoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-29</date>
// <summary>
//     The ISchedulerHistoryService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ISchedulerHistoryService class
    /// </summary>
    public interface ISchedulerHistoryService : IBaseService<TblSchedulerHistoryDto>
    {
        /// <summary>
        /// Gets the by scheduler identifier.
        /// </summary>
        /// <param name="schedulerId">The scheduler identifier.</param>
        /// <returns>TblSchedulerHistoryDto object</returns>
        ResultMessage<IEnumerable<TblSchedulerHistoryDto>> GetBySchedulerId(long schedulerId);

        /// <summary>
        /// Updates the name of the status by group.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="status">The status.</param>
        /// <param name="emailStatus">The email status.</param>
        /// <returns>
        /// Updated TblSchedulerHistoryDto object
        /// </returns>
        ResultMessage<IEnumerable<TblSchedulerHistoryDto>> UpdateStatusByGroupName(long userId, string groupName, SchedulerExecutionStatus? status, SchedulerHistoryEmailStatus? emailStatus);
    }
}
