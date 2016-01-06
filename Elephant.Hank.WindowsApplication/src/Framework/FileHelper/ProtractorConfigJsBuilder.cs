// ---------------------------------------------------------------------------------------------------
// <copyright file="ProtractorConfigJsBuilder.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-10</date>
// <summary>
//     The ProtractorConfigJsBuilder class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.FileHelper
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels;

    public class ProtractorConfigJsBuilder
    {
        /// <summary>
        /// The tr start tag
        /// </summary>
        private const string MulticapBrowserStartTag = "##Browser-Repeat-Start##";

        /// <summary>
        /// The tr end tag
        /// </summary>
        private const string MulticapBrowserEndTag = "##Browser-Repeat-End##";

        /// <summary>
        /// Creates the specified test list.
        /// </summary>
        /// <param name="testQueue">The test queue.</param>
        /// <returns>
        /// Location for protractor.conf.js file
        /// </returns>
        public string Create(TestQueue testQueue)
        {
            var baseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            baseLocation = Path.Combine(baseLocation, "Template", "protractor.conf.js");

            var jsContent = this.ReadFile(baseLocation);
            jsContent = jsContent.Replace("##SeleniumAddress##", testQueue.Settings.SeleniumAddress)
                                 .Replace("##BaseApiUrl##", SettingsHelper.Get().BaseApiUrl)
                                 .Replace("##CurLocation##", testQueue.GroupName);

            int startIndex = jsContent.IndexOf(MulticapBrowserStartTag) + MulticapBrowserStartTag.Length;
            int length = jsContent.IndexOf(MulticapBrowserEndTag) - startIndex;

            var browserDetailsJson = jsContent.Substring(startIndex, length);

            if (testQueue.Browsers != null)
            {
                jsContent = jsContent.Replace("##BrowserDetails##", this.GetBrowserDetailsJson(browserDetailsJson, testQueue.Browsers));
            }

            jsContent = jsContent.Replace(browserDetailsJson, string.Empty).Replace(MulticapBrowserStartTag, string.Empty).Replace(MulticapBrowserEndTag, string.Empty);

            var path = SettingsHelper.Get().BaseSpecPath + "\\" + testQueue.GroupName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(path + "\\protractor.conf.js", jsContent);

            return path;
        }

        /// <summary>
        /// Gets the browser details json.
        /// </summary>
        /// <param name="browserDetailsJson">The browser details json.</param>
        /// <param name="lstBrowsers">The LST browsers.</param>
        /// <returns>
        /// Browser details json for config file
        /// </returns>
        private string GetBrowserDetailsJson(string browserDetailsJson, IEnumerable<Browsers> lstBrowsers)
        {
            StringBuilder sbDataRow = new StringBuilder();

            foreach (var browser in lstBrowsers)
            {
                sbDataRow.Append(browserDetailsJson.Replace("##Platform##", browser.Platform)
                                                   .Replace("##BrowserName##", browser.ConfigName)
                                                   .Replace("##Version##", browser.Version + string.Empty)
                                                   .Replace("##ShardTestFiles##", browser.ShardTestFiles.ToString().ToLower())
                                                   .Replace("##MaxInstances##", (browser.MaxInstances <= 0 ? 1 : browser.MaxInstances).ToString()) + ",");
            }

            return sbDataRow.Length > 0 ? sbDataRow.ToString().Substring(0, sbDataRow.Length - 1) : string.Empty;
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
