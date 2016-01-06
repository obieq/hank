// ---------------------------------------------------------------------------------------------------
// <copyright file="TblDataBaseConnection.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The TblDataBaseConnection class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The TblDataBaseConnection class
    /// </summary>
    public class TblDataBaseConnection : BaseTable
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
        /// Gets or sets the Server Name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Authentication
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
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the DataBaseName
        /// </summary>
        public string DataBaseName { get; set; }

        /// <summary>
        /// Gets or sets the Website
        /// </summary>
        [ForeignKey("WebsiteId")]
        public virtual TblWebsite Website { get; set; }

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        [ForeignKey("EnvironmentId")]
        public virtual TblEnvironment Environment { get; set; }

        /// <summary>
        /// Gets or sets the DataBaseCategories
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual TblDataBaseCategories DataBaseCategories { get; set; }
    }
}
