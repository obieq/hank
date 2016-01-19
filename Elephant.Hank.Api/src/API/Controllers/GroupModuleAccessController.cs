// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupModuleAccessController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-14</date>
// <summary>
//     The GroupModuleAccessController class
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
    /// The GroupModuleAccessController class
    /// </summary>
    [RoutePrefix("api/group-module-access")]
    [Authorize]
    public class GroupModuleAccessController : BaseApiController
    {
        /// <summary>
        /// The group module access service
        /// </summary>
        private readonly IGroupModuleAccessService groupModuleAccessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupModuleAccessController"/> class.
        /// </summary>
        /// <param name="loggerService">the logger service</param>
        /// <param name="groupModuleAccessService">the group module access service</param>
        public GroupModuleAccessController(ILoggerService loggerService, IGroupModuleAccessService groupModuleAccessService)
            : base(loggerService)
        {
            this.groupModuleAccessService = groupModuleAccessService;
        }

        /// <summary>
        /// Update the TblGroupModuleAccessDto table entries in one go
        /// </summary>
        /// <param name="groupModuleAccessList">TblGroupModuleAccessDto list object</param>
        /// <returns>list of updated entries</returns>
        [HttpPost]
        [Route("update-access-bulk")]
        public IHttpActionResult UpdateModuleAccessBulk(IEnumerable<TblGroupModuleAccessDto> groupModuleAccessList)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            try
            {
                this.groupModuleAccessService.UpdateModuleAccessBulk(groupModuleAccessList, this.UserId);
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