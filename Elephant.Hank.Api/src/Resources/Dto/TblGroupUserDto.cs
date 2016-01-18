// ---------------------------------------------------------------------------------------------------
// <copyright file="TblGroupUserDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The TblGroupUserDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblGroupUserDto class
    /// </summary>
    public class TblGroupUserDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or sets the Group
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public string ModifiedByUserName { get; set; }
    }
}
