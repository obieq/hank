// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomUserRoleDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomUserRoleDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto.CustomIdentity
{
    /// <summary>
    /// The custom user role.
    /// </summary>
    public class CustomUserRoleDto
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the role details.
        /// </summary>
        public virtual CustomRoleDto RoleDetails { get; set; }
    }
}
