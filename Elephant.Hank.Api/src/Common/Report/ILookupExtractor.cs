// ---------------------------------------------------------------------------------------------------
// <copyright file="ILookupExtractor.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-15</date>
// <summary>
//     The ILookupExtractor interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.Report
{
    using System.Collections.Generic;

    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The ILookupExtractor interface
    /// </summary>
    public interface ILookupExtractor
    {
        /// <summary>
        /// Gets the browser name.
        /// </summary>
        /// <param name="reportFolders">The report folders.</param>
        /// <returns>Browser name look up from IO</returns>
        IEnumerable<NameValuePair> GetBrowserNames(IEnumerable<NameValuePair> reportFolders);

        /// <summary>
        /// Gets the operating system names.
        /// </summary>
        /// <param name="reportFolders">The report folders.</param>
        /// <returns>OS names</returns>
        IEnumerable<NameValuePair> GetOperatingSystemNames(IEnumerable<NameValuePair> reportFolders);

        /// <summary>
        /// Gets the execution slots.
        /// </summary>
        /// <param name="reportFolders">The report folders.</param>
        /// <returns>Execution slots</returns>
        IEnumerable<NameValuePair> GetExecutionSlots(IEnumerable<NameValuePair> reportFolders);
    }
}