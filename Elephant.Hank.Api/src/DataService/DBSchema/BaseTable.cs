// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseTable.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-16</date>
// <summary>
//     The BaseTable class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The BaseTable class
    /// </summary>
    public class BaseTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTable"/> class.
        /// </summary>
        public BaseTable()
        {
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;
            this.LogTracker = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [EfIgnoreDbLog]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public long ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        [EfIgnoreDbLog]
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the Log tracker
        /// </summary>
        [NotMapped]
        [EfIgnoreDbLog]
        public Guid LogTracker { get; set; }
    }
}