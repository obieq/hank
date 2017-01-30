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
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels;

    public class ProtractorConfigJsBuilder
    {
        /// <summary>
        /// Creates the specified test list.
        /// </summary>
        /// <param name="testQueue">The test queue.</param>
        /// <param name="numberOfTest">The number of test.</param>
        /// <returns>
        /// Location for protractor.conf.js file
        /// </returns>
        public string Create(TestQueue testQueue, int numberOfTest)
        {
            var baseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            baseLocation = Path.Combine(baseLocation, "Template", "protractor.conf.js");

            var jsContent = this.ReadFile(baseLocation);
            jsContent = jsContent.Replace("##SeleniumAddress##", testQueue.Settings.SeleniumAddress)
                                 .Replace("##BaseApiUrl##", SettingsHelper.Get().BaseApiUrl)
                                 .Replace("##CurLocation##", testQueue.GroupName);

            if (testQueue.Browsers != null)
            {
                jsContent = jsContent.Replace("##BrowserDetails##", this.GetBrowserDetailsJson(testQueue.Browsers, numberOfTest));
                jsContent = jsContent.Replace("##IsMobileBrowser##", testQueue.Browsers.Any(x => x.IsMobile).ToString().ToLower());
            }

            var path = SettingsHelper.Get().BaseSpecPath + "\\" + testQueue.GroupName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(path + "\\protractor.conf.js", jsContent);

            return path;
        }

        /// <summary>
        /// Gets the browser details JSON.
        /// </summary>
        /// <param name="lstBrowsers">The LST browsers.</param>
        /// <param name="numberOfTest">The number of test.</param>
        /// <returns>
        /// Browser details JSON for config file
        /// </returns>
        private string GetBrowserDetailsJson(IEnumerable<Browsers> lstBrowsers, int numberOfTest)
        {
            StringBuilder sbDataRow = new StringBuilder();

            foreach (var browser in lstBrowsers)
            {
                var browserProperties = browser.Properties ?? new List<NameValuePair>();

                browserProperties.Add(new NameValuePair { Name = "platform", Value = browser.Platform });
                browserProperties.Add(new NameValuePair { Name = "browserName", Value = browser.ConfigName });
                browserProperties.Add(new NameValuePair { Name = "shardTestFiles", Value = browser.ShardTestFiles.ToString().ToLower() });
                browserProperties.Add(new NameValuePair { Name = "maxInstances", Value = numberOfTest <= 1 ? "1" : ((browser.MaxInstances <= 0 ? 1 : browser.MaxInstances).ToString()) });

                if (!browser.IsMobile)
                {
                    browserProperties.Add(new NameValuePair { Name = "version", Value = browser.Version + string.Empty });
                }
                
                string extraProperties = string.Empty;

                foreach (var property in browserProperties)
                {
                    extraProperties += "\n\"" + property.Name + "\": \"" + property.Value + "\",";
                }

                extraProperties = extraProperties.Length > 0
                                      ? extraProperties.Substring(0, extraProperties.Length - 1)
                                      : string.Empty;


                sbDataRow.Append("{" + extraProperties + "},");
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
