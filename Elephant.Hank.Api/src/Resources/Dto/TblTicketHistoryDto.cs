// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTicketHistoryDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-21</date>
// <summary>
//     The TblTicketHistoryDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblTicketHistoryDto class
    /// </summary>
    public class TblTicketHistoryDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the Json Data of Ticket.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the TicketId of Ticket.
        /// </summary>
        [Required]
        public long TicketId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedByFullName.
        /// </summary>
        public string CreatedByFullName { get; set; }
    }
}