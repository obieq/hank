// ---------------------------------------------------------------------------------------------------
// <copyright file="ITicketCommentService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-07-14</date>
// <summary>
//     The ITicketCommentService  class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ITicketCommentService interface
    /// </summary>
    public interface ITicketCommentService : IBaseService<TblTicketCommentDto>
    {
        /// <summary>
        /// Saves Ticket Comment
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="ticketId">The ticketId.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        ResultMessage<TblTicketCommentDto> Save(TblTicketCommentDto sourceData, long userId, long ticketId);

        /// <summary>
        /// Gets the by ticket identifier.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>TblTicketCommentDto objects</returns>
        IEnumerable<TblTicketCommentDto> GetByTicketId(long ticketId);        
    }
}