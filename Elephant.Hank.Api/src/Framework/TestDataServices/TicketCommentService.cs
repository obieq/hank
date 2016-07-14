// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketCommentService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-07-14</date>
// <summary>
//     The TicketCommentService class
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
    /// The TicketCommentService class
    /// </summary>
    public class TicketCommentService : GlobalService<TblTicketCommentDto, TblTicketComment>, ITicketCommentService
    {
        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblTicketComment> table;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketCommentService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TicketCommentService(IMapperFactory mapperFactory, IRepository<TblTicketComment> table)
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
        /// <param name="ticketId">The ticketId.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        public ResultMessage<TblTicketCommentDto> Save(TblTicketCommentDto sourceData, long userId, long ticketId, string comment)
        {
            var result = new ResultMessage<TblTicketCommentDto>();
            
            var map = this.mapperFactory.GetMapper<TblTicketCommentDto, TblTicketCommentDto>().Map(sourceData);

            var dbResult = this.Save(new List<TblTicketCommentDto> { map }, userId, ticketId, sourceData.Description);

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
        /// <param name="ticketId">The ticketId.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        public ResultMessage<IEnumerable<TblTicketCommentDto>> Save(IEnumerable<TblTicketCommentDto> sourceDataList, long userId, long ticketId, string comment)
        {
            var result = new ResultMessage<IEnumerable<TblTicketCommentDto>>();

            var entities = sourceDataList.Select(this.mapperFactory.GetMapper<TblTicketCommentDto, TblTicketComment>().Map).ToList();

            if (userId == 0)
            {
                result.Messages.Add(new Message(null, "User Id can not be null"));
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.ModifiedBy = userId;
                    entity.Description = comment;
                    entity.TicketId = ticketId;
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
                    result.Item = entities.Select(this.mapperFactory.GetMapper<TblTicketComment, TblTicketCommentDto>().Map);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the by ticket identifier.
        /// </summary>
        /// <param name="ticketId">The ticket identifier.</param>
        /// <returns>
        /// TblTicketCommentDto objects
        /// </returns>
        public IEnumerable<TblTicketCommentDto> GetByTicketId(long ticketId)
        {
            var comments = this.table.Find(m => m.TicketId == ticketId);

            var items = comments.Select(comment => new TblTicketCommentDto
            {
                Description = comment.Description,
                TicketId = comment.TicketId,
                CreatedBy = comment.CreatedBy,
                Id = comment.Id,
            }).OrderByDescending(x => x.Id);
            return items;
        }
    }
}
