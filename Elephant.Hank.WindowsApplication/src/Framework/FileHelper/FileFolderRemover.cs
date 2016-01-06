// ---------------------------------------------------------------------------------------------------
// <copyright file="FileFolderRemover.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-08</date>
// <summary>
//     The FileFolderRemover class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.FileHelper
{
    using System;
    using System.IO;
    using System.Threading;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;

    /// <summary>
    /// The FileFolderRemover class
    /// </summary>
    public class FileFolderRemover
    {
        /// <summary>
        /// The retries on error
        /// </summary>
        private const int RetriesOnError = 3;

        /// <summary>
        /// The delay on retry
        /// </summary>
        private const int DelayOnRetry = 1000;

        /// <summary>
        /// Deletes the old files folders.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="maximumAgeInHours">The maximum age in hours.</param>
        public static void DeleteOldFilesFolders(string folderPath, uint maximumAgeInHours)
        {
                DeleteOldFolders(folderPath, maximumAgeInHours);

                DeleteOldFiles(folderPath, maximumAgeInHours);
        }

        public static void DeleteOldFolders(string folderPath, uint maximumAgeInHours)
        {
            if (!new DirectoryInfo(folderPath).Exists)
            {
                return;
            }

            LoggerService.LogInformation(string.Format("Cleaning folder {0} location at {1} hours setting!", folderPath, maximumAgeInHours));

            DateTime minimumDate = DateTime.Now.AddHours(-maximumAgeInHours);

            foreach (var dir in Directory.EnumerateDirectories(folderPath))
            {
                for (int i = 0; i < RetriesOnError; i++)
                {
                    try
                    {
                        var curDir = new DirectoryInfo(dir);

                        if (curDir.Exists && curDir.CreationTime < minimumDate)
                        {
                            curDir.Delete(true);
                        }
                        break;
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(DelayOnRetry);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the old files.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        /// <param name="maximumAgeInHours">The maximum age in hours.</param>
        public static void DeleteOldFiles(string folderPath, uint maximumAgeInHours)
        {
            if (!new DirectoryInfo(folderPath).Exists)
            {
                return;
            }

            LoggerService.LogInformation(string.Format("Cleaning files {0} location at {1} hours setting!", folderPath, maximumAgeInHours));

            DateTime minimumDate = DateTime.Now.AddHours(-maximumAgeInHours);

            foreach (var path in Directory.EnumerateFiles(folderPath))
            {
                DeleteFileIfOlderThan(path, minimumDate);
            }
        }

        /// <summary>
        /// Deletes the file if older than.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="date">The date.</param>
        /// <returns>true if all goes fine</returns>
        private static void DeleteFileIfOlderThan(string path, DateTime date)
        {
            FileInfo file = new FileInfo(path);

            if (file.Exists && file.CreationTime < date)
            {
                DeleteFile(path);
            }
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void DeleteFile(string path)
        {
            for (int i = 0; i < RetriesOnError; ++i)
            {
                try
                {
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    
                    return;
                }
                catch (IOException)
                {
                    Thread.Sleep(DelayOnRetry);
                }
                catch (UnauthorizedAccessException)
                {
                    Thread.Sleep(DelayOnRetry);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
