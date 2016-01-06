// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomRoleManager.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomRoleManager class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.CustomIdentity
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Attributes;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The CustomRoleManager class
    /// </summary>
    public class CustomRoleManager : RoleManager<CustomRole, long>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRoleManager"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public CustomRoleManager(CustomRoleStore store)
            : base(store)
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
