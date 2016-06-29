// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketsData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-23</date>
// <summary>
//     The TicketsData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The SearchCriteriaData class
    /// </summary>
    public class TicketsData
    {
        /// <summary>
        /// Gets or sets the operation systems.
        /// </summary>
        public List<NameValueIntPair> Type { get; set; }

        /// <summary>
        /// Gets or sets the test cases.
        /// </summary>
        public List<NameValueIntPair> AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        public List<NameValueIntPair> Status { get; set; }

        /// <summary>
        /// Gets or sets the suites.
        /// </summary>
        public List<NameValueIntPair> Priority { get; set; }
    }
}
