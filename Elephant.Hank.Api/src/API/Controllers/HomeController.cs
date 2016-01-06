// ---------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The HomeController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System.IO;
    using System.Web.Mvc;

    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.Resources.Constants;

    /// <summary>
    /// The HomeController controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index page</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return this.View();
        }

        /// <summary>
        /// Gets my image.
        /// </summary>
        /// <param name="loc">The location.</param>
        /// <returns>Image content</returns>
        public ActionResult GetScreenShot(string loc)
        {
            var imageUrl = AppSettings.Get(ConfigConstants.ReportLocation, string.Empty) + "\\" + loc;

            imageUrl = imageUrl.ToLower().Replace(".png", ".jpg");

            if (imageUrl.Contains("-t200"))
            {
                if (!new FileInfo(imageUrl).Exists)
                {
                    imageUrl = imageUrl.Replace("-t200", string.Empty);
                }
            }

            // Return the original image if no thumb or big image found
            if (!new FileInfo(imageUrl).Exists)
            {
                imageUrl = imageUrl.ToLower().Replace(".jpg", ".png");
            }

            return this.File(imageUrl, "image/jpg");
        }
    }
}