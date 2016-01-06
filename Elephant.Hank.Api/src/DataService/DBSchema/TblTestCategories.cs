// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestCategories.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-01</date>
// <summary>
//     The TblTestCategories class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The TblTestCategories class
    /// </summary>
    public class TblTestCategories : BaseTable
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
        /// Gets or sets the website.
        /// </summary>
        [ForeignKey("WebsiteId")]
        public virtual TblWebsite Website { get; set; }
    }
}
