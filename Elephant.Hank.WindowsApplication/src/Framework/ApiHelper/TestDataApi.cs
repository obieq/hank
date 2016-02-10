// ---------------------------------------------------------------------------------------------------
// <copyright file="TestDataApi.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The TestDataApi class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.ApiHelper
{
    using System;
    using System.Configuration;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Messages;

    using RestSharp;

    using System.Text;

    /// <summary>
    /// The ApiHelper class
    /// </summary>
    public static class TestDataApi
    {
        /// <summary>
        /// Gets the base URL.
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                return Properties.Settings.Default.BaseApiUrl;
            }
        }

        /// <summary>
        /// Gets the specified base URL.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="endPoint">The end point.</param>
        /// <returns>
        /// ResultMessage object
        /// </returns>
        public static ResultMessage<T> Get<T>(string endPoint) where T : class
        {
            var getRequest = GetRestRequest();

            return Execute<T>(BaseUrl + endPoint, getRequest);
        }

        /// <summary>
        /// Posts the specified end point.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endPoint">The end point.</param>
        /// <param name="objToPost">The object to post.</param>
        /// <returns>ResultMessage object</returns>
        public static ResultMessage<T> Post<T>(string endPoint, T objToPost) where T : class
        {
            var getRequest = GetRestRequest();
            getRequest.Method = Method.POST;
            getRequest.AddJsonBody(objToPost);
            return Execute<T>(BaseUrl + endPoint, getRequest);
        }

        /// <summary>
        /// Posts the specified end point.
        /// </summary>
        /// <typeparam name="Tin">The type of the in.</typeparam>
        /// <typeparam name="Tout">The type of the out.</typeparam>
        /// <param name="endPoint">The end point.</param>
        /// <param name="objToPost">The object to post.</param>
        /// <returns>ResultMessage object</returns>
        public static ResultMessage<Tout> Post<Tin, Tout>(string endPoint, Tin objToPost)
            where Tin : class
            where Tout : class
        {
            var getRequest = GetRestRequest();
            getRequest.Method = Method.POST;
            getRequest.AddJsonBody(objToPost);
            return Execute<Tout>(BaseUrl + endPoint, getRequest);
        }

        /// <summary>
        /// Executes the specified base URL.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="request">The request.</param>
        /// <returns>Object Type</returns>
        public static ResultMessage<T> Execute<T>(string baseUrl, RestRequest request) where T : class
        {
            var result = new ResultMessage<T>();

            var client = new RestClient
                         {
                             BaseUrl = new Uri(baseUrl)
                         };

            var response = client.Execute<ResultMessage<T>>(request);

            if (response.ErrorException != null)
            {
                LoggerService.LogException("Error log: " + baseUrl);
                LoggerService.LogException(response.ErrorException); 
                
                result.Messages.Add(new Message("Error", response.ErrorException.Message));
            }
            else
            {
                result = response.Data;
            }

            return result;
        }

        /// <summary>
        /// Gets the rest request.
        /// </summary>
        /// <returns>RestRequest object</returns>
        public static RestRequest GetRestRequest()
        {
            var result = new RestRequest();
            result.AddHeader("content-type", "application/json");
            var bytes = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["UserName"] + ":" + ConfigurationManager.AppSettings["Password"]);
            var base64 = Convert.ToBase64String(bytes);

            result.AddHeader("Authorization", base64);
            return result;
        }
    }
}
