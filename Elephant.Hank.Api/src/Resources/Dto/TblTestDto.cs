// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblTestDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblTestDto class
    /// </summary>
    public class TblTestDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        [Required]
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the website identifier.
        /// </summary>
        [Required]
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        public long? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Get or sets the TestCaseAccessStatus.
        /// </summary>
        public long TestCaseAccessStatus { get; set; }

        /// <summary>
        /// Gets or sets the CreatedByUserName.
        /// </summary>
        public string CreatedByUserName { get; set; }
    }
}