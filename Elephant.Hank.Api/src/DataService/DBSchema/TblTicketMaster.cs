// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTicketMaster.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-21</date>
// <summary>
//     The TblTicketMaster class
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
    public class TblTicketMaster : BaseTable
    {
        /// <summary>
        /// Gets or sets the Title of Ticket.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description of Ticket.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Type of Ticket.
        /// </summary>
        [Required]
        public TicketType Type { get; set; }

        /// <summary>
        /// Gets or sets the Ticket Assigned To.
        /// </summary>
        [Required]
        public long AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the Status of Ticket.
        /// </summary>
        [Required]
        public TicketStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the Priority of Ticket.
        /// </summary>
        [Required]
        public TicketPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        [ForeignKey("CreatedBy")]
        public virtual CustomUser CreatedByUser { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        [ForeignKey("AssignedTo")]
        public virtual CustomUser AssignedToUser { get; set; }
    }
}
