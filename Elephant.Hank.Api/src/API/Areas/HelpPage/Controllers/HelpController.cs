// ---------------------------------------------------------------------------------------------------
// <copyright file="HelpController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The HelpController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Areas.HelpPage.Controllers
{
    using System.Web.Http;
    using System.Web.Mvc;
    using Elephant.Hank.Api.Areas.HelpPage.Models;

    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpController"/> class.
        /// </summary>
        public HelpController() : this(GlobalConfiguration.Configuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpController"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public HelpController(HttpConfiguration config)
        {
            this.Configuration = config;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public HttpConfiguration Configuration { get; private set; }

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = this.Configuration.Services.GetDocumentationProvider();
            return this.View(this.Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        /// <summary>
        /// APIs the specified API identifier.
        /// </summary>
        /// <param name="apiId">The API identifier.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = this.Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return this.View(apiModel);
                }
            }

            return this.View("Error");
        }
    }
}