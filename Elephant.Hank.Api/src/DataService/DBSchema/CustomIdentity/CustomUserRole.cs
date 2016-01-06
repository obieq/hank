// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomUserRole.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomUserRole class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.CustomIdentity
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Attributes;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The custom user role.
    /// </summary>
    public class CustomUserRole : IdentityUserRole<long>
    {
        /// <summary>
        /// Gets or sets the role details.
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual CustomRole RoleDetails { get; set; }

        /// <summary>
        /// Gets or sets the log tracker.
        /// </summary>
        [NotMapped]
        [EfIgnoreDbLog]
        public Guid LogTracker { get; set; }
    }
}
