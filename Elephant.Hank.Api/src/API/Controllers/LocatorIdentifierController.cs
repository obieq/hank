// ---------------------------------------------------------------------------------------------------
// <copyright file="LocatorIdentifierController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The LocatorIdentifierController class
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
    /// The LocatorIdentifierController class
    /// </summary>
    [RoutePrefix("api/website/{websiteId}/pages/{pageId}/locator-identifier")]
    public class LocatorIdentifierController : BaseApiController
    {
        /// <summary>
        /// The locator identifier service
        /// </summary>
        private readonly ILocatorIdentifierService locatorIdentifierService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatorIdentifierController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="locatorIdentifierService">The locator identifier service.</param>
        public LocatorIdentifierController(ILoggerService loggerService, ILocatorIdentifierService locatorIdentifierService)
            : base(loggerService)
        {
            this.locatorIdentifierService = locatorIdentifierService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="pageId">The page identoifier.</param>
        /// <returns>List of TblLocatorIdentifierDto objects</returns>
        [Route("")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.LocatorIdentifier)]
        public IHttpActionResult GetAll(long pageId)
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

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="locatorIdentifierId">The identifier.</param>
        /// <returns>TblLocatorIdentifierDto objects</returns>
        [Route("{locatorIdentifierId}")]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Read, ModuleType = FrameworkModules.LocatorIdentifier)]
        public IHttpActionResult GetById(long locatorIdentifierId)
        {
            var result = new ResultMessage<TblLocatorIdentifierDto>();
            try
            {
                result = this.locatorIdentifierService.GetById(locatorIdentifierId);
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
        /// <param name="locatorIdentifierId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{locatorIdentifierId}")]
        [HttpDelete]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Delete, ModuleType = FrameworkModules.LocatorIdentifier)]
        public IHttpActionResult DeleteById(long locatorIdentifierId)
        {
            var result = new ResultMessage<TblLocatorIdentifierDto>();
            try
            {
                result = this.locatorIdentifierService.DeleteById(locatorIdentifierId, this.UserId);
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
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [Route("")]
        [HttpPost]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.LocatorIdentifier)]
        public IHttpActionResult Add([FromBody]TblLocatorIdentifierDto locatorIdentifierDto)
        {
            var data = this.locatorIdentifierService.IsExisting(locatorIdentifierDto);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Page already has '" + locatorIdentifierDto.DisplayName + "' as display name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(locatorIdentifierDto);
        }

        /// <summary>
        /// Updates the specified action dto.
        /// </summary>
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <param name="locatorIdentifierId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{locatorIdentifierId}")]
        [HttpPut]
        [CustomAuthorize(Roles = RoleName.TestUserRole + "," + RoleName.TestAdminRole, ActionType = ActionTypes.Write, ModuleType = FrameworkModules.LocatorIdentifier)]
        public IHttpActionResult Update([FromBody]TblLocatorIdentifierDto locatorIdentifierDto, long locatorIdentifierId)
        {
            var data = this.locatorIdentifierService.IsExisting(locatorIdentifierDto);

            if (!data.IsError && data.Item != null && locatorIdentifierId != data.Item.Id)
            {
                data.Messages.Add(new Message(null, "Page already has '" + locatorIdentifierDto.DisplayName + "' as display name!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            locatorIdentifierDto.Id = locatorIdentifierId;
            return this.AddUpdate(locatorIdentifierDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        private IHttpActionResult AddUpdate(TblLocatorIdentifierDto locatorIdentifierDto)
        {
            var result = new ResultMessage<TblLocatorIdentifierDto>();
            try
            {
                result = this.locatorIdentifierService.SaveOrUpdate(locatorIdentifierDto, this.UserId);
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