// ---------------------------------------------------------------------------------------------------
// <copyright file="ApiDescriptionExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The ApiDescriptionExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Areas.HelpPage
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Http.Description;

    /// <summary>
    /// The ApiDescriptionExtensions class
    /// </summary>
    public static class ApiDescriptionExtensions
    {
        /// <summary>
        /// Generates an URI-friendly ID for the <see cref="ApiDescription"/>. E.g. "Get-Values-id_name" instead of "GetValues/{id}?name={name}"
        /// </summary>
        /// <param name="description">The <see cref="ApiDescription"/>.</param>
        /// <returns>The ID as a string.</returns>
        public static string GetFriendlyId(this ApiDescription description)
        {
            string path = description.RelativePath;
            string[] urlParts = path.Split('?');
            string localPath = urlParts[0];
            string queryKeyString = null;
            if (urlParts.Length > 1)
            {
                string query = urlParts[1];
                string[] queryKeys = HttpUtility.ParseQueryString(query).AllKeys;
                queryKeyString = string.Join("_", queryKeys);
            }

            StringBuilder friendlyPath = new StringBuilder();
            friendlyPath.AppendFormat(
                "{0}-{1}",
                description.HttpMethod.Method,
                localPath.Replace("/", "-").Replace("{", string.Empty).Replace("}", string.Empty));

            if (queryKeyString != null)
            {
                friendlyPath.AppendFormat("_{0}", queryKeyString);
            }

            return friendlyPath.ToString();
        }
    }
}