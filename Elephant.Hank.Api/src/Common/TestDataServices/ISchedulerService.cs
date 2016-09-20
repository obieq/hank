// ---------------------------------------------------------------------------------------------------
// <copyright file="ISchedulerService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-20</date>
// <summary>
//     The ISchedulerService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// the ISchedulerService class
    /// </summary>
    public interface ISchedulerService : IBaseService<TblSchedulerDto>
    {
        /// <summary>
        /// Get List of Scheduler by website identifier
        /// </summary>
        /// <param name="webSiteId">website identifier</param>
        /// <returns>List object of TblSchedulerDto</returns>
        ResultMessage<IEnumerable<TblSchedulerDto>> GetByWebsiteId(long webSiteId);

        /// <summary>
        /// sets the force execute flag to true
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="schedulerId">scheduler identifier</param>
        /// <returns>
        /// object of TblSchedulerDto
        /// </returns>
        ResultMessage<TblSchedulerDto> ForceExecute(long userId, long schedulerId);

        /// <summary>
        /// Forces the execute.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="schedulerId">The scheduler identifier.</param>
        /// <param name="target">The target.</param>
        /// <param name="port">The port.</param>
        /// <returns>Group name</returns>
        ResultMessage<string> ForceExecute(long userId, long schedulerId, string target, int? port);

        /// <summary>
        /// Gets the by URL identifier.
        /// </summary>
        /// <param name="urlId">The URL identifier.</param>
        /// <returns>returns the list of Scheduler by mathing urlid</returns>
        ResultMessage<IEnumerable<TblSchedulerDto>> GetByUrlId(long urlId);
    }
}
