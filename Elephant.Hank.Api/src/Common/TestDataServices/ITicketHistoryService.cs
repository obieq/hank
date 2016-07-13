// ---------------------------------------------------------------------------------------------------
// <copyright file="ITicketHistoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-21</date>
// <summary>
//     The ITicketHistoryService  class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using DataService;

    using Resources.Dto;
    using Resources.Messages;
    
    /// <summary>
    /// The IActionsService interface
    /// </summary>
    public interface ITicketHistoryService : IBaseService<TblTicketHistoryDto>
    {
        /// <summary>
        /// Saves Ticket History
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        ResultMessage<TblTicketHistoryDto> Save(TblTicketMasterDto sourceData, long userId);

        /// <summary>
        /// Gets the by ticket identifier.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>TblTicketMasterDto objects</returns>
        IEnumerable<TblTicketMasterDto> GetByTicketId(long ticketId);
    }
}