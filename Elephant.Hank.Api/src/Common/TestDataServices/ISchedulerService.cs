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
    }
}
