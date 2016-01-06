// ---------------------------------------------------------------------------------------------------
// <copyright file="TblDbLog.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-18</date>
// <summary>
//     The TblDbLog class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Resources.Attributes;    

    /// <summary>
    /// The TblDbLog class
    /// </summary>
    [EfIgnoreDbLog]
    public class TblDbLog : BaseTable
    {
        /// <summary>
        /// Gets or sets the PreviosValue
        /// </summary>
        public string PreviousValue { get; set; }

        /// <summary>
        /// Gets or sets the NewValue
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the ValueId
        /// </summary>
        public long ValueId { get; set; }

        /// <summary>
        /// Gets or sets the TableType
        /// </summary>
        public string TableType { get; set; }

        /// <summary>
        /// Gets or sets the OperationType
        /// </summary>
        public int OperationType { get; set; }

        /// <summary>
        /// Gets or sets the User object
        /// </summary>
        [ForeignKey("ModifiedBy")]
        public virtual CustomUser User { get; set; }
    }
}
