// ---------------------------------------------------------------------------------------------------
// <copyright file="TestQueueController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-06</date>
// <summary>
//     The TestQueueController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Framework.Extensions;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.ViewModal;

    /// <summary>
    /// The TestQueueController class
    /// </summary>
    [RoutePrefix("api/testqueue")]
    public class TestQueueController : BaseApiController
    {
        /// <summary>
        /// The testQueueService service
        /// </summary>
        private readonly ITestQueueService testQueueService;

        /// <summary>
        /// The testQueueExecutable Service
        /// </summary>
        private readonly ITestQueueExecutableService testQueueExecutableService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestQueueController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="testQueueService">The test service.</param>
        /// /// <param name="testQueueExecutableService">The test Queue Executable service.</param>
        public TestQueueController(ILoggerService loggerService, ITestQueueService testQueueService, ITestQueueExecutableService testQueueExecutableService)
            : base(loggerService)
        {
            this.testQueueService = testQueueService;
            this.testQueueExecutableService = testQueueExecutableService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblTestQueueDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblTestQueueDto>>();
            try
            {
                result = this.testQueueService.GetAllUnProcessed();
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
        /// <returns>TblTestQueueDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblTestQueueDto>();
            try
            {
                result = this.testQueueService.GetById(id);
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
            var result = new ResultMessage<TblTestQueueDto>();
            try
            {
                result = this.testQueueService.DeleteById(id, this.UserId);
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
        /// <param name="testQueueDto">The testQueue Dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody] TblTestQueueDto testQueueDto)
        {
            var result = new ResultMessage<TblTestQueueDto>();
            if (ModelState.IsValid)
            {
                return this.AddUpdate(testQueueDto);
            }

            result.Messages.AddRange(this.ModelStateToMessage());

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="testQueueDto">The testQueue Dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblTestQueueDto testQueueDto, long id)
        {
            testQueueDto.Id = id;
            return this.AddUpdate(testQueueDto);
        }

        /// <summary>
        /// Updates the bulk.
        /// </summary>
        /// <param name="testQueueDto">The test queue dto.</param>
        /// <returns>Newly updated object</returns>
        [Route("bulk-update")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateBulk([FromBody]TblTestQueueDto[] testQueueDto)
        {
            var result = new ResultMessage<IEnumerable<TblTestQueueDto>>();
            try
            {
                result = this.testQueueService.SaveOrUpdate(testQueueDto, this.UserId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// get executable test data
        /// </summary>
        /// <param name="id">the test identifier</param>
        /// <returns>GetTestQueueExecutableData object</returns>
        [Route("{id}/exe-test-data")]
        public IHttpActionResult GetExecutableTestData(long id)
        {
            var result = new ResultMessage<TestQueue_FullTestData>();
            try
            {
                result = this.testQueueExecutableService.GetTestQueueExecutableData(id);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// change the status of test queue
        /// </summary>
        /// <param name="testQueueId">test queue identifier</param>
        /// <param name="stateId">status of test queue</param>
        /// <returns>TblTestQueueDto object</returns>
        [Route("{testQueueId}/test-state/{stateId}")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult ChangeTestQueueState(long testQueueId, int stateId)
        {
            var result = new ResultMessage<TblTestQueueDto>();
            try
            {
                result = this.testQueueService.GetById(testQueueId);

                if (!result.IsError)
                {
                    result.Item.Status = stateId;

                    result = this.testQueueService.SaveOrUpdate(result.Item, this.UserId);
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
        /// Adds the update.
        /// </summary>
        /// <param name="testQueueDto">The testQueue dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblTestQueueDto testQueueDto)
        {
            var result = new ResultMessage<TblTestQueueDto>();
            try
            {
                if (testQueueDto.GroupName.IsBlank())
                {
                    testQueueDto.GroupName = DateTime.Now.ToGroupName();
                }

                result = this.testQueueService.SaveOrUpdate(testQueueDto, this.UserId);
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