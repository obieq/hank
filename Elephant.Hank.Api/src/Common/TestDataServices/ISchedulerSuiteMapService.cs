// ---------------------------------------------------------------------------------------------------
// <copyright file="ISchedulerSuiteMapService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-20</date>
// <summary>
//     The ISchedulerSuiteMapService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Messages;    

    /// <summary>
    /// The ISchedulerSuiteService interface
    /// </summary>
    public interface ISchedulerSuiteMapService : IBaseService<TblLnkSchedulerSuiteDto>
    {
        /// <summary>
        /// Get Scheduler Suite by SchedulerId
        /// </summary>
        /// <param name="schedulerId">scheduler identifier</param>
        /// <returns>TblSchedulerSuiteDto object</returns>
        ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>> GetBySchedulerId(long schedulerId);

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="schedulerId">The suite identifier.</param>
        /// <param name="sourceData">The source data.</param>
        /// <returns>
        /// TblSchedulerSuiteDto objects
        /// </returns>
        ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>> SaveOrUpdate(long userId, long schedulerId, List<TblLnkSchedulerSuiteDto> sourceData);
    }
}
