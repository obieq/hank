// ---------------------------------------------------------------------------------------------------
// <copyright file="FileFolderHelper.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-25</date>
// <summary>
//     The FileFolderHelper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Report
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Elephant.Hank.Common.Report;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The FileFolderHelper class
    /// </summary>
    public class FileFolderHelper : IFileFolderHelper
    {
        /// <summary>
        /// The base folder
        /// </summary>
        private string baseFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileFolderHelper"/> class.
        /// </summary>
        /// <param name="baseFolder">The base folder.</param>
        public FileFolderHelper(string baseFolder)
        {
            this.baseFolder = baseFolder;
        }

        /// <summary>
        /// Gets the folder by date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// NameValuePair for folders
        /// </returns>
        public IEnumerable<NameValuePair> GetFolderByDate(DateTime date)
        {
            var resultData = new List<NameValuePair>();

            if (Directory.Exists(this.baseFolder))
            {
                string targetDate = date.ToString("dd-MM-yyyy");

                var allFolders = Directory.GetDirectories(this.baseFolder);

                resultData.AddRange(from folder in allFolders where folder.Contains(targetDate) select new NameValuePair { Name = targetDate, Value = folder });
            }

            return resultData;
        }

        /// <summary>
        /// Gets all sub folder.
        /// </summary>
        /// <param name="dirLocation">The directory location.</param>
        /// <returns>Sub folders</returns>
        public IEnumerable<NameValuePair> GetAllSubFolder(string dirLocation)
        {
            var resultData = new List<NameValuePair>();

            if (Directory.Exists(dirLocation))
            {
                var allFolders = Directory.GetDirectories(dirLocation);

                resultData.AddRange(allFolders.Select(folderLocation => new NameValuePair
                                                         {
                                                             Name = folderLocation.Replace(dirLocation + Path.DirectorySeparatorChar, string.Empty),
                                                             Value = folderLocation
                                                         }));
            }

            return resultData;
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="fileLocation">The file location.</param>
        /// <returns>file content</returns>
        public string ReadFile(string fileLocation)
        {
            string retValue = null;

            var fileinfo = new FileInfo(fileLocation);
            if (fileinfo.Exists)
            {
                retValue = File.ReadAllText(fileLocation);
            }

            return retValue;
        }
    }
}
