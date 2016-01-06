// ---------------------------------------------------------------------------------------------------
// <copyright file="TblUserProfileDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The TblUserProfileDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using Elephant.Hank.Resources.Dto.CustomIdentity;
    using Elephant.Hank.Resources.Json;

    /// <summary>
    /// the TblUserProfileDto class
    /// </summary>
    public class TblUserProfileDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the User settings
        /// </summary>
        public UserProfileSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the Designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        public CustomUserDto User { get; set; }
    }
}
