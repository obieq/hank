// ---------------------------------------------------------------------------------------------------
// <copyright file="TblPages.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-04</date>
// <summary>
//     The TblPages class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblPages class
    /// </summary>
    public class TblPages : BaseTable
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

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
