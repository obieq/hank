// ---------------------------------------------------------------------------------------------------
// <copyright file="SharedTestDataController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-19</date>
// <summary>
//     The SharedTestDataController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Api.Security;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SharedTestDataController class.
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/shared-test/{sharedTestId}/shared-test-data")]
    public class SharedTestDataController : BaseApiController
    {
        /// <summary>
        /// The test service
        /// </summary>
        private readonly ISharedTestService sharedTestService;

        /// <summary>
        /// The test data service
        /// </summary>
        private readonly ISharedTestDataService sharedTestDataService;

        /// <summary>
        /// the test data shared test data service
        /// </summary>
        private readonly ITestDataSharedTestDataMapService testDataSharedTestDataMapService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedTestDataController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="sharedTestService">The test service.</param>
        /// <param name="sharedTestDataService">The shared test data service.</param>
        /// <param name="testDataSharedTestDataMapService">The test data map service.</param>
        public SharedTestDataController(ILoggerService loggerService, ISharedTestService sharedTestService, ISharedTestDataService sharedTestDataService, ITestDataSharedTestDataMapService testDataSharedTestDataMapService)
            : base(loggerService)
        {
            this.sharedTestService = sharedTestService;
            this.sharedTestDataService = sharedTestDataService;
            this.testDataSharedTestDataMapService = testDataSharedTestDataMapService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="sharedTestId">The test case identifier.</param>
        /// <returns>
        /// List of TblTestDataDto objects
        /// </returns>
        [Route("")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult GetAllTestData(long sharedTestId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDataDto>>();
            try
            {
                result = this.sharedTestDataService.GetTestDataByTestCase(sharedTestId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the test data by identifier.
        /// </summary>
        /// <param name="sharedTestDataId">The test data identifier.</param>
        /// <returns>TblTestDataDto object</returns>
        [Route("{sharedTestDataId}")]
        [CustomAuthorize(ActionType = ActionTypes.Read, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult GetTestDataById(long sharedTestDataId)
        {
            var result = new ResultMessage<TblSharedTestDataDto>();
            try
            {
                result = this.sharedTestDataService.GetById(sharedTestDataId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Adds the test data.
        /// </summary>
        /// <param name="sharedtestDataDto">The test data dto.</param>
        /// <param name="sharedTestId">The test case identifier.</param>
        /// <returns>Newly added object</returns>
        [HttpPost]
        [Route("")]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult AddTestData([FromBody]TblSharedTestDataDto sharedtestDataDto, long sharedTestId)
        {
            return this.AddUpdateSharedTestData(sharedtestDataDto);
        }

        /// <summary>
        /// Updates the test data.
        /// </summary>
        /// <param name="sharedtestDataDto">The test data dto.</param>
        /// <param name="sharedTestId">The test case identifier.</param>
        /// <param name="sharedTestDataId">The test data identifier.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPut]
        [Route("{sharedTestDataId}")]
        [CustomAuthorize(ActionType = ActionTypes.Write, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult UpdateTestData([FromBody]TblSharedTestDataDto sharedtestDataDto, long sharedTestId, long sharedTestDataId)
        {
            sharedtestDataDto.Id = sharedTestDataId;
            sharedtestDataDto.SharedTestId = sharedTestId;

            return this.AddUpdateSharedTestData(sharedtestDataDto);
        }

        /// <summary>
        /// Deletes the test data.
        /// </summary>
        /// <param name="sharedTestId">The test case identifier.</param>
        /// <param name="sharedTestDataId">The test data identifier.</param>
        /// <returns>Deleted TblTestDataDto object</returns>
        [HttpDelete]
        [Route("{sharedTestDataId}")]
        [CustomAuthorize(ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.SharedTestCases)]
        public IHttpActionResult DeleteTestData(long sharedTestId, long sharedTestDataId)
        {
            var result = new ResultMessage<TblSharedTestDataDto>();
            try
            {
                this.sharedTestDataService.ResetExecutionSequence(this.UserId, sharedTestId, sharedTestDataId, 0);
                result = this.sharedTestDataService.DeleteById(sharedTestDataId, this.UserId);
                this.testDataSharedTestDataMapService.DeleteBySharedTestDataId(this.UserId, sharedTestDataId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.AddUpdateSharedTestData(result.Item);
        }

        /// <summary>
        /// Adds the update test data.
        /// </summary>
        /// <param name="sharedtestDataDto">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdateSharedTestData(TblSharedTestDataDto sharedtestDataDto)
        {
            var result = new ResultMessage<TblSharedTestDataDto>();
            try
            {
                sharedtestDataDto.ExecutionSequence = sharedtestDataDto.ExecutionSequence <= 0 ? 1 : sharedtestDataDto.ExecutionSequence;
                this.sharedTestDataService.ResetExecutionSequence(this.UserId, sharedtestDataDto.SharedTestId, sharedtestDataDto.Id, sharedtestDataDto.ExecutionSequence);
                result = this.sharedTestDataService.SaveUpdateCustom(sharedtestDataDto, this.UserId);
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