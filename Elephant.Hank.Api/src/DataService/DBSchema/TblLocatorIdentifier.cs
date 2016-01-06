// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLocatorIdentifier.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-17</date>
// <summary>
//     The TblLocatorIdentifier class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblLocatorIdentifier class
    /// </summary>
    public class TblLocatorIdentifier : BaseTable
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the display name identifier.
        /// </summary>
        [Required]
        public long PageId { get; set; }

        /// <summary>
        /// Gets or sets the locator identifier.
        /// </summary>
        [Required]
        public long LocatorId { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        [ForeignKey("PageId")]
        public virtual TblPages Page { get; set; }

        /// <summary>
        /// Gets or sets the locator.
        /// </summary>
        [ForeignKey("LocatorId")]
        public virtual TblLocator Locator { get; set; }
    }
}
