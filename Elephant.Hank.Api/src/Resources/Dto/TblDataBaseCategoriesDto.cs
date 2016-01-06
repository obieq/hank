// ---------------------------------------------------------------------------------------------------
// <copyright file="TblDataBaseCategoriesDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The TblDataBaseCategoriesDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblDataBaseCategoriesDto class
    /// </summary>
    public class TblDataBaseCategoriesDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Website Name
        /// </summary>
        public string WebsiteName { get; set; }
    }
}
