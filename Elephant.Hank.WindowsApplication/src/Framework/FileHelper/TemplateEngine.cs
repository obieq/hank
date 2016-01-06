// ---------------------------------------------------------------------------------------------------
// <copyright file="TemplateEngine.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-11</date>
// <summary>
//     The TemplateEngine class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.FileHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// The TemplateEngine class
    /// </summary>
    public class TemplateEngine
    {
        #region Properties

        /// <summary>
        /// Name of the file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Left delimiter
        /// </summary>
        public string LDelimiter { get; set; }

        /// <summary>
        /// Right delimiter
        /// </summary>
        public string RDelimiter { get; set; }

        /// <summary>
        /// base directory of template
        /// </summary>
        public string TemplatesDir { get; set; }

        /// <summary>
        /// variables to be replaced in dynamic file content
        /// </summary>
        public IDictionary Variables { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// default constructor
        /// </summary>
        public TemplateEngine()
        {

            this.SetDefaultValues();
        }

        /// <summary>
        /// parameter constructor
        /// </summary>
        /// <param name="file"></param>
        public TemplateEngine(string file)
        {
            this.SetDefaultValues();
            this.FileName = file;
        }

        /// <summary>
        /// set values to mail
        /// </summary>
        private void SetDefaultValues()
        {
            this.TemplatesDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.TemplatesDir = Path.Combine(this.TemplatesDir, "Template");

            this.LDelimiter = "##";
            this.RDelimiter = "##";
            this.Variables = new Dictionary<string, object>();
        }

        /// <summary>
        /// get template file
        /// </summary>
        /// <returns>return string value</returns>
        public string GetTempFileContent(string content)
        {
            string temp_content = content;

            IDictionaryEnumerator e = this.Variables.GetEnumerator();

            while (e.MoveNext())
            {
                temp_content = temp_content.Replace(this.LDelimiter + e.Key + this.RDelimiter, e.Value.ToString());
            }
            return temp_content;
        }


        /// <summary>
        /// get template file
        /// </summary>
        /// <returns>return string value</returns>
        public string GetFileContent()
        {
            string temp_content = "";
            StreamReader sr = new StreamReader(this.TemplatesDir + "\\" + this.FileName);
            try
            {
                temp_content = sr.ReadToEnd();
            }
            catch (Exception)
            {
            }
            finally
            {
                sr.Close();
            }

            IDictionaryEnumerator e = this.Variables.GetEnumerator();

            while (e.MoveNext())
            {
                temp_content = temp_content.Replace(this.LDelimiter + e.Key + this.RDelimiter, e.Value.ToString());
            }
            return temp_content;
        }

        /// <summary>
        /// get Content
        /// </summary>
        /// <returns>return string value</returns>
        public string GetContent()
        {
            string temp_content = "";
            try
            {
                temp_content = this.FileName;
            }
            catch (Exception)
            {
            }

            IDictionaryEnumerator e = this.Variables.GetEnumerator();

            while (e.MoveNext())
            {
                temp_content = temp_content.Replace(this.LDelimiter + e.Key + this.RDelimiter, e.Value.ToString());
            }
            return temp_content;
        }
        #endregion
    }

}
