// ---------------------------------------------------------------------------------------------------
// <copyright file="ProtractorCommandRunner.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-11</date>
// <summary>
//     The ProtractorCommandGenerator class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Processes
{
    using System;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;

    /// <summary>
    /// The ProtractorCommandRunner class
    /// </summary>
    public class ProtractorCommandRunner
    {
        /// <summary>
        /// Command template to be executed
        /// </summary>
        private const string command = "cd /d  \"{0}\" && protractor spec/{1}/protractor.conf.js  --params.config.curLocation=\"{1}\" --specs=\"spec/{1}/*.js\" > Logs/{1}.txt";

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        public void ExecuteCommand(string groupName, double maxExecutionTime)
        {
            try
            {
                var settings = SettingsHelper.Get();

                string cmdProtractorRunner = string.Format(
                    command,
                    settings.BaseTestPath,
                    groupName);

                LoggerService.LogInformation("Executing: " + cmdProtractorRunner);

                var process = System.Diagnostics.Process.Start("CMD.exe", "/C " + cmdProtractorRunner);
                process.WaitForExit(Int32.Parse(maxExecutionTime.ToString()));
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
            }
        }
    }
}
