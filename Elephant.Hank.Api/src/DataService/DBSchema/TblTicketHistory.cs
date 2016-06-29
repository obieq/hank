// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTicketHistory.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-21</date>
// <summary>
//     The TblTicketHistory class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CustomIdentity;

    /// <summary>
    /// The Ticket Master class
    /// </summary>
    public class TblTicketHistory : BaseTable
    {
        /// <summary>
        /// Gets or sets the details of Ticket.
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the TicketId of Ticket.
        /// </summary>
        [Required]
        public long TicketId { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        [ForeignKey("ModifiedBy")]
        public virtual CustomUser ModifiedByUser { get; set; }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        [ForeignKey("CreatedBy")]
        public virtual CustomUser CreatedByUser { get; set; }
    }
}