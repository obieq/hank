// ---------------------------------------------------------------------------------------------------
// <copyright file="ITicketManagerService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-21</date>
// <summary>
//     The ITicketManagerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using DataService;
    using Resources.Dto;
    using Resources.Messages;
    using Resources.Models;
    
    /// <summary>
    /// The IActionsService interface
    /// </summary>
    public interface ITicketManagerService : IBaseService<TblTicketMasterDto>
    {
        /// <summary>
        /// Get Data Related to Ticket
        /// </summary>
        /// <returns>Returns TicketData</returns>
        ResultMessage<TicketsData> GetTicketData();
    }
}