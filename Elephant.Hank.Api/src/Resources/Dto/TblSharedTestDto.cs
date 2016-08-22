// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSharedTestDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The TblSharedTestDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// The TblSharedTestDto class
    /// </summary>
    public class TblSharedTestDto : BaseTableDto
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
        /// Gets or sets the SharedTestDataList
        /// </summary>
        public virtual IEnumerable<TblSharedTestDataDto> SharedTestDataList
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the modified by user.
        /// </summary>
        /// <value>
        /// The name of the modified by user.
        /// </value>
        public string ModifiedByUserName { get; set; }
    }
}
