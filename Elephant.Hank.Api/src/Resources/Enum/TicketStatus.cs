// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketStatus.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-22</date>
// <summary>
//     The TicketStatus class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Enum
  {
      using Attributes;

    /// <summary>
    /// The TicketStatus class
    /// </summary>
    public enum TicketStatus
    {
        /// <summary>
        /// Ticket Status New
        /// </summary>
        [DisplayText("New")] New = 1,

        /// <summary>
        /// Ticket Status In Progress
        /// </summary>
        [DisplayText("In Progress")] InProgress = 2,

        /// <summary>
        /// Ticket Status On Hold
        /// </summary>
        [DisplayText("On Hold")] OnHold = 3,

        /// <summary>
        /// Ticket Status Completed
        /// </summary>
        [DisplayText("Completed")] Completed = 4
    }
}