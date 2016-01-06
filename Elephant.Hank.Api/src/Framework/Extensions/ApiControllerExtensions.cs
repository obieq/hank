// ---------------------------------------------------------------------------------------------------
// <copyright file="ApiControllerExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-15</date>
// <summary>
//     The ApiControllerExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Extensions
{
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Framework.CustomApi;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The api controller extensions.
    /// </summary>
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Creates the custom response.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="apiController">The API controller.</param>
        /// <param name="content">The content.</param>
        /// <param name="statusCode">The status code.</param>
        /// <returns>
        /// Custom result
        /// </returns>
        public static CustomResult<T> CreateCustomResponse<T>(this ApiController apiController, T content, HttpStatusCode statusCode)
        {
            return new CustomResult<T>(content, apiController, statusCode);
        }

        /// <summary>
        /// Creates the custom response.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="apiController">The API controller.</param>
        /// <param name="content">The content.</param>
        /// <returns>Custom result</returns>
        public static CustomResult<ResultMessage<T>> CreateCustomResponse<T>(this ApiController apiController, ResultMessage<T> content)
        {
            return new CustomResult<ResultMessage<T>>(content, apiController, content.IsError ? HttpStatusCode.BadRequest : HttpStatusCode.OK);
        }

        /// <summary>
        /// The create custom response.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="apiController">The api controller.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// The <see cref="CustomResult" />.
        /// </returns>
        public static CustomResult<T> CreateCustomResponse<T>(this ApiController apiController, T content)
        {
            return new CustomResult<T>(content, apiController, HttpStatusCode.OK);
        }
    }
}
