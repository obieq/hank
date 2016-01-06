// ---------------------------------------------------------------------------------------------------
// <copyright file="SharedTestController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharna</author>
// <date>2015-06-05</date>
// <summary>
//     The SharedTestController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SharedTestController class
    /// </summary>
    [RoutePrefix("api/sharedtest")]
    [Authorize]
    public class SharedTestController : BaseApiController
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
        /// Initializes a new instance of the <see cref="SharedTestController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="sharedTestService">The test service.</param>
        /// <param name="sharedTestDataService">The shared test data service.</param>
        /// <param name="testDataSharedTestDataMapService">The test data map service.</param>
        public SharedTestController(ILoggerService loggerService, ISharedTestService sharedTestService, ISharedTestDataService sharedTestDataService, ITestDataSharedTestDataMapService testDataSharedTestDataMapService)
            : base(loggerService)
        {
            this.sharedTestService = sharedTestService;
            this.sharedTestDataService = sharedTestDataService;
            this.testDataSharedTestDataMapService = testDataSharedTestDataMapService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblTestDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDto>>();
            try
            {
                result = this.sharedTestService.GetAll();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TblTestDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblSharedTestDto>();
            try
            {
                result = this.sharedTestService.GetById(id);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long id)
        {
            var result = new ResultMessage<TblSharedTestDto>();
            try
            {
                result = this.sharedTestService.DeleteById(id, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="sharedTestDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblSharedTestDto sharedTestDto)
        {
            var data = this.sharedTestService.GetByName(sharedTestDto.TestName, sharedTestDto.WebsiteId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + sharedTestDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(sharedTestDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="sharedTestDto">The test dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblSharedTestDto sharedTestDto, long id)
        {
            var data = this.sharedTestService.GetByName(sharedTestDto.TestName, sharedTestDto.WebsiteId);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + sharedTestDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            sharedTestDto.Id = id;
            return this.AddUpdate(sharedTestDto);
        }

        #region Child Objects Test Data

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="sharedTestCaseId">The test case identifier.</param>
        /// <returns>
        /// List of TblTestDataDto objects
        /// </returns>
        [Route("{sharedTestCaseId}/shared-test-data")]
        public IHttpActionResult GetAllTestData(long sharedTestCaseId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDataDto>>();
            try
            {
                result = this.sharedTestDataService.GetTestDataByTestCase(sharedTestCaseId);
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
        /// <param name="sharedTestCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <returns>TblTestDataDto object</returns>
        [Route("{sharedTestCaseId}/shared-test-data/{testDataId}")]
        public IHttpActionResult GetTestDataById(long sharedTestCaseId, long testDataId)
        {
            var result = new ResultMessage<TblSharedTestDataDto>();
            try
            {
                result = this.sharedTestDataService.GetById(testDataId);
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
        /// <param name="sharedTestCaseId">The test case identifier.</param>
        /// <returns>Newly added object</returns>
        [HttpPost]
        [Route("{sharedTestCaseId}/shared-test-data")]
        public IHttpActionResult AddTestData([FromBody]TblSharedTestDataDto sharedtestDataDto, long sharedTestCaseId)
        {
            return this.AddUpdateSharedTestData(sharedtestDataDto);
        }

        /// <summary>
        /// Updates the test data.
        /// </summary>
        /// <param name="sharedtestDataDto">The test data dto.</param>
        /// <param name="sharedTestCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPut]
        [Route("{sharedTestCaseId}/shared-test-data/{testDataId}")]
        public IHttpActionResult UpdateTestData([FromBody]TblSharedTestDataDto sharedtestDataDto, long sharedTestCaseId, long testDataId)
        {
            sharedtestDataDto.Id = testDataId;
            sharedtestDataDto.SharedTestId = sharedTestCaseId;

            return this.AddUpdateSharedTestData(sharedtestDataDto);
        }

        /// <summary>
        /// Deletes the test data.
        /// </summary>
        /// <param name="sharedTestCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <returns>Deleted TblTestDataDto object</returns>
        [HttpDelete]
        [Route("{sharedTestCaseId}/shared-test-data/{testDataId}")]
        public IHttpActionResult DeleteTestData(long sharedTestCaseId, long testDataId)
        {
            var result = new ResultMessage<TblSharedTestDataDto>();
            try
            {
                this.sharedTestDataService.ResetExecutionSequence(this.UserId, sharedTestCaseId, testDataId, 0);
                result = this.sharedTestDataService.DeleteById(testDataId, this.UserId);
                this.testDataSharedTestDataMapService.DeleteBySharedTestDataId(this.UserId, testDataId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.AddUpdateSharedTestData(result.Item);
        }

        /// <summary>
        /// Get the list of variable type test steps
        /// </summary>
        /// <param name="sharedTestCaseId">the shared test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{sharedTestCaseId}/variable-test-steps")]
        public IHttpActionResult GetVariableTypeTestDataByTestCase(long sharedTestCaseId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDataDto>>();
            try
            {
                result = this.sharedTestDataService.GetVariableTypeSharedTestDataBySharedTestCase(sharedTestCaseId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
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
                if (sharedtestDataDto.Id == 0)
                {
                    sharedtestDataDto.ExecutionSequence = sharedtestDataDto.ExecutionSequence <= 0 ? 1 : sharedtestDataDto.ExecutionSequence;

                    this.sharedTestDataService.ResetExecutionSequence(this.UserId, sharedtestDataDto.SharedTestId, sharedtestDataDto.Id, sharedtestDataDto.ExecutionSequence);

                    result = this.sharedTestDataService.SaveOrUpdate(sharedtestDataDto, this.UserId);
                }
                else
                {
                    sharedtestDataDto.ExecutionSequence = sharedtestDataDto.ExecutionSequence <= 0 ? 1 : sharedtestDataDto.ExecutionSequence;

                    this.sharedTestDataService.ResetExecutionSequence(this.UserId, sharedtestDataDto.SharedTestId, sharedtestDataDto.Id, sharedtestDataDto.ExecutionSequence);

                    result = this.sharedTestDataService.SaveOrUpdate(sharedtestDataDto, this.UserId);
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region All private

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="sharedTestDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblSharedTestDto sharedTestDto)
        {
            var result = new ResultMessage<TblSharedTestDto>();
            try
            {
                result = this.sharedTestService.SaveOrUpdate(sharedTestDto, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion
    }
}