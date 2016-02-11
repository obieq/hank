// ---------------------------------------------------------------------------------------------------
// <copyright file="ExecuteSqlController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-12-18</date>
// <summary>
//     The ExecuteSqlController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The ExecuteSqlController class.
    /// </summary>
    [RoutePrefix("api/execute-sql")]
    [CustomAuthorize(Roles = RoleName.WindowServiceRole)]
    public class ExecuteSqlController : BaseApiController
    {
        /// <summary>
        /// The execute sql for protractor service
        /// </summary>
        private readonly IExecuteSqlForProtractorService executeSqlForProtractorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteSqlController"/> class.
        /// </summary>
        /// <param name="loggerService">the logger service</param>
        /// <param name="executeSqlForProtractorService">execute SQL for protractor service</param>
        public ExecuteSqlController(ILoggerService loggerService, IExecuteSqlForProtractorService executeSqlForProtractorService)
            : base(loggerService)
        {
            this.executeSqlForProtractorService = executeSqlForProtractorService;
        }

        /// <summary>
        /// Execute the querry defined in protractor test script
        /// </summary>
        /// <param name="executableTestData">executable Test data step</param>
        /// <param name="testQueueId">the test Queue identifier</param>
        /// <returns>response object in string format</returns>
        [HttpPost]
        [Route("{testQueueId}")]
        public IHttpActionResult ExecuteSqlForProtractor(ExecutableTestData executableTestData, long testQueueId)
        {
            var result = new ResultMessage<object>();
            try
            {
                result = this.executeSqlForProtractorService.ExecuteQuery(executableTestData, testQueueId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }
    }
}