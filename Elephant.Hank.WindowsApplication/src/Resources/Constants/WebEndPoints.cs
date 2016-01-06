// ---------------------------------------------------------------------------------------------------
// <copyright file="WebEndPoints.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-08</date>
// <summary>
//     The WebEndPoints class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.Constants
{
    /// <summary>
    /// The WebEndPoints class
    /// </summary>
    public static class WebEndPoints
    {
        /// <summary>
        /// The report by web site identifier
        /// </summary>
        public static string ReportByWebSiteId = "#/Website/{0}/Report";

        /// <summary>
        /// The report by web site and report identifier
        /// </summary>
        public static string ReportByWebSiteAndReportId = "#/Website/{0}/Report-Detail/{1}";
    }
}
