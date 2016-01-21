// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomUserManager.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomUserManager class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.CustomIdentity
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Attributes;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The custom user manager.
    /// </summary>
    public class CustomUserManager : UserManager<CustomUser, long>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomUserManager"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public CustomUserManager(CustomUserStore store)
            : base(store)
        {
            this.UserTokenProvider = new TokenProvider();
        }

        /// <summary>
        /// Gets or sets the log tracker.
        /// </summary>
        [NotMapped]
        [EfIgnoreDbLog]
        public Guid LogTracker { get; set; }
    }
}
