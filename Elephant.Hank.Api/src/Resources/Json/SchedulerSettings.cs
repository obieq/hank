// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerSettings.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The SchedulerSettings class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The SchedulerSettings class
    /// </summary>
    public class SchedulerSettings
    {
        /// <summary>
        /// Gets or sets to email ids.
        /// </summary>
        public string ToEmailIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [do email report].
        /// </summary>
        public bool DoEmailReport { get; set; }

        /// <summary>
        /// Gets or sets the selenium address.
        /// </summary>
        public string SeleniumAddress { get; set; }
        
        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        public long[] Browsers { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotOnUrlChanged
        /// </summary>
        public long TakeScreenShotOnUrlChanged { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotOnUrlChangedTestId
        /// </summary>
        public long TakeScreenShotOnUrlChangedTestId { get; set; }

        /// <summary>
        /// Gets or sets the TakeScreenShotOnUrlChangedSuiteId
        /// </summary>
        public long TakeScreenShotOnUrlChangedSuiteId { get; set; }
    }
}
