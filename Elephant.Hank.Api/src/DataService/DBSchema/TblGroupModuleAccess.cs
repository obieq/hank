// ---------------------------------------------------------------------------------------------------
// <copyright file="TblGroupModuleAccess.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The TblGroupModuleAccess class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The TblGroupModuleAccess class.
    /// </summary>
    public class TblGroupModuleAccess : BaseTable
    {
        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or sets the ModuleId
        /// </summary>
        public long ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the CanRead
        /// </summary>
        public bool CanRead { get; set; }

        /// <summary>
        /// Gets or sets the CanWrite
        /// </summary>
        public bool CanWrite { get; set; }

        /// <summary>
        /// Gets or sets the CanDelete
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// Gets or sets the CanDelete
        /// </summary>
        public bool CanExecute { get; set; }

        /// <summary>
        /// Gets or sets the Group
        /// </summary>
        [ForeignKey("GroupId")]
        public virtual TblGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the ModuleId
        /// </summary>
        [ForeignKey("ModuleId")]
        public virtual TblModule Module { get; set; }

        /// <summary>
        /// Gets or sets the Website
        /// </summary>
        [ForeignKey("WebsiteId")]
        public virtual TblWebsite Website { get; set; }
    }
}
