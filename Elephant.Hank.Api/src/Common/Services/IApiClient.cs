// ---------------------------------------------------------------------------------------------------
// <copyright file="IApiClient.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The IApiClient class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The IApiClient interface
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the custom headers.
        /// </summary>
        List<NameValuePair> CustomHeaders { get; set; }

        /// <summary>
        /// Gets the specified location.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="location">The location.</param>
        /// <returns>
        /// Object of type T
        /// </returns>
        Task<ResultMessage<T>> Get<T>(string location);

        /// <summary>
        /// Posts the specified relative path.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <typeparam name="Tin">The type of the in.</typeparam>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="data">The data.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="changeToPutReq">if set to <c>true</c> [change to put req].</param>
        /// <returns>
        /// Object of type T
        /// </returns>
        Task<ResultMessage<T>> Post<T, Tin>(string relativePath, Tin data, string contentType = null, bool changeToPutReq = false);
    }
}
