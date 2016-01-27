// ---------------------------------------------------------------------------------------------------
// <copyright file="TestDataController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TestDataController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Elephant.Hank.Api.App_Start;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TestDataController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/test-cat/{testCategoryId}/test/{testId}/test-data")]
    public class TestDataController : BaseApiController
    {
        /// <summary>
        /// The test data service
        /// </summary>
        private readonly ITestDataService testDataService;

        /// <summary>
        /// The TestDataSharedTestDataMap Service
        /// </summary>
        private readonly ITestDataSharedTestDataMapService testDataSharedTestDataMapService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDataController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="testDataService">The test data service.</param>
        /// <param name="testDataSharedTestDataMapService">The shared test data service.</param>
        public TestDataController(ILoggerService loggerService, ITestDataService testDataService, ITestDataSharedTestDataMapService testDataSharedTestDataMapService)
            : base(loggerService)
        {
            this.testDataService = testDataService;
            this.testDataSharedTestDataMapService = testDataSharedTestDataMapService;
        }

        /// <summary>
        /// Get all test data 
        /// </summary>
        /// <param name="testId">the test identifier</param>
        /// <returns>TblTestDataDto object</returns>
        [Route("")]
        public IHttpActionResult GetAllTestData(long testId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();
            try
            {
                result = this.testDataService.GetTestDataByTestCase(testId);
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
        /// <param name="testDataId">The identifier.</param>
        /// <returns>TblTestDataDto objects</returns>
        [Route("{testDataId}")]
        public IHttpActionResult GetById(long testDataId)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                result = this.testDataService.GetById(testDataId);
                if (result.Item != null)
                {
                    if (result.Item.LinkTestType == (int)LinkTestType.SharedTest)
                    {
                        ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>> sharedTestData = this.testDataSharedTestDataMapService.GetByTestDataId(result.Item.Id);
                        result.Item.SharedTestSteps = sharedTestData.Item.ToList();
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

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="testId">The test identifier.</param>
        /// <param name="testDataId">The testdata identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{testDataId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long testId, long testDataId)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                this.testDataService.ResetExecutionSequence(this.UserId, testId, testDataId, 0);
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
        /// Gets the by identifier.
        /// </summary>
        /// <param name="testDataDto">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody]TblTestDataDto testDataDto)
        {
            return this.AddUpdateTestData(testDataDto);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="testDataDto">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("add-in-bulk")]
        public IHttpActionResult Add([FromBody]IEnumerable<TblTestDataDto> testDataDto)
        {
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();
            try
            {
                result = this.testDataService.AddTestDataList(this.UserId, testDataDto);
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
        /// <param name="copyTestDataModel">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("copy-test-data")]
        public IHttpActionResult Add([FromBody]CopyTestDataModel copyTestDataModel)
        {
            var result = new ResultMessage<bool>();
            try
            {
                bool executionStatus = this.testDataService.CopyTestData(this.UserId, copyTestDataModel);
                if (executionStatus)
                {
                    result.Item = true;
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="testDataDto">The test data dto.</param>
        /// <param name="testId">The test identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{testDataId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblTestDataDto testDataDto, long testId, long testDataId)
        {
            testDataDto.Id = testDataId;
            testDataDto.TestId = testId;
            return this.AddUpdateTestData(testDataDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="testDataDto">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblTestDataDto testDataDto)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                if (testDataDto.LinkTestType == (int)LinkTestType.SharedTest)
                {
                    result = this.testDataService.SaveOrUpdateWithSharedTest(this.UserId, testDataDto);
                }
                else
                {
                    result = this.testDataService.SaveOrUpdate(testDataDto, this.UserId);
                }
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
    }
}