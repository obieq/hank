// ---------------------------------------------------------------------------------------------------
// <copyright file="ChartData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-08-19</date>
// <summary>
//     The ChartData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System;

    /// <summary>
    /// The Chart Data class
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the count passed.
        /// </summary>
        /// <value>
        /// The count passed.
        /// </value>
        public long CountPassed { get; set; }

        /// <summary>
        /// Gets or sets the count failed.
        /// </summary>
        /// <value>
        /// The count failed.
        /// </value>
        public long CountFailed { get; set; }

        /// <summary>
        /// Gets or sets the count un processed.
        /// </summary>
        /// <value>
        /// The count un processed.
        /// </value>
        public long CountUnProcessed { get; set; }

        /// <summary>
        /// Gets or sets the count cancelled.
        /// </summary>
        /// <value>
        /// The count cancelled.
        /// </value>
        public long CountCancelled { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public long Total { get; set; }
    }
}
