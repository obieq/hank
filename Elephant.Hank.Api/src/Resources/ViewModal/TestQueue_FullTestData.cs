// ---------------------------------------------------------------------------------------------------
// <copyright file="TestQueue_FullTestData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-25</date>
// <summary>
//     The TestQueue_FullTestData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.ViewModal
{
    using System.Collections.Generic;

    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TestQueue_FullTestData class
    /// </summary>
    public class TestQueue_FullTestData
    {
        /// <summary>
        /// Gets or sets the URL to test.
        /// </summary>
        public string UrlToTest { get; set; }

        /// <summary>
        /// Gets or sets the website
        /// </summary>
        public TblWebsiteDto Website { get; set; }

        /// <summary>
        /// Gets or sets the suite.
        /// </summary>
        public TblSuiteDto Suite { get; set; }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        public TblTestDto TestCase { get; set; }

        /// <summary>
        /// Gets or sets the test data.
        /// </summary>
        public List<ExecutableTestData> TestData { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotBrowser
        /// </summary>
        public TblBrowsersDto TakeScreenShotBrowser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [take screen shot].
        /// </summary>
        public bool TakeScreenShot { get; set; }
    }
}
