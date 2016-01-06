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
    [RoutePrefix("api/test-data")]
    [Authorize]
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
        /// Gets all.
        /// </summary>
        /// <returns>List of TblTestDataDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();
            try
            {
                result = this.testDataService.GetAll();
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
        /// <returns>TblTestDataDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                result = this.testDataService.GetById(id);
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
        /// <param name="id">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long id)
        {
            var result = new ResultMessage<TblTestDataDto>();
            try
            {
                result = this.testDataService.DeleteById(id, this.UserId);
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
        /// <param name="testDataDto">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblTestDataDto testDataDto)
        {
            return this.AddUpdate(testDataDto);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="testDataDto">The test data dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        [Route("test-data-list")]
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
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();
            try
            {
                result = this.testDataService.CopyTestData(this.UserId, copyTestDataModel);
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
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblTestDataDto testDataDto, long id)
        {
            testDataDto.Id = id;
            return this.AddUpdate(testDataDto);
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
    }
}