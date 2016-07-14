// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTicketComment.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-07-14</date>
// <summary>
//     The TblTicketComment class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CustomIdentity;
    using Resources.Enum;

    /// <summary>
    /// The Ticket Master class
    /// </summary>
    public class TblTicketComment : BaseTable
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
        /// Gets or sets the User.
        /// </summary>
        [ForeignKey("CreatedBy")]
        public virtual CustomUser CreatedByUser { get; set; }        
    }
}
