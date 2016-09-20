// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-20</date>
// <summary>
//     The SchedulerService class
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
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;

    using Npgsql;

    /// <summary>
    /// the SchedulerService class
    /// </summary>
    public class SchedulerService : GlobalService<TblSchedulerDto, TblScheduler>, ISchedulerService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public SchedulerService(IMapperFactory mapperFactory, IRepository<TblScheduler> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Get List of Scheduler by website identifier
        /// </summary>
        /// <param name="webSiteId">website identifier</param>
        /// <returns>List object of TblSchedulerDto</returns>
        public ResultMessage<IEnumerable<TblSchedulerDto>> GetByWebsiteId(long webSiteId)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerDto>>();

            var entity = this.Table.Find(x => x.WebsiteId == webSiteId && x.IsDeleted != true).ToList();

            var mapper = this.mapperFactory.GetMapper<TblScheduler, TblSchedulerDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }

        /// <summary>
        /// sets the force execute flag to true
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="schedulerId">scheduler identifier</param>
        /// <returns>
        /// object of TblSchedulerDto
        /// </returns>
        public ResultMessage<TblSchedulerDto> ForceExecute(long userId, long schedulerId)
        {
            var result = new ResultMessage<TblSchedulerDto>();

            var entity = this.Table.Find(x => x.Id == schedulerId && x.IsDeleted != true).FirstOrDefault();

            if (entity != null)
            {
                entity.ForceExecute = true;
                entity.ModifiedBy = userId;
                entity.ModifiedOn = DateTime.Now;

                this.Table.Update(entity);

                this.Table.Commit();

                var mapper = this.mapperFactory.GetMapper<TblScheduler, TblSchedulerDto>();
                result.Item = mapper.Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Forces the execute.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="schedulerId">The scheduler identifier.</param>
        /// <param name="target">The target.</param>
        /// <param name="port">The port.</param>
        /// <returns>
        /// Group name
        /// </returns>
        public ResultMessage<string> ForceExecute(long userId, long schedulerId, string target, int? port)
        {
            var result = new ResultMessage<string>();

            var entity = this.Table.Find(x => x.Id == schedulerId && x.IsDeleted != true).FirstOrDefault();

            if (entity != null)
            {
                entity.ForceExecute = true;
                entity.ModifiedBy = userId;
                entity.ModifiedOn = DateTime.Now;

                if (entity.Settings == null)
                {
                    entity.Settings = new SchedulerSettings();
                }

                entity.Settings.Target = target;
                entity.Settings.Port = port;

                this.Table.Update(entity);

                this.Table.Commit();

                var groupName = DateTime.Now.ToGroupName();

                this.Table.ExecuteSqlCommand("select * from procprocessschedulerdata(@groupName)", new NpgsqlParameter("@groupName", groupName));

                result.Item = groupName + "-" + schedulerId;
            }
            else
            {
                result.Messages.Add(new Message("Scheduler not found!"));
            }

            return result;
        }

        /// <summary>
        /// Gets the by URL identifier.
        /// </summary>
        /// <param name="urlId">The URL identifier.</param>
        /// <returns>returns the list of Scheduler by mathing urlid</returns>
        public ResultMessage<IEnumerable<TblSchedulerDto>> GetByUrlId(long urlId)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerDto>>();

            var entities = this.Table.Find(x => x.UrlId == urlId && x.IsDeleted != true).ToList();

            if (entities.Count == 0)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblScheduler, TblSchedulerDto>();
                result.Item = entities.Select(mapper.Map);
            }           

            return result;
        }
    }
}
