// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketType.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-22</date>
// <summary>
//     The TicketType class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Enum
{
    using Attributes;

    /// <summary>
    /// The TicketType class
    /// </summary>
    public enum TicketType
    {
        /// <summary>
        /// TicketType Bug
        /// </summary>
        [DisplayText("Bug")] Bug = 1,

        /// <summary>
        /// TicketType Enhancement
        /// </summary>
        [DisplayText("Enhancement")] Enhancement = 2
    }
}