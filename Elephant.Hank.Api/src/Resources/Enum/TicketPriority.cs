// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketPriority.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-22</date>
// <summary>
//     The TicketPriority class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Enum
{
    using Attributes;

    /// <summary>
    /// The UserModel class
    /// </summary>
    public enum TicketPriority
    {
        /// <summary>
        /// Ticket Priority High
        /// </summary>
        [DisplayText("High")]
        High = 1,

        /// <summary>
        /// Ticket Priority Medium
        /// </summary>
        [DisplayText("Medium")]
        Medium = 2,

        /// <summary>
        /// Ticket Priority Low
        /// </summary>
        [DisplayText("Low")]
        Low = 3
    }
}