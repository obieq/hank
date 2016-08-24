using Elephant.Hank.Common.LogService;
using Elephant.Hank.Common.TestDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Elephant.Hank.Framework.Extensions;

namespace Elephant.Hank.Api.Controllers
{
    [RoutePrefix("api/Restaurant")]
    public class RestaurantController : BaseApiController
    {
        /// <summary>
        /// The authentication repository
        /// </summary>
        private readonly IRestaurantService restaurantService;

        private readonly ILoggerService ILoggerService;

        public RestaurantController(IRestaurantService restaurantService, ILoggerService loggerService)
            : base(loggerService)
        {
            this.restaurantService = restaurantService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            var result = this.restaurantService.GetAll();
            return this.CreateCustomResponse(result);
        }

    }
}