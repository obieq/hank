// ---------------------------------------------------------------------------------------------------
// <copyright file="TblWebsiteDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblWebsiteDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Elephant.Hank.Resources.Json;

    /// <summary>
    /// The TblWebsiteDto class
    /// </summary>
    public class TblWebsiteDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [Required]
        public List<WebsiteUrl> WebsiteUrlList { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is angular.
        /// </summary>
        [Required]
        public bool IsAngular { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public WebsiteSettings Settings { get; set; }
    }
}
