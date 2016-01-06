// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomUserDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomUserDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto.CustomIdentity
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The CustomUser user.
    /// </summary>
    public class CustomUserDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomUserDto"/> class.
        /// </summary>
        public CustomUserDto()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.ModifiedOn = DateTime.UtcNow;
            this.ActivationId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the access failed count.
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether email confirmed.
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether lockout enabled.
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the lockout end date utc.
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether phone number confirmed.
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the security stamp.
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public ICollection<CustomUserRoleDto> Roles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the activation id.
        /// </summary>
        public string ActivationId { get; set; }

        /// <summary>
        /// Gets or sets the activated on.
        /// </summary>
        public DateTime? ActivatedOn { get; set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public long ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }
    }
}