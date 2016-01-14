// ---------------------------------------------------------------------------------------------------
// <copyright file="TblGroup.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The TblGroup class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;

    /// <summary>
    /// The TblGroup class.
    /// </summary>
    public class TblGroup : BaseTable 
    {
        /// <summary>
        /// Gets or sets the group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        [ForeignKey("ModifiedBy")]
        public virtual CustomUser ModifiedByUser { get; set; }
    }
}
