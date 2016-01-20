// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomAuthorizeAttribute.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-20</date>
// <summary>
//     The CustomAuthorizeAttribute class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.App_Start
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    /// <summary>
    /// The CustomAuthorizeAttribute class
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Gets or sets the module name
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// calls while authorising the user
        /// </summary>
        /// <param name="filterContext"> The filter context</param>
        protected override bool IsAuthorized(HttpActionContext filterContext)
        {
            if (base.IsAuthorized(filterContext))
            {
                return true;
            }

            return false;
        }
    }
}