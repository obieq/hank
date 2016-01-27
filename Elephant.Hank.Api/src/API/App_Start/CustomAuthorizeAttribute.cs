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
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// The CustomAuthorizeAttribute class
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Gets or sets the Roles
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the module name
        /// </summary>
        public FrameworkModules ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the module name
        /// </summary>
        public ActionTypes ActionType { get; set; }

        /// <summary>
        /// calls while authorising the user
        /// </summary>
        /// <param name="filterContext">The filter context</param>
        /// <returns>returns the Authorization status</returns>
        protected override bool IsAuthorized(HttpActionContext filterContext)
        {
            if (base.IsAuthorized(filterContext))
            {
                var principal = filterContext.RequestContext.Principal as ClaimsPrincipal;
                string userRole = principal.FindFirst("role").Value;
                if (userRole == FrameworkRoles.TestAdmin.ToString() || userRole == FrameworkRoles.WindowService.ToString())
                {
                    return true;
                }
                else if (userRole == FrameworkRoles.TestUser.ToString())
                {
                    var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    string[] rolesArray = this.Role.Split(',');
                    if (rolesArray.Contains(userRole))
                    {
                        if (controller.ToLower() == "website" && this.ActionType == ActionTypes.Read)
                        {
                            return true;
                        }
                        else
                        {
                            long userId = long.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
                            var moduleAccessService = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IGroupModuleAccessService>();
                            ResultMessage<IEnumerable<ModuleAuthenticationModel>> authenticatedModules = moduleAccessService.GetModuleAuthenticatedToUser(userId);
                            List<ModuleAuthenticationModel> modules = authenticatedModules.Item.ToList();

                            if (modules != null)
                            {
                                object website_Id;
                                filterContext.RequestContext.RouteData.Values.TryGetValue("websiteId", out website_Id);
                                if (website_Id != null)
                                {
                                    int websiteId = 0;
                                    int.TryParse(website_Id.ToString(), out websiteId);
                                    List<ModuleAuthenticationModel> check;
                                    switch (this.ActionType)
                                    {
                                        case ActionTypes.Read:
                                            {
                                                check = modules.Where(x => x.WebsiteId == websiteId).ToList();
                                                if (check.Count > 0)
                                                {
                                                    return true;
                                                }

                                                break;
                                            }

                                        case ActionTypes.Write:
                                            {
                                                check = modules.Where(x => x.ModuleId == (int)this.ModuleId && x.WebsiteId == websiteId && x.CanWrite).ToList();
                                                if (check.Count > 0)
                                                {
                                                    return true;
                                                }

                                                break;
                                            }

                                        case ActionTypes.Execute:
                                            {
                                                check = modules.Where(x => x.ModuleId == (int)this.ModuleId && x.WebsiteId == websiteId && x.CanExecute).ToList();
                                                if (check.Count > 0)
                                                {
                                                    return true;
                                                }

                                                break;
                                            }

                                        case ActionTypes.Delete:
                                            {
                                                check = modules.Where(x => x.ModuleId == (int)this.ModuleId && x.WebsiteId == websiteId && x.CanDelete).ToList();
                                                if (check.Count > 0)
                                                {
                                                    return true;
                                                }

                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}