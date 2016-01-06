// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestCategoriesDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-01</date>
// <summary>
//     The TblTestCategoriesDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblTestCategoriesDto class
    /// </summary>
    public class TblTestCategoriesDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the website identifier.
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the name of the website.
        /// </summary>
        public string WebsiteName { get; set; }
    }
}
