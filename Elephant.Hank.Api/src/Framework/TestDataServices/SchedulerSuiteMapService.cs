// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerSuiteMapService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-20</date>
// <summary>
//     The SchedulerSuiteMapService class
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
    using Elephant.Hank.DataService.DBSchema.Linking;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SchedulerSuiteService class
    /// </summary>
    public class SchedulerSuiteMapService : GlobalService<TblLnkSchedulerSuiteDto, TblLnkSchedulerSuite>, ISchedulerSuiteMapService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;
     
        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerSuiteMapService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public SchedulerSuiteMapService(IMapperFactory mapperFactory, IRepository<TblLnkSchedulerSuite> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Get Scheduler Suite by SchedulerId
        /// </summary>
        /// <param name="schedulerId">scheduler identifier</param>
        /// <returns>TblSchedulerSuiteDto object</returns>
        public ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>> GetBySchedulerId(long schedulerId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>>();

            var suiteTestMap = this.Table.Find(x => x.SchedulerId == schedulerId && !x.IsDeleted).ToList();

            var mapper = this.mapperFactory.GetMapper<TblLnkSchedulerSuite, TblLnkSchedulerSuiteDto>();

            result.Item = suiteTestMap.Select(mapper.Map);

            return result;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="schedulerId">The suite identifier.</param>
        /// <param name="sourceData">The source data.</param>
        /// <returns>
        /// TblSchedulerSuiteDto objects
        /// </returns>
        public ResultMessage<IEnumerable<TblLnkSchedulerSuiteDto>> SaveOrUpdate(long userId, long schedulerId, List<TblLnkSchedulerSuiteDto> sourceData)
        {
            sourceData.ForEach(
                x =>
                {
                    x.SchedulerId = schedulerId;
                    x.CreatedBy = userId;
                    x.CreatedOn = DateTime.Now;
                    x.ModifiedBy = userId;
                    x.ModifiedOn = DateTime.Now;
                });

            var suiteSchedulerMap = this.Table.Find(x => x.SchedulerId == schedulerId).ToList();

            if (suiteSchedulerMap.Count > 0)
            {
                suiteSchedulerMap.Where(x => sourceData.All(src => src.SuiteId != x.SuiteId)).ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                    x.ModifiedBy = userId;
                    x.ModifiedOn = DateTime.Now;
                });
                suiteSchedulerMap.Where(x => sourceData.Any(src => src.SuiteId == x.SuiteId)).ToList().ForEach(x => 
                {
                    x.IsDeleted = false;
                    x.ModifiedBy = userId;
                    x.ModifiedOn = DateTime.Now;
                });
            }

            var mapper = this.mapperFactory.GetMapper<TblLnkSchedulerSuiteDto, TblLnkSchedulerSuite>();

            var newLinks = sourceData.Where(x => suiteSchedulerMap.All(src => src.SuiteId != x.SuiteId)).Select(mapper.Map).ToList();

            newLinks.ForEach(x => this.Table.Insert(x));

            this.Table.Commit();

            return this.GetBySchedulerId(schedulerId);
        }
    }
}
