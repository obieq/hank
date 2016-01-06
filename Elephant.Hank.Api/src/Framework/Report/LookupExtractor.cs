// ---------------------------------------------------------------------------------------------------
// <copyright file="LookupExtractor.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-25</date>
// <summary>
//     The LookupExtractor class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Report
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.Report;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The LookupExtractor class
    /// </summary>
    public class LookupExtractor : ILookupExtractor
    {
        /// <summary>
        /// The file folder helper
        /// </summary>
        private readonly IFileFolderHelper fileFolderHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupExtractor" /> class.
        /// </summary>
        /// <param name="fileFolderHelper">The file folder helper.</param>
        public LookupExtractor(IFileFolderHelper fileFolderHelper)
        {
            this.fileFolderHelper = fileFolderHelper;
        }

        /// <summary>
        /// Gets the browser name.
        /// </summary>
        /// <param name="reportFolders">The report folders.</param>
        /// <returns>Browser name look up from IO</returns>
        public IEnumerable<NameValuePair> GetBrowserNames(IEnumerable<NameValuePair> reportFolders)
        {
            var browserList = new List<NameValuePair>();

            reportFolders.ToList().ForEach(folderName => browserList.AddRange(this.fileFolderHelper.GetAllSubFolder(folderName.Value)));

            browserList.ForEach(
                browser =>
                {
                    browser.Name = browser.Name.ToBrowserName();
                    browser.Value = browser.Name;
                });

            browserList.Insert(0, new NameValuePair { Name = "All Browsers", Value = string.Empty });

            return browserList.GroupBy(x => x.Name).Select(x => x.First());
        }

        /// <summary>
        /// Gets the operating system names.
        /// </summary>
        /// <param name="reportFolders">The report folders.</param>
        /// <returns>OS names</returns>
        public IEnumerable<NameValuePair> GetOperatingSystemNames(IEnumerable<NameValuePair> reportFolders)
        {
            var operatingSystem = new List<NameValuePair>();

            reportFolders.ToList().ForEach(folderName => operatingSystem.AddRange(this.fileFolderHelper.GetAllSubFolder(folderName.Value)));

            operatingSystem.ForEach(
                os =>
                {
                    os.Name = os.Name.ToOperatingSystemName();
                    os.Value = os.Name;
                });

            operatingSystem.Insert(0, new NameValuePair { Name = "All OS", Value = string.Empty });

            return operatingSystem.GroupBy(x => x.Name).Select(x => x.First());
        }

        /// <summary>
        /// Gets the execution slots.
        /// </summary>
        /// <param name="reportFolders">The report folders.</param>
        /// <returns>Execution slots</returns>
        public IEnumerable<NameValuePair> GetExecutionSlots(IEnumerable<NameValuePair> reportFolders)
        {
            var executionSlots = new List<NameValuePair>();

            reportFolders.ToList().ForEach(folderName => executionSlots.Add(new NameValuePair { Name = folderName.Name, Value = folderName.Value }));

            executionSlots.ForEach(
                ts =>
                {
                    ts.Name = ts.Value.ToExecutionSlot();
                    ts.Value = ts.Name;
                });

            executionSlots.Insert(0, new NameValuePair { Name = "All", Value = string.Empty });

            return executionSlots.GroupBy(x => x.Name).Select(x => x.First());
        }
    }
}
