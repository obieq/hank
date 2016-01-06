// ---------------------------------------------------------------------------------------------------
// <copyright file="SearchLogModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-28</date>
// <summary>
//     The SearchLogModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System;

    /// <summary>
    /// The SearchLogModel class
    /// </summary>
    public class SearchLogModel
    {
        /// <summary>
        /// Gets or sets the startDate
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the endDate
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
