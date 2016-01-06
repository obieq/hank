// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomUserStore.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomUserStore class
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
    /// The CustomUserStore class
    /// </summary>
    public class CustomUserStore : UserStore<CustomUser, CustomRole, long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomUserStore"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public CustomUserStore(AuthContext ctx)
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
