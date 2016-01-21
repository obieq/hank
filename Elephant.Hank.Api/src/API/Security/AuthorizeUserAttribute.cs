// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthorizeUserAttribute.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The AuthorizeUserAttribute class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Security
{
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Elephant.Hank.Resources.Constants;

    /// <summary>
    /// The AuthorizeUserAttribute class
    /// </summary>
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is admin role required.
        /// </summary>
        public bool IsAdminRoleRequired { get; set; }

        /// <summary>
        /// Indicates whether the specified control is authorized.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        /// <returns>
        /// true if the control is authorized; otherwise, false.
        /// </returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (this.IsAdminRoleRequired)
            {
                this.Roles = RoleName.TestAdminRole;
            }

            var isAuthorized = base.IsAuthorized(actionContext);

            return isAuthorized;
        }
    }
}