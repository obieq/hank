﻿// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailBuilder.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-26</date>
// <summary>
//     The EmailBuilder class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.FileHelper
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Elephant.Hank.WindowsApplication.Resources.ApiModels;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum;
    using Elephant.Hank.WindowsApplication.Resources.Constants;
    using Elephant.Hank.WindowsApplication.Resources.Extensions;
    using Elephant.Hank.WindowsApplication.Resources.Models;

    /// <summary>
    /// The EmailBuilder class
    /// </summary>
    public class EmailBuilder
    {
        /// <summary>
        /// The tr start tag
        /// </summary>
        private const string TrStartTag = "##Tr-Repeat-Start##";

        /// <summary>
        /// The tr end tag
        /// </summary>
        private const string TrEndTag = "##Tr-Repeat-End##";

        /// <summary>
        /// Gets the report HTML.
        /// </summary>
        /// <param name="reportResultData">The report result data.</param>
        /// <returns>HTML for report</returns>
        public string GetReportHtml(ReportResultData reportResultData)
        {
            var baseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            baseLocation = Path.Combine(baseLocation, "Template", "emailHtml.html");

            var html = this.ReadFile(baseLocation);
            html = html.Replace("##Passed##", reportResultData.PassedCount.ToString())
                   .Replace("##Faulted##", reportResultData.FaultCount.ToString())
                   .Replace("##Total##", reportResultData.TotalCount.ToString())
                   .Replace("##Cancelled##", reportResultData.CancelledCount.ToString())
                   .Replace("##UnProcessed##", reportResultData.UnProcessedCount.ToString())
                   .Replace("##ReportUrl##", string.Format(Properties.Settings.Default.BaseWebUrl + WebEndPoints.ReportByWebSiteId, reportResultData.WebsiteId));

            int startIndex = html.IndexOf(TrStartTag) + TrStartTag.Length;
            int length = html.IndexOf(TrEndTag) - startIndex;

            var trHtml = html.Substring(startIndex, length);

            if (reportResultData.ReportData != null)
            {
                html = html.Replace("##UrlTested##", reportResultData.UrlTested);
                html = html.Replace("##Execution Group##", reportResultData.GroupName);

                var startDate = reportResultData.ReportData.Min(x => x.FinishedAtDateTime);
                var endDate = reportResultData.ReportData.Max(x => x.FinishedAtDateTime);

                html = html.Replace("##Execution Started##", startDate.ToDateEstFormat());
                html = html.Replace("##Execution Completed##", endDate.ToDateEstFormat());

                html = html.Replace("##Tr-Passed##", this.GetTrHtml(trHtml, reportResultData.ReportData.Where(x => x.Status == ExecutionReportStatus.Passed).OrderBy(x => x.SuiteId), reportResultData.WebsiteId));

                html = html.Replace("##Tr-Faulted##", this.GetTrHtml(trHtml, reportResultData.ReportData.Where(x => x.Status == ExecutionReportStatus.Failed).OrderBy(x => x.SuiteId), reportResultData.WebsiteId));

                html = html.Replace("##Tr-Cancelled##", this.GetTrHtml(trHtml, reportResultData.ReportData.Where(x => x.Status == ExecutionReportStatus.Cancelled).OrderBy(x => x.SuiteId), reportResultData.WebsiteId, false));

                html = html.Replace("##Tr-UnProcessed##", this.GetTrHtml(trHtml, reportResultData.ReportData.Where(x => x.Status != ExecutionReportStatus.Passed && x.Status != ExecutionReportStatus.Failed && x.Status != ExecutionReportStatus.Cancelled).OrderBy(x => x.SuiteId), reportResultData.WebsiteId, false));
            }

            html = html.Replace(trHtml, string.Empty).Replace(TrStartTag, string.Empty).Replace(TrEndTag, string.Empty);

            return html;
        }

        /// <summary>
        /// Gets the tr HTML.
        /// </summary>
        /// <param name="trTemplate">The tr template.</param>
        /// <param name="lstReportData">The LST report data.</param>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <param name="genrateViewLnk">if set to <c>true</c> [genrate view LNK].</param>
        /// <returns>
        /// HTML based on template
        /// </returns>
        private string GetTrHtml(string trTemplate, IEnumerable<ReportData> lstReportData, long webSiteId, bool genrateViewLnk = true)
        {
            StringBuilder sbDataRow = new StringBuilder();

            int count = 1;

            if (!genrateViewLnk)
            {
                trTemplate = trTemplate.Replace("<td><a href=\"##ReportUrlById##\">View</a></td>", string.Empty);
            }

            foreach (var reportData in lstReportData)
            {
                var color = "rgb(172, 148, 6)";
                if (reportData.Status == ExecutionReportStatus.Passed)
                {
                    color = "green";
                }
                else if (reportData.Status == ExecutionReportStatus.Failed)
                {
                    color = "red";
                }
                else if (reportData.Status == ExecutionReportStatus.Cancelled)
                {
                    color = "brown";
                }

                sbDataRow.Append(trTemplate.Replace("##Sno##", count.ToString())
                    .Replace("##Color##", color)
                        .Replace("##SuiteName##", reportData.SuiteName)
                        .Replace("##Test Case##", reportData.Description ?? reportData.TestName)
                        .Replace("##Completed(EST)##", reportData.FinishedAtDateTime.ToDateEstFormat())
                        .Replace("##Time Taken##", reportData.TimeTaken)
                        .Replace("##Operating System##", reportData.Os)
                        .Replace("##Browser Name##", reportData.BrowserName)
                        .Replace("##Browser Version##", reportData.BrowserVersion)
                        .Replace("##ReportUrlById##", string.Format(Properties.Settings.Default.BaseWebUrl + WebEndPoints.ReportByWebSiteAndReportId, webSiteId, reportData.Id)));

                count++;
            }

            return sbDataRow.ToString();
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="fileLocation">The file location.</param>
        /// <returns>File text</returns>
        private string ReadFile(string fileLocation)
        {
            string retValue = null;

            var fileinfo = new FileInfo(fileLocation);
            if (fileinfo.Exists)
            {
                retValue = File.ReadAllText(fileLocation);
            }

            return retValue;
        }
    }
}
