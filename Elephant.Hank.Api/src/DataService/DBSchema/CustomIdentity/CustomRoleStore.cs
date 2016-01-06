// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomRoleStore.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomRoleStore class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.CustomIdentity
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.DataService;
    using Elephant.Hank.Resources.Attributes;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The CustomRoleStore class
    /// </summary>
    public class CustomRoleStore : RoleStore<CustomRole, long, CustomUserRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRoleStore"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public CustomRoleStore(AuthContext ctx)
            : base(ctx)
        {
        }

        /// <summary>
        /// Gets or sets the log tracker.
        /// </summary>
        [NotMapped]
        [EfIgnoreDbLog]
        public Guid LogTracker { get; set; }
    }
}
