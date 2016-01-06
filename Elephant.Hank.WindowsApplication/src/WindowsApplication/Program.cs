// ---------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-11</date>
// <summary>
//     The Program class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication
{
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Windows.Forms;

    using Elephant.Hank.WindowsApplication.Resources.Constants;

    /// <summary>
    /// The Program class
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The mutex
        /// </summary>
        static Mutex mutex = new Mutex(true, ConfigurationManager.AppSettings[ConfigConstants.EnvironmentName] ?? "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("Only one instance at a time!");
            }
        }
    }
}
