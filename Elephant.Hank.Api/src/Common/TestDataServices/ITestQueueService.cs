// ---------------------------------------------------------------------------------------------------
// <copyright file="ITestQueueService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-06</date>
// <summary>
//     The ITestQueueService interface
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
    /// The ITestQueueService Interface
    /// </summary>
    public interface ITestQueueService : IBaseService<TblTestQueueDto>
    {
        /// <summary>
        /// Get All unprocessed tests 
        /// </summary>
        /// <returns>List TblTestQueueDto</returns>
        ResultMessage<IEnumerable<TblTestQueueDto>> GetAllUnProcessed();

        /// <summary>
        /// Updates the name of the test queue status by group.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// Status update
        /// </returns>
        ResultMessage<bool> UpdateTestQueueStatusByGroupName(long userId, string groupName, ExecutionReportStatus status);

        /// <summary>
        /// Updates the test queue processing flag.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="isProcessed">if set to <c>true</c> [is processed].</param>
        /// <returns>
        /// Update status
        /// </returns>
        ResultMessage<bool> UpdateTestQueueProcessingFlag(long userId, string groupName, bool isProcessed);
    }
}
