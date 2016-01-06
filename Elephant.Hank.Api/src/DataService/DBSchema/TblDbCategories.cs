// ---------------------------------------------------------------------------------------------------
// <copyright file="TblDbCategories.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-11</date>
// <summary>
//     The TblDbCategories class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The TblDbCategories class
    /// </summary>
    public class TblDBCategories : BaseTable
    {
        /// <summary>
        /// Gets or sets the EnvironmentId
        /// </summary>
        public long EnvironmentId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryName
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the ServerName
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public int Authentication { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Website Identifier
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        [ForeignKey("EnvironmentId")]
        public virtual TblEnvironment Environment { get; set; }

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        [ForeignKey("WebsiteId")]
        public virtual TblWebsite Website { get; set; }
    }
}
