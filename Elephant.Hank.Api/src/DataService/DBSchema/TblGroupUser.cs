// ---------------------------------------------------------------------------------------------------
// <copyright file="TblGroupUser.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The TblGroupUser class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;

    /// <summary>
    /// The TblGroupUser class.
    /// </summary>
    public class TblGroupUser : BaseTable
    {
        /// <summary>
        /// Gets or set the UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or sets the Group
        /// </summary>
        [ForeignKey("GroupId")]
        public virtual TblGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        [ForeignKey("UserId")]
        public virtual CustomUser User { get; set; }
    }
}
