// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTicketMasterDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-21</date>
// <summary>
//     The TblTicketMasterDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enum;
    using Extensions;
    using Models;
    
    /// <summary>
    /// The TblTicketMasterDto class
    /// </summary>
    public class TblTicketMasterDto : BaseTableDto
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
        /// Gets the TicketType text.
        /// </summary>
        public string TicketTypeText
        {
            get
            {
                return this.Type.GetAttributeText();
            }
        }

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
        /// Gets the TicketStatus text.
        /// </summary>
        public string TicketStatusText
        {
            get
            {
                return this.Status.GetAttributeText();
            }
        }

        /// <summary>
        /// Gets or sets the Priority of Ticket.
        /// </summary>
        [Required]
        public TicketPriority Priority { get; set; }

        /// <summary>
        /// Gets the Priority text.
        /// </summary>
        public string PriorityText
        {
            get
            {
                return this.Priority.GetAttributeText();
            }
        }

        /// <summary>
        /// Gets or sets the Data required to display text
        /// </summary>
        [Required]
        public TicketsData TicketData { get; set; }
        
        /// <summary>
        /// Gets or sets the History for the Ticket
        /// </summary>
        [Required]
        public List<TblTicketMasterDto> TicketHistory { get; set; }

        /// <summary>
        /// Gets or sets the CreatedByUserName.
        /// </summary>
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the Assigned To UserName.
        /// </summary>
        public string AssignedToUserName { get; set; }
    }
}