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

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
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
    }
}
