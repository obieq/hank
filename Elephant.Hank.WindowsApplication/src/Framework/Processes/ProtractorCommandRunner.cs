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
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Management;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum;
    using Elephant.Hank.WindowsApplication.Resources.Constants;
    using Elephant.Hank.WindowsApplication.Resources.Extensions;

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
        /// <returns>SchedulerExecutionStatus status</returns>
        public SchedulerExecutionStatus ExecuteCommand(string groupName)
        {
            try
            {
                var settings = SettingsHelper.Get();

                string cmdProtractorRunner = string.Format(command, settings.BaseTestPath, groupName);

                LoggerService.LogInformation("Executing: " + cmdProtractorRunner);

                var process = Process.Start("CMD.exe", "/C " + cmdProtractorRunner);

                var maxExecutionTime = ConfigurationManager.AppSettings[ConfigConstants.ProcessKillTimeMinutes].ToInt32(180) * 60 * 1000;

                var exitSuccess = process.WaitForExit(maxExecutionTime);

                if (!exitSuccess)
                {
                    this.KillProcessAndChildren(process.Id);
                    LoggerService.LogInformation("Force exit: " + cmdProtractorRunner);

                    return SchedulerExecutionStatus.ForcilyTerminatedAtTimeOut;
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
                return SchedulerExecutionStatus.ErrorWhileExecuting;
            }

            return SchedulerExecutionStatus.Completed;
        }

        /// <summary>
        /// Kills the process and children.
        /// </summary>
        /// <param name="pid">The pid.</param>
        private void KillProcessAndChildren(int pid)
        {
            using (var searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid))
            {
                var moc = searcher.Get();

                foreach (var mo in moc.Cast<ManagementObject>())
                {
                    this.KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
                }

                try
                {
                    var proc = Process.GetProcessById(pid);
                    proc.Kill();
                }
                catch (Exception e)
                {
                    // Process already exited.
                }
            }
        }
    }
}
