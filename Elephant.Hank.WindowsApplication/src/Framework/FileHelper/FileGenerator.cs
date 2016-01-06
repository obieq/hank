// ---------------------------------------------------------------------------------------------------
// <copyright file="FileGenerator.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-11</date>
// <summary>
//     The FileGenerator class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.FileHelper
{
    using System.Collections.Generic;
    using System.IO;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels;

    /// <summary>
    /// The FileGenerator class
    /// </summary>
    public class FileGenerator
    {
        /// <summary>
        /// List of all Queue test
        /// </summary>
        private IEnumerable<TestQueue> testList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileGenerator"/> class.
        /// </summary>
        /// <param name="testList">list of all queued test</param>
        public FileGenerator(IEnumerable<TestQueue> testList)
        {
            this.testList = testList;
        }

        /// <summary>
        /// Generate specs file for each test
        /// </summary>
        /// <returns>returns the name of the directory generated</returns>
        public string GenerateSpecFiles()
        {
            string path = null;

            var settings = SettingsHelper.Get();

            foreach (var item in this.testList)
            {
                TemplateEngine templateEngine = new TemplateEngine("SpecSample.txt");

                templateEngine.Variables.Add("TestName", item.TestName);
                templateEngine.Variables.Add("TestQueueId", item.Id);

                string generatedTemplate = templateEngine.GetFileContent();
                path = settings.BaseSpecPath + "\\" + item.GroupName;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                File.WriteAllText(path + "\\TestQueue_" + item.Id + ".js", generatedTemplate);
            }

            return path;
        }
    }
}
