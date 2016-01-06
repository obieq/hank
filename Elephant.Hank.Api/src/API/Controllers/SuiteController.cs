// ---------------------------------------------------------------------------------------------------
// <copyright file="SuiteController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The SuiteController class
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
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SuiteController class
    /// </summary>
    [RoutePrefix("api/suite")]
    [Authorize]
    public class SuiteController : BaseApiController
    {
        /// <summary>
        /// The suite service
        /// </summary>
        private readonly ISuiteService suiteService;

        /// <summary>
        /// The suite test map service
        /// </summary>
        private readonly ISuiteTestMapService suiteTestMapService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuiteController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="suiteService">The suite service.</param>
        /// <param name="suiteTestMapService">The suite test map service.</param>
        public SuiteController(ILoggerService loggerService, ISuiteService suiteService, ISuiteTestMapService suiteTestMapService)
            : base(loggerService)
        {
            this.suiteService = suiteService;
            this.suiteTestMapService = suiteTestMapService;
        }

        #region Suite CRUD

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblSuiteDto objects</returns>
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblSuiteDto>>();
            try
            {
                result = this.suiteService.GetAll();
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
        /// <returns>TblSuiteDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblSuiteDto>();
            try
            {
                result = this.suiteService.GetById(id);
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
            var result = new ResultMessage<TblSuiteDto>();
            try
            {
                result = this.suiteService.DeleteById(id, this.UserId);
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
        /// <param name="suiteDto">The suite dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblSuiteDto suiteDto)
        {
            suiteDto.SuiteTestMapList = null;
            return this.AddUpdate(suiteDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="suiteDto">The suite dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblSuiteDto suiteDto, long id)
        {
            suiteDto.SuiteTestMapList = null;
            suiteDto.Id = id;
            return this.AddUpdate(suiteDto);
        }

        #endregion

        #region Suite Child - Mappings

        /// <summary>
        /// Gets the multiple test links.
        /// </summary>
        /// <param name="suiteId">The suite identifier.</param>
        /// <returns>TblLnkSuiteTestDto objects</returns>
        [Route("{suiteId}/suite-test-map")]
        [HttpGet]
        public IHttpActionResult GetMultipleTestLinks(long suiteId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSuiteTestDto>>();
            try
            {
                result = this.suiteTestMapService.GetLinkBySuiteId(suiteId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the multiple test links.
        /// </summary>
        /// <param name="suiteIdList">The suite identifier.</param>
        /// <returns>TblLnkSuiteTestDto objects</returns>
        [Route("suite-test-map/{suiteIdList}")]
        [HttpGet]
        public IHttpActionResult GetMultipleTestLinks(string suiteIdList)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSuiteTestDto>>();
            try
            {
                result = this.suiteTestMapService.GetLinksBySuiteIdList(suiteIdList);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Adds the multiple test links.
        /// </summary>
        /// <param name="suiteTestDto">The suite test dto.</param>
        /// <param name="suiteId">The suite identifier.</param>
        /// <returns>
        /// TblLnkSuiteTestDto objects
        /// </returns>
        [Route("{suiteId}/suite-test-map")]
        [HttpPost]
        public IHttpActionResult AddMultipleTestLinks([FromBody]List<TblLnkSuiteTestDto> suiteTestDto, long suiteId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSuiteTestDto>>();
            try
            {
                result = this.suiteTestMapService.SaveOrUpdate(this.UserId, suiteId, suiteTestDto);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion

        #region Suite Child - Suite executable handler

        /// <summary>
        /// Change the state of the test suite case.
        /// </summary>
        /// <param name="suiteId">The suite identifier.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>TblLnkSuiteTestDto object</returns>
        [Route("{suiteId}/test-case/{testCaseId}/test-state/{stateId}")]
        [HttpPost]
        public IHttpActionResult ChangeTestSuiteCaseState(long suiteId, long testCaseId, long stateId)
        {
            var result = new ResultMessage<TblLnkSuiteTestDto>();
            try
            {
                result = this.suiteTestMapService.GetLinkBySuiteIdAndTestId(suiteId, testCaseId);

                if (!result.IsError)
                {
                    result.Item.TestState = stateId;

                    result = this.suiteTestMapService.SaveOrUpdate(result.Item, this.UserId);
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
        /// <param name="suiteDto">The suite dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblSuiteDto suiteDto)
        {
            var result = new ResultMessage<TblSuiteDto>();
            try
            {
                result = this.suiteService.SaveOrUpdate(suiteDto, this.UserId);
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