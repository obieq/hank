// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSuiteDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblSuiteDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Elephant.Hank.Resources.Dto.Linking;

    /// <summary>
    /// The TblSuiteDto class
    /// </summary>
    public class TblSuiteDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the website identifier.
        /// </summary>
        [Required]
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        public TblWebsiteDto Website { get; set; }

        /// <summary>
        /// Gets or sets the suite test map list.
        /// </summary>
        public List<TblLnkSuiteTestDto> SuiteTestMapList { get; set; }
    }
}