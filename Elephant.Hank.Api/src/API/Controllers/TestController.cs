// ---------------------------------------------------------------------------------------------------
// <copyright file="TestController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TestController class
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
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TestController class
    /// </summary>
    [RoutePrefix("api/test")]
    [Authorize]
    public class TestController : BaseApiController
    {
        /// <summary>
        /// The test service
        /// </summary>
        private readonly ITestService testService;

        /// <summary>
        /// The test data service
        /// </summary>
        private readonly ITestDataService testDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="testService">The test service.</param>
        /// <param name="testDataService">The test data service.</param>
        public TestController(ILoggerService loggerService, ITestService testService, ITestDataService testDataService)
            : base(loggerService)
        {
            this.testService = testService;
            this.testDataService = testDataService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblTestDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblTestDto>>();
            try
            {
                result = this.testService.GetAll();
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
            var result = new ResultMessage<TblTestDto>();
            try
            {
                result = this.testService.GetById(id);
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
            var result = new ResultMessage<TblTestDto>();
            try
            {
                result = this.testService.DeleteById(id, this.UserId);
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
        /// <param name="testDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblTestDto testDto)
        {
            var data = this.testService.GetByName(testDto.TestName, testDto.WebsiteId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + testDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(testDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="testDto">The test dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblTestDto testDto, long id)
        {
            var data = this.testService.GetByName(testDto.TestName, testDto.WebsiteId);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Website already exists with '" + testDto.TestName + "' name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            testDto.Id = id;
            return this.AddUpdate(testDto);
        }

        #region Child Objects Test Data

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>
        /// List of TblTestDataDto objects
        /// </returns>
        [Route("{testCaseId}/test-data")]
        public IHttpActionResult GetAllTestData(long testCaseId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();
            try
            {
                result = this.testDataService.GetTestDataByTestCase(testCaseId);
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
        /// <param name="testCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <returns>TblTestDataDto object</returns>
        [Route("{testCaseId}/test-data/{testDataId}")]
        public IHttpActionResult GetTestDataById(long testCaseId, long testDataId)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                result = this.testDataService.GetById(testDataId);
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
        /// <param name="testDataDto">The test data dto.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>Newly added object</returns>
        [HttpPost]
        [Route("{testCaseId}/test-data")]
        public IHttpActionResult AddTestData([FromBody]TblTestDataDto testDataDto, long testCaseId)
        {
            return this.AddUpdateTestData(testDataDto);
        }

        /// <summary>
        /// Updates the test data.
        /// </summary>
        /// <param name="testDataDto">The test data dto.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPut]
        [Route("{testCaseId}/test-data/{testDataId}")]
        public IHttpActionResult UpdateTestData([FromBody]TblTestDataDto testDataDto, long testCaseId, long testDataId)
        {
            testDataDto.Id = testDataId;
            testDataDto.TestId = testCaseId;

            return this.AddUpdateTestData(testDataDto);
        }

        /// <summary>
        /// Deletes the test data.
        /// </summary>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <returns>Deleted TblTestDataDto object</returns>
        [HttpDelete]
        [Route("{testCaseId}/test-data/{testDataId}")]
        public IHttpActionResult DeleteTestData(long testCaseId, long testDataId)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                this.testDataService.ResetExecutionSequence(this.UserId, testCaseId, testDataId, 0);
                result = this.testDataService.DeleteById(testDataId, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.AddUpdateTestData(result.Item);
        }

        /// <summary>
        /// Get the list of variable type test steps
        /// </summary>
        /// <param name="testCaseId">the test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{testCaseId}/variable-test-steps")]
        public IHttpActionResult GetVariableTypeTestDataByTestCase(long testCaseId)
        {
            var result = new ResultMessage<IEnumerable<ProtractorVariableModel>>();
            try
            {
                result = this.testDataService.GetVariableTypeTestDataByTestCase(testCaseId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Get the list of variable type test steps
        /// </summary>
        /// <param name="testCaseId">the test case identifier</param>
        /// <returns>TblTestDataDto object list</returns>
        [Route("{testCaseId}/variable-for-autocomplete")]
        public IHttpActionResult GetAllVariableByTestIdForAutoComplete(long testCaseId)
        {
            var result = new ResultMessage<IEnumerable<string>>();
            try
            {
                result = this.testDataService.GetAllVariableByTestIdForAutoComplete(testCaseId);
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
        /// <param name="testDataDto">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdateTestData(TblTestDataDto testDataDto)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                if (testDataDto.Id == 0)
                {
                    testDataDto.ExecutionSequence = testDataDto.ExecutionSequence <= 0 ? 1 : testDataDto.ExecutionSequence;
                    this.testDataService.ResetExecutionSequence(this.UserId, testDataDto.TestId, testDataDto.Id, testDataDto.ExecutionSequence);
                    if (testDataDto.LinkTestType == (int)LinkTestType.SharedTest)
                    {
                        testDataDto.LocatorIdentifierId = null;
                        testDataDto.ActionId = null;
                        result = this.testDataService.SaveOrUpdateWithSharedTest(this.UserId, testDataDto);
                    }
                    else
                    {
                        result = this.testDataService.SaveOrUpdate(testDataDto, this.UserId);
                    }
                }
                else
                {
                    testDataDto.ExecutionSequence = testDataDto.ExecutionSequence <= 0 ? 1 : testDataDto.ExecutionSequence;
                    this.testDataService.ResetExecutionSequence(this.UserId, testDataDto.TestId, testDataDto.Id, testDataDto.ExecutionSequence);
                    if (testDataDto.LinkTestType == (int)LinkTestType.SharedTest)
                    {
                        testDataDto.LocatorIdentifierId = null;
                        testDataDto.ActionId = null;
                        result = this.testDataService.SaveOrUpdateWithSharedTest(this.UserId, testDataDto);
                    }
                    else
                    {
                        result = this.testDataService.SaveOrUpdate(testDataDto, this.UserId);
                    }
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
        /// <param name="testDto">The test dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblTestDto testDto)
        {
            var result = new ResultMessage<TblTestDto>();
            try
            {
                result = this.testService.SaveOrUpdate(testDto, this.UserId);
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