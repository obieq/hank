// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLocator.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-17</date>
// <summary>
//     The TblLocator class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblLocator class
    /// </summary>
    public class TblLocator : BaseTable
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}