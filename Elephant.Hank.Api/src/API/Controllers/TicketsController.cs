// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketsController.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-22</date>
// <summary>
//     The TicketsController class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using Common.LogService;
    using Common.TestDataServices;
    using Framework.Extensions;
    using Newtonsoft.Json;
    using Resources.Constants;
    using Resources.Dto;
    using Resources.Messages;
    using Resources.Models;
    using Security;    

    /// <summary>
    /// The ActionController class
    /// </summary>
    [RoutePrefix("api/tickets")]
    [CustomAuthorize(Roles = RoleName.TestAdminRole)]
    public class TicketsController : BaseApiController
    {
        /// <summary>
        /// The TicketManager service
        /// </summary>
        private readonly ITicketManagerService ticketManagerService;

        /// <summary>
        /// The TicketHistoryService service
        /// </summary>
        private readonly ITicketHistoryService ticketHistoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketsController"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="ticketManagerService">The TicketManager service.</param>
        /// <param name="ticketHistoryService">The TicketHistory Service</param>
        public TicketsController(ILoggerService loggerService, ITicketManagerService ticketManagerService, ITicketHistoryService ticketHistoryService)
            : base(loggerService)
        {
            this.ticketManagerService = ticketManagerService;
            this.ticketHistoryService = ticketHistoryService;
        }

        /// <summary>
        /// Searches the report criteria data.
        /// </summary>
        /// <returns>SearchCriteriaData object</returns>
        [Route("ticketsData")]
        [HttpGet]
        public IHttpActionResult GetTicketData()
        {
            var result = new ResultMessage<TicketsData>();
            try
            {
                result = this.ticketManagerService.GetTicketData();
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }
        
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of TblTicketMasterDto objects</returns>
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var result = new ResultMessage<IEnumerable<TblTicketMasterDto>>();

            try
            {
                result = this.ticketManagerService.GetAll();
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }
            
            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="ticketId">The identifier.</param>
        /// <returns>TblTicketMasterDto objects</returns>
        [Route("{ticketId}")]
        [AllowAnonymous]
        public IHttpActionResult GetById(long ticketId)
        {
            var result = new ResultMessage<TblTicketMasterDto>();
            try
            {
                result = this.ticketManagerService.GetById(ticketId);
                var historyData = this.ticketHistoryService.GetAll().Item.ToList();
                var historyItems = historyData.FindAll(m => m.TicketId == ticketId);
                var items = historyItems.Select(historyItem => JsonConvert.DeserializeObject<TblTicketMasterDto>(historyItem.Value)).ToList();
                result.Item.TicketHistory = items;
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="ticketId">The identifier.</param>
        /// <returns>Deleted object</returns>
        [Route("{ticketId}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(long ticketId)
        {
            var result = new ResultMessage<TblTicketMasterDto>();
            try
            {
                result = this.ticketManagerService.DeleteById(ticketId, this.UserId);
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="ticketMasterDto">The TicketMaster dto.</param>
        /// <returns>
        /// Newly added object
        /// </returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody]TblTicketMasterDto ticketMasterDto)
        {
            var data = this.ticketManagerService.GetById(ticketMasterDto.Id);

            if (!data.IsError)
            {
                data.Messages.Add(new Message(null, "Ticket already exists with '" + ticketMasterDto.Id + "' Id!"));

                return this.CreateCustomResponse(data, HttpStatusCode.BadRequest);
            }

            return this.AddUpdate(ticketMasterDto);
        }

        /// <summary>
        /// Updates the specified TicketMaster dto.
        /// </summary>
        /// <param name="ticketMasterDto">The TicketMaster dto.</param>
        /// <param name="ticketId">The identifier.</param>
        /// <returns>
        /// Newly updated object
        /// </returns>
        [Route("{ticketId}")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]TblTicketMasterDto ticketMasterDto, long ticketId)
        {
            this.ticketManagerService.GetById(ticketMasterDto.Id);
            ticketMasterDto.Id = ticketId;
            return this.AddUpdate(ticketMasterDto);
        }

        /// <summary>
        /// Adds the update.
        /// </summary>
        /// <param name="ticketMasterDto">The ticketMasterDto dto.</param>
        /// <returns>Newly added object</returns>
        private IHttpActionResult AddUpdate(TblTicketMasterDto ticketMasterDto)
        {
            var result = new ResultMessage<TblTicketMasterDto>();
            
            try
            {
                result = this.ticketManagerService.SaveOrUpdate(ticketMasterDto, this.UserId);
                this.SaveHistory(ticketMasterDto, result.Item);
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);
                result.Messages.Add(new Message(null, ex.Message));
            }

            return this.CreateCustomResponse(result);
        }

        /// <summary>
        /// Save the History of changes happened with ticket
        /// </summary>
        /// <param name="ticketMasterDto">The TicketMasterDto </param>
        /// <param name="item">The object saved in DB firts time</param>
        private void SaveHistory(TblTicketMasterDto ticketMasterDto, TblTicketMasterDto item)
        {
            if (ticketMasterDto.Id == 0)
            {
                ticketMasterDto.Id = item.Id;
                this.ticketHistoryService.Save(item, this.UserId);
            }
            else if (ticketMasterDto.Description != item.Description || ticketMasterDto.Type != item.Type || ticketMasterDto.AssignedTo != item.AssignedTo || ticketMasterDto.CreatedByUserName != item.CreatedByUserName ||
                     ticketMasterDto.Status != item.Status || ticketMasterDto.Priority != item.Priority || ticketMasterDto.IsDeleted != item.IsDeleted)
            {
                this.ticketHistoryService.Save(item, this.UserId);
            }
        }
    }
}