// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLocatorDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblLocatorDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The TblLocatorDto class
    /// </summary>
    public class TblLocatorDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}