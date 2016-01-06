// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSuite.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-17</date>
// <summary>
//     The TblSuite class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;

    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblSuite class
    /// </summary>
    public class TblSuite : BaseTable
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
        public virtual TblWebsite Website { get; set; }
    }
}
