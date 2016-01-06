// ---------------------------------------------------------------------------------------------------
// <copyright file="IFileFolderHelper.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-15</date>
// <summary>
//     The IFileFolderHelper interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.Report
{
    using System;
    using System.Collections.Generic;

    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The IFileFolderHelper interface
    /// </summary>
    public interface IFileFolderHelper
    {
        /// <summary>
        /// Gets the folder by date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        /// NameValuePair for folders
        /// </returns>
        IEnumerable<NameValuePair> GetFolderByDate(DateTime date);

        /// <summary>
        /// Gets all sub folder.
        /// </summary>
        /// <param name="dirLocation">The directory location.</param>
        /// <returns>Sub folders</returns>
        IEnumerable<NameValuePair> GetAllSubFolder(string dirLocation);

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="fileLocation">The file location.</param>
        /// <returns>file content</returns>
        string ReadFile(string fileLocation);
    }
}