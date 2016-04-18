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

namespace Elephant.Hank.Api.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The CustomAuthorizeAttribute class
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Gets or sets the module name
        /// </summary>
        public FrameworkModules ModuleType { get; set; }

        /// <summary>
        /// Gets or sets the module name
        /// </summary>
        public ActionTypes ActionType { get; set; }

        /// <summary>
        /// calls while authorizing the user
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

                if (userRole == FrameworkRoles.TestUser.ToString())
                {
                    var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    if (controller.ToLower() == "website" && this.ActionType == ActionTypes.Read)
                    {
                        return true;
                    }

                    if (controller.ToLower() == "userprofile")
                    {
                        return true;
                    }

                    long userId = long.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var moduleAccessService = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IGroupModuleAccessService>();
                    var testService = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<ITestService>();
                    ResultMessage<IEnumerable<ModuleAuthenticationModel>> authenticatedModules = moduleAccessService.GetModuleAuthenticatedToUser(userId);
                    List<ModuleAuthenticationModel> modules = authenticatedModules.Item.ToList();

                    if (modules.Any())
                    {
                        object website_Id;
                        object test_Id;
                        filterContext.RequestContext.RouteData.Values.TryGetValue("websiteId", out website_Id);
                        filterContext.RequestContext.RouteData.Values.TryGetValue("testId", out test_Id);
                        if (website_Id != null)
                        {
                            int websiteId = website_Id.ToString().ToInt32();
                            int testId = 0;
                            if (test_Id != null)
                            {
                                testId = test_Id.ToString().ToInt32();
                            }

                            if (websiteId > 0)
                            {
                                switch (this.ActionType)
                                {
                                    case ActionTypes.Read:
                                        {
                                            if (testId > 0)
                                            {
                                                var test = testService.GetById(testId).Item;
                                                return modules.Any(x => x.WebsiteId == websiteId) && (test.TestCaseAccessStatus != (int)TestCaseAccessStatus.Private || test.CreatedBy == userId);
                                            }
                                            else
                                            {
                                                return modules.Any(x => x.WebsiteId == websiteId);
                                            }
                                        }

                                    case ActionTypes.Write:
                                        {
                                            if (testId > 0)
                                            {
                                                var test = testService.GetById(testId).Item;
                                                return modules.Any(x => x.ModuleId == this.ModuleType && x.WebsiteId == websiteId && x.CanWrite) && (test.TestCaseAccessStatus == (int)TestCaseAccessStatus.Public || test.CreatedBy == userId);
                                            }
                                            else
                                            {
                                                return modules.Any(x => x.ModuleId == this.ModuleType && x.WebsiteId == websiteId && x.CanWrite);
                                            }
                                        }

                                    case ActionTypes.Execute:
                                        {
                                            if (testId > 0)
                                            {
                                                var test = testService.GetById(testId, userId).Item;
                                                return modules.Any(x => x.ModuleId == this.ModuleType && x.WebsiteId == websiteId && x.CanExecute) && (test.TestCaseAccessStatus == (int)TestCaseAccessStatus.Public || test.CreatedBy == userId);
                                            }
                                            else
                                            {
                                                return modules.Any(x => x.ModuleId == this.ModuleType && x.WebsiteId == websiteId && x.CanExecute);
                                            }
                                        }

                                    case ActionTypes.Delete:
                                        {
                                            if (testId > 0)
                                            {
                                                var test = testService.GetById(testId, userId).Item;
                                                return modules.Any(x => x.ModuleId == this.ModuleType && x.WebsiteId == websiteId && x.CanDelete) && (test.TestCaseAccessStatus == (int)TestCaseAccessStatus.Public || test.CreatedBy == userId);
                                            }
                                            else
                                            {
                                                return modules.Any(x => x.ModuleId == this.ModuleType && x.WebsiteId == websiteId && x.CanDelete);
                                            }
                                        }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}