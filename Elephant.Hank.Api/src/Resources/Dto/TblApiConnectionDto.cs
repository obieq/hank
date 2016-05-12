// ---------------------------------------------------------------------------------------------------
// <copyright file="TblApiConnectionDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The TblApiConnectionDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;

    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TblApiConnectionDto class
    /// </summary>
    public class TblApiConnectionDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets EnvironmentIds
        /// </summary>
        public long EnvironmentId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the base url.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public List<NameValuePair> Headers { get; set; }

        /// <summary>
        /// Gets or sets the Website
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
