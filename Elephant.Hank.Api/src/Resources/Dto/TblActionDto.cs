// ---------------------------------------------------------------------------------------------------
// <copyright file="TblActionDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblActionDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;

    /// <summary>
    /// The TblActionDto class
    /// </summary>
    public class TblActionDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        public string Value { get; set; }       
    }
}