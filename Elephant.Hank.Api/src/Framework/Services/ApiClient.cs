// ---------------------------------------------------------------------------------------------------
// <copyright file="ApiClient.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The ApiClient class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.Services;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The ApiClient class
    /// </summary>
    public class ApiClient : IApiClient
    {
        /// <summary>
        /// The default content type.
        /// </summary>
        public const string DefaultContentType = "application/json";

        /// <summary>
        /// The logger logger service
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        public ApiClient(ILoggerService loggerService)
        {
            this.loggerService = loggerService;

            this.CustomHeaders = new List<NameValuePair>();
        }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the custom headers.
        /// </summary>
        public List<NameValuePair> CustomHeaders { get; set; }

        /// <summary>
        /// Gets the specified location.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="location">The location.</param>
        /// <returns>
        /// Object of type T
        /// </returns>
        public async Task<ResultMessage<T>> Get<T>(string location)
        {
            var result = new ResultMessage<T>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultContentType));
                this.LoadAuthHeaders(client.DefaultRequestHeaders);

                try
                {
                    var response = await client.GetAsync(location);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string errorMessage = null;
                        try
                        {
                            result.Item = await response.Content.ReadAsAsync<T>();
                        }
                        catch (Exception ex)
                        {
                            errorMessage = "Error: " + ex.Message;
                        }

                        if (errorMessage.IsNotBlank())
                        {
                            var testData = await response.Content.ReadAsStringAsync();
                            throw new Exception(errorMessage + " ErrorData: " + testData);
                        }
                    }
                    else
                    {
                        var testData = await response.Content.ReadAsStringAsync();
                        throw new Exception(response.StatusCode + " ErrorData: " + testData);
                    }
                }
                catch (Exception ex)
                {
                    this.loggerService.LogException(string.Format("{0}.{1}: Requested Uri: {2} - Failed with Exception. {3} ", this.GetType(), "Get", client.BaseAddress + location, ex.Message));
                    result.Messages.Add(new Message { Name = "Error", Value = "Please try again!" });
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the specified location.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <typeparam name="Tin">The type of the in object.</typeparam>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="data">The data.</param>
        /// <param name="contentType">content type</param>
        /// <param name="changeToPutReq">if set to <c>true</c> [change to put request].</param>
        /// <returns>
        /// Object of type T
        /// </returns>
        /// <exception cref="Exception">returns argument exception</exception>
        public async Task<ResultMessage<T>> Post<T, Tin>(string relativePath, Tin data, string contentType = null, bool changeToPutReq = false)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = (data is string) ? data.ToString() : JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                HttpContent content = new StringContent(json);

                if (string.IsNullOrEmpty(contentType))
                {
                    contentType = DefaultContentType;
                }

                client.BaseAddress = new Uri(this.BaseUrl);
                content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                this.LoadAuthHeaders(client.DefaultRequestHeaders);

                try
                {
                    var response = changeToPutReq ? await client.PutAsync(relativePath, content) : await client.PostAsync(relativePath, content);

                    var result = new ResultMessage<T>();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        result.Messages.Add(new Message("Unauthorized", "Unauthorized access!"));
                    }

                    string errorMessage = null;

                    try
                    {
                        if ((response.Headers.TransferEncoding + string.Empty).EqualsIgnoreCase("chunked"))
                        {
                            var pcResultAsStr = await response.Content.ReadAsStringAsync();

                            result.Item = JsonConvert.DeserializeObject<T>(pcResultAsStr);
                        }
                        else
                        {
                            result.Item = await response.Content.ReadAsAsync<T>();
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = "Error: " + ex.Message;
                    }

                    if (errorMessage.IsNotBlank())
                    {
                        var testData = await response.Content.ReadAsStringAsync();
                        throw new Exception(response.StatusCode + " " + errorMessage + " ErrorData: " + testData);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    this.loggerService.LogException(string.Format("{0}.{1}: Requested Uri: {2} - Failed with Exception. {3} ", this.GetType(), "Get", client.BaseAddress + relativePath, ex.Message));
                    return new ResultMessage<T>(new Message { Name = "Error", Value = "Please try again!" });
                }
            }
        }

        /// <summary>
        /// Loads the authentication headers.
        /// </summary>
        /// <param name="defaultHeaders">The default headers.</param>
        private void LoadAuthHeaders(HttpRequestHeaders defaultHeaders)
        {
            if (this.CustomHeaders != null)
            {
                foreach (var header in this.CustomHeaders)
                {
                    if (header.Name.IsNotBlank())
                    {
                        defaultHeaders.Add(header.Name, header.Value);
                    }
                }
            }
        }
    }
}