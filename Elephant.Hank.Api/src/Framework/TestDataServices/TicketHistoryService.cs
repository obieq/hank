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
    using Resources.Json;
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
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketHistoryService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TicketHistoryService(IMapperFactory mapperFactory, IRepository<TblTicketHistory> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.table = table;
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
            var value = JsonConvert.SerializeObject(sourceData);

            var map = this.mapperFactory.GetMapper<TblTicketMasterDto, TblTicketHistoryDto>().Map(sourceData);

            var dbResult = this.Save(new List<TblTicketHistoryDto> { map }, userId, value);

            if (dbResult == null)
            {
                return result;
            }

            result.Messages.AddRange(dbResult.Messages);

            if (!result.IsError && dbResult.Item != null && dbResult.Item.Any())
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
        /// <param name="value">Value of entity in jsonformat</param>
        /// <returns>
        /// Tin object
        /// </returns>
        public ResultMessage<IEnumerable<TblTicketHistoryDto>> Save(IEnumerable<TblTicketHistoryDto> sourceDataList, long userId, string value)
        {
            var result = new ResultMessage<IEnumerable<TblTicketHistoryDto>>();

            var entities = sourceDataList.Select(this.mapperFactory.GetMapper<TblTicketHistoryDto, TblTicketHistory>().Map).ToList();

            if (userId == 0)
            {
                result.Messages.Add(new Message(null, "User Id can not be null"));
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.ModifiedBy = userId;
                    entity.TicketId = entity.Id;
                    entity.Value = value;
                    entity.CreatedBy = userId;
                    entity.CreatedOn = DateTime.Now;
                    this.table.Insert(entity);
                }

                var resultCount = this.table.Commit();

                if (resultCount == 0)
                {
                    result.Messages.Add(new Message(null, "Record not found!"));
                }
                else
                {
                    result.Item = entities.Select(this.mapperFactory.GetMapper<TblTicketHistory, TblTicketHistoryDto>().Map);
                }
            }

            return result;
        }

        /// <summary>
        /// Get allowed actions for sql steps
        /// </summary>
        /// <returns>TblActionDto list</returns>
        public ResultMessage<IEnumerable<TblTicketHistoryDto>> GetActionForSqlTestStep()
        {
            var result = new ResultMessage<IEnumerable<TblTicketHistoryDto>>();
            var entity = this.Table.Find(x => (x.Id == ActionConstants.Instance.LogTextActionId || x.Id == ActionConstants.Instance.SetVariableActionId) && x.IsDeleted != true).ToList();
            var mapper = this.mapperFactory.GetMapper<TblTicketHistory, TblTicketHistoryDto>();
            result.Item = entity.Select(mapper.Map).ToList();
            return result;
        }
    }
}
