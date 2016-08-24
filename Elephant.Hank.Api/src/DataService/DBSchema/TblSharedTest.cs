// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSharedTest.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The TblSharedTest class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblSharedTest class
    /// </summary>
    public class TblSharedTest : BaseTable
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
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        [ForeignKey("WebsiteId")]
        public virtual TblWebsite Website { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestDataList
        /// </summary>
        public virtual ICollection<TblSharedTestData> SharedTestDataList { get; set; }

        /// <summary>
        /// Gets or sets the name of the modified by user.
        /// </summary>
        /// <value>
        /// The name of the modified by user.
        /// </value>
        [ForeignKey("ModifiedBy")]
        public virtual CustomUser ModifiedByUser { get; set; }
    }
}
