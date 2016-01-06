// ---------------------------------------------------------------------------------------------------
// <copyright file="SearchData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-25</date>
// <summary>
//     The SearchData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System;

    using Elephant.Hank.Resources.Extensions;

    /// <summary>
    /// The SearchData class
    /// </summary>
    public class SearchData
    {
        /// <summary>
        /// Gets or sets a value indicating whether [only latest].
        /// </summary>
        public bool OnlyLatest { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string ReportDate { get; set; }

        /// <summary>
        /// Gets the report date time.
        /// </summary>
        public DateTime? ReportDateTime 
        {
            get
            {
                return this.ReportDate.ToDate();
            }
        }

        /// <summary>
        /// Gets or sets the OS.
        /// </summary>
        public string OperatingSystem { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the time slot.
        /// </summary>
        public string ExecutionSlot { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [load lookups].
        /// </summary>
        public bool LoadLookups { get; set; }
    }
}