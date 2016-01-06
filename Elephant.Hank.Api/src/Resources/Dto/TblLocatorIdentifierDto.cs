// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLocatorIdentifierDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblLocatorIdentifierDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblLocatorIdentifierDto class
    /// </summary>
    public class TblLocatorIdentifierDto : BaseTableDto
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
        public string PageName { get; set; }

        /// <summary>
        /// Gets or sets the name of the website.
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Gets or sets the locator.
        /// </summary>
        public string LocatorValue { get; set; }
    }
}