// ---------------------------------------------------------------------------------------------------
// <copyright file="DbLogService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The DbLogService class
// </summary>
// --------------------------------------------------------------------------------------------------- 

namespace Elephant.Hank.Framework.TestDataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Npgsql;

    /// <summary>
    /// The DbLogService Service
    /// </summary>
    public class DbLogService : GlobalService<TblDbLogDto, TblDbLog>, IDbLogService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbLogService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public DbLogService(IMapperFactory mapperFactory, IRepository<TblDbLog> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Roll back/forward the data entry
        /// </summary>
        /// <param name="dblogid">the logger id</param>
        /// <param name="tooldvalue">rolling identifier</param>
        /// <returns>return proc execution status</returns>
        public int RollData(long dblogid, bool tooldvalue)
        {
            return this.Table.ExecuteSqlCommand("Select * from procrollbackdata(@dblogid,@tooldvalue);", new NpgsqlParameter("@dblogid", dblogid), new NpgsqlParameter("@tooldvalue", tooldvalue));
        }

        /// <summary>
        /// Get Log data with in the range
        /// </summary>
        /// <param name="start">start date time</param>
        /// <param name="end">end date time</param>
        /// <returns>List of TBlLogDto Object with in the provided range</returns>
        public ResultMessage<IEnumerable<TblDbLogDto>> GetDataWithInDateTimeRange(DateTime start, DateTime end)
        {
            var result = new ResultMessage<IEnumerable<TblDbLogDto>>();

            var entities = this.Table.Find(m => m.CreatedOn >= start && m.CreatedOn <= end).ToList();
            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblDbLog, TblDbLogDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.Id);
            }

            return result;
        }

        /// <summary>
        /// Get Data in chunks of 50
        /// </summary>
        /// <param name="chunk">last chunk identifier</param>
        /// <param name="tableType">Table type filter</param>
        /// <param name="model">date range filter</param>
        /// <returns>TblDbLog List filtered object</returns>
        public ResultMessage<IEnumerable<TblDbLogDto>> GetChunk(long chunk, string tableType, SearchLogModel model)
        {
            var result = new ResultMessage<IEnumerable<TblDbLogDto>>();

            List<TblDbLog> entities;

            if (chunk == 0)
            {
                if (tableType == "All")
                {
                    entities = model.StartDate == DateTime.MinValue ? this.Table.GetAll().OrderByDescending(m => m.Id).Take(50).ToList() : this.Table.Find(m => m.CreatedOn >= model.StartDate && m.CreatedOn <= model.EndDate).OrderByDescending(m => m.Id).Take(50).ToList();
                }
                else
                {
                    entities = model.StartDate == DateTime.MinValue ? this.Table.Find(m => m.TableType == tableType).OrderByDescending(m => m.Id).Take(50).ToList() : this.Table.Find(m => m.TableType == tableType && m.CreatedOn >= model.StartDate && m.CreatedOn <= model.EndDate).OrderByDescending(m => m.Id).Take(50).ToList();
                }
            }
            else
            {
                if (tableType == "All")
                {
                    entities = model.StartDate == DateTime.MinValue ? this.Table.Find(m => m.Id < chunk).OrderByDescending(m => m.Id).Take(50).ToList() : this.Table.Find(m => m.Id < chunk && m.CreatedOn >= model.StartDate && m.CreatedOn <= model.EndDate).OrderByDescending(m => m.Id).Take(50).ToList();
                }
                else
                {
                    entities = model.StartDate == DateTime.MinValue ? this.Table.Find(m => m.Id < chunk && m.TableType == tableType).OrderByDescending(m => m.Id).Take(50).ToList() : this.Table.Find(m => m.Id < chunk && m.TableType == tableType && m.CreatedOn >= model.StartDate && m.CreatedOn <= model.EndDate).OrderByDescending(m => m.Id).Take(50).ToList();
                }
            }

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblDbLog, TblDbLogDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.Id);
            }

            return result;
        }
    }
}
