// ---------------------------------------------------------------------------------------------------
// <copyright file="ImageProcessor.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-07-03</date>
// <summary>
//     The ImageProcessor class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Processes
{
    using System;
    using System.IO;

    using Elephant.Hank.WindowsApplication.Framework.FileHelper;
    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.Extensions;

    using Kaliko.ImageLibrary;
    using Kaliko.ImageLibrary.Scaling;

    /// <summary>
    /// The ImageProcessor class
    /// </summary>
    public static class ImageProcessor
    {
        /// <summary>
        /// Processes the images.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        public static void ProcessImages(string groupName)
        {
            try
            {
                var settings = SettingsHelper.Get();

                string fileFolder = settings.BaseReportPath + "\\" + groupName;

                var imgLocations = Directory.GetFiles(fileFolder, "*.png", SearchOption.AllDirectories);

                foreach (var imgLoc in imgLocations)
                {
                    using (KalikoImage image = new KalikoImage(imgLoc))
                    {
                        using (KalikoImage thumb = image.Scale(new FitScaling(200, 200)))
                        {
                            thumb.SaveJpg(imgLoc.ToJpgThumbFileName(), 90);
                        }

                        image.SaveJpg(imgLoc.ToJpgImageFileName(), 90);
                    }

                    FileFolderRemover.DeleteFile(imgLoc);
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
            }
        }
    }
}
