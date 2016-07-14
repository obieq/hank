// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTicketCommentDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-07-14</date>
// <summary>
//     The TblTicketCommentDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblTicketCommentDto class
    /// </summary>
    public class TblTicketCommentDto : BaseTableDto
    {       
        /// <summary>
        /// Gets or sets the Description of Ticket.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the TicketId of Ticket.
        /// </summary>
        [Required]
        public long TicketId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedByUserName.
        /// </summary>
        public string CreatedByUserName { get; set; }
    }
}