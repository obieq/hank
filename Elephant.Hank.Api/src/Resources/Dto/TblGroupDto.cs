// ---------------------------------------------------------------------------------------------------
// <copyright file="TblGroupDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The TblGroupDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblGroupDto class
    /// </summary>
    public class TblGroupDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedByUserName
        /// </summary>
        public string ModifiedByUserName { get; set; }
    }
}
