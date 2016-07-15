// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketHistoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-22</date>
// <summary>
//     The TicketHistoryService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.DataService;
    using Common.Mapper;
    using Common.TestDataServices;
    using Data;
    using DataService.DBSchema;
    using Newtonsoft.Json;
    using Resources.Dto;
    using Resources.Messages;

    /// <summary>
    /// The TicketHistoryService class
    /// </summary>
    public class TicketHistoryService : GlobalService<TblTicketHistoryDto, TblTicketHistory>, ITicketHistoryService
    {
        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblTicketHistory> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketHistoryService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TicketHistoryService(IMapperFactory mapperFactory, IRepository<TblTicketHistory> table)
            : base(mapperFactory, table)
        {
            this.table = table;
        }

        /// <summary>
        /// Gets the by ticket identifier.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>
        /// TblTicketMasterDto objects
        /// </returns>
        public IEnumerable<TblTicketMasterDto> GetByTicketId(long ticketId)
        {
            var historyItems = this.table.Find(m => m.TicketId == ticketId);

            var items = historyItems.Select(historyItem =>
            {
                var resultData = JsonConvert.DeserializeObject<TblTicketMasterDto>(historyItem.Value);
                resultData.Id = historyItem.Id;
                return resultData;
            }).OrderByDescending(x => x.Id);

            return items;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        public ResultMessage<TblTicketHistoryDto> Save(TblTicketMasterDto sourceData, long userId)
        {
            var result = new ResultMessage<TblTicketHistoryDto>();

            var data = new TblTicketHistoryDto
                           {
                               TicketId = sourceData.Id,
                               Value = JsonConvert.SerializeObject(sourceData)
                           };

            var dbResult = this.Save(new List<TblTicketHistoryDto> { data }, userId);

            if (dbResult == null)
            {
                return result;
            }

            result.Messages.AddRange(dbResult.Messages);

            if (!result.IsError && dbResult.Item != null)
            {
                result.Item = dbResult.Item.FirstOrDefault();
            }

            return result;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="sourceDataList">The source data list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        public ResultMessage<IEnumerable<TblTicketHistoryDto>> Save(IEnumerable<TblTicketHistoryDto> sourceDataList, long userId)
        {
            var result = new ResultMessage<IEnumerable<TblTicketHistoryDto>>();

            if (userId == 0)
            {
                result.Messages.Add(new Message(null, "User Id can not be null"));
            }
            else
            {
                foreach (var entity in sourceDataList)
                {
                    entity.ModifiedBy = userId;
                    entity.CreatedBy = userId;
                    entity.CreatedOn = DateTime.Now;
                }

                result = this.SaveOrUpdate(sourceDataList, userId);
            }

            return result;
        }
    }
}
