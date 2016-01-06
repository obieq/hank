// ---------------------------------------------------------------------------------------------------
// <copyright file="RollDataDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-27</date>
// <summary>
//     The RollDataDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The RollDataDto class
    /// </summary>
    public class RollDataDto
    {
        /// <summary>
        /// Gets or sets the LogId
        /// </summary>
        public long LogId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [to old value].
        /// </summary>
        public bool ToOldValue { get; set; }
    }
}
