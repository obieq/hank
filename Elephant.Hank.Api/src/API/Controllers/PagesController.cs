// ---------------------------------------------------------------------------------------------------
// <copyright file="PagesController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-04</date>
// <summary>
//     The PagesController class
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
    /// The Pages class
    /// </summary>
    [RoutePrefix("api/pages")]
    [Authorize]
    public class PagesController : BaseApiController
    {
        /// <summary>
        /// The pages service
        /// </summary>
        private readonly IPagesService pagesService;

        /// <summary>
        /// The locator identifier service
        /// </summary>
        private readonly ILocatorIdentifierService locatorIdentifierService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="pagesService">The pages service.</param>
        /// <param name="locatorIdentifierService">The locator identifier service.</param>
        public PagesController(ILoggerService loggerService, IPagesService pagesService, ILocatorIdentifierService locatorIdentifierService)
            : base(loggerService)
        {
            this.pagesService = pagesService;
            this.locatorIdentifierService = locatorIdentifierService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblPagesDto objects</returns>
        [Route]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblPagesDto>>();
            try
            {
                result = this.pagesService.GetAll();
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
        /// <returns>TblPagesDto objects</returns>
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var result = new ResultMessage<TblPagesDto>();
            try
            {
                result = this.pagesService.GetById(id);
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
            var result = new ResultMessage<TblPagesDto>();
            try
            {
                result = this.pagesService.DeleteById(id, this.UserId);
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
        /// <param name="pagesDto">The display name dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [Route]
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblPagesDto pagesDto)
        {
            var data = this.pagesService.GetByValue(pagesDto.Value, pagesDto.WebsiteId);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Pages already exists with '" + pagesDto.Value + "' value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(pagesDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="pagesDto">The display name dto.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblPagesDto pagesDto, long id)
        {
            var data = this.pagesService.GetByValue(pagesDto.Value, pagesDto.WebsiteId);

            if (!data.IsError && data.Item != null && id != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Pages already exists with '" + pagesDto.Value + "' value!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            pagesDto.Id = id;
            return this.AddUpdate(pagesDto);
        }

        #region Website Child - Locator Identifier

        /// <summary>
        /// get by website id
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// List of TblTestDto objects
        /// </returns>
        [Route("{pageId}/locator-identifier")]
        public IHttpActionResult GetLocatorIdentifier(long pageId)
        {
            var result = new ResultMessage<IEnumerable<TblLocatorIdentifierDto>>();
            try
            {
                result = this.locatorIdentifierService.GetByPageId(pageId);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        #endregion
        
        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="pagesDto">The display name dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblPagesDto pagesDto)
        {
            var result = new ResultMessage<TblPagesDto>();
            try
            {
                result = this.pagesService.SaveOrUpdate(pagesDto, this.UserId);
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