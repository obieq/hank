// ---------------------------------------------------------------------------------------------------
// <copyright file="SettingsHelper.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The SettingsHelper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Helpers
{
    using Elephant.Hank.WindowsApplication.Framework.Properties;
    using Elephant.Hank.WindowsApplication.Resources;

    /// <summary>
    /// The SettingsHelper class
    /// </summary>
    public static class SettingsHelper
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>SettingsModel object</returns>
        public static SettingsModel Get()
        {
            return new SettingsModel
                   {
                       ExecutionTimeLapseInMilli = Settings.Default.ExecutionTimeLapseInMilli,
                       BaseApiUrl = Settings.Default.BaseApiUrl,
                       FrameworkBasePath = Settings.Default.FrameworkBasePath,
                       BaseWebUrl = Settings.Default.BaseWebUrl,

                       ClearLogHours = Settings.Default.ClearLogHours,
                       ClearReportHours = Settings.Default.ClearReportHours,
                       CleanerRunAtHour = Settings.Default.CleanerRunAtHour
                   };
        }

        /// <summary>
        /// Saves the specified settings model.
        /// </summary>
        /// <param name="settingsModel">The settings model.</param>
        /// <returns>SettingsModel object</returns>
        public static SettingsModel Save(SettingsModel settingsModel)
        {
            Settings.Default.ExecutionTimeLapseInMilli = settingsModel.ExecutionTimeLapseInMilli;
            Settings.Default.BaseApiUrl = settingsModel.BaseApiUrl;
            Settings.Default.FrameworkBasePath = settingsModel.FrameworkBasePath;
            Settings.Default.BaseWebUrl = settingsModel.BaseWebUrl;

            Settings.Default.CleanerRunAtHour = settingsModel.CleanerRunAtHour;
            Settings.Default.ClearLogHours = settingsModel.ClearLogHours;
            Settings.Default.ClearReportHours = settingsModel.ClearReportHours;

            Settings.Default.Save();

            return settingsModel;
        }
    }
}
