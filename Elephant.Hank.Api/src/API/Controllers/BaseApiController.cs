// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The BaseApiController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The base api controller.
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        private long userId;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController" /> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        public BaseApiController(ILoggerService loggerService)
        {
            this.LoggerService = loggerService;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        public long UserId
        {
            get
            {
                if (this.userId == 0)
                {
                    this.userId = this.User.Identity.GetUserId().ToInt64();
                }

                return this.userId;
            }
        }

        /// <summary>
        /// Gets the logger service.
        /// </summary>
        protected ILoggerService LoggerService { get; private set; }

        /// <summary>
        /// Models the state to message.
        /// </summary>
        /// <returns>Message list</returns>
        protected List<Message> ModelStateToMessage()
        {
            var result = new List<Message>();

            foreach (var key in this.ModelState.Keys)
            {
                result.AddRange(this.ModelState[key].Errors.Select(error => new Message(key, error.ErrorMessage)));
            }

            return result;
        }
    }
}