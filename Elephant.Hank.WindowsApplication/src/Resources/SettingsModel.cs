// ---------------------------------------------------------------------------------------------------
// <copyright file="SettingsModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The SettingsModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// The Settings class
    /// </summary>
    public class SettingsModel
    {
        /// <summary>
        /// Gets or sets the execution time lapse in milliseconds.
        /// </summary>
        public int ExecutionTimeLapseInMilli { get; set; }

        /// <summary>
        /// Gets or sets the cleaner run at hour.
        /// </summary>
        public int CleanerRunAtHour { get; set; }

        /// <summary>
        /// Gets the run cleaner in milliseconds.
        /// </summary>
        public int RunCleanerInMilliSec 
        {
            get
            {
                return (((this.CleanerRunAtHour <= 0 ? 24 : this.CleanerRunAtHour) * 60) * 60) * 1000;
            }
        }

        /// <summary>
        /// Gets or sets the clear log hours.
        /// </summary>
        public int ClearLogHours { get; set; }

        /// <summary>
        /// Gets or sets the clear report hours.
        /// </summary>
        public int ClearReportHours { get; set; }

        /// <summary>
        /// Gets or sets the base API URL.
        /// </summary>
        public string BaseApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the base web URL.
        /// </summary>
        public string BaseWebUrl { get; set; }

        /// <summary>
        /// Gets or sets the framework base path.
        /// </summary>
        public string FrameworkBasePath { get; set; }

        /// <summary>
        /// Gets the spec base path.
        /// </summary>
        public string BaseSpecPath 
        {
            get
            {
                return this.BaseTestPath + "\\spec";
            }
        }

        /// <summary>
        /// Gets the base test path.
        /// </summary>
        public string BaseTestPath
        {
            get
            {
                return this.FrameworkBasePath + "\\test";
            }
        }

        /// <summary>
        /// Gets the base test log path.
        /// </summary>
        public string BaseTestLogPath
        {
            get
            {
                return this.BaseTestPath + "\\Logs";
            }
        }

        /// <summary>
        /// Gets the base report path.
        /// </summary>
        public string BaseReportPath
        {
            get
            {
                return this.BaseTestPath + "\\reports";
            }
        }

        /// <summary>
        /// Gets the logs location.
        /// </summary>
        public string LogsLocation
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Logs";
            }
        }
    }
}
