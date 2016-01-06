// ---------------------------------------------------------------------------------------------------
// <copyright file="IdentityRoleObj.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The IdentityRoleObj class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Test.Common.Objects
{
    using System;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The identity role obj.
    /// </summary>
    public class IdentityRoleObj
    {
        /// <summary>
        /// The get identity role.
        /// </summary>
        /// <returns>
        /// The <see cref="IdentityRole"/>.
        /// </returns>
        public static IdentityRole GetIdentityRole()
        {
            return new IdentityRole
                       {
                           Id = Guid.NewGuid().ToString(),
                           Name = "TestRole"
                       };
        }
    }
}