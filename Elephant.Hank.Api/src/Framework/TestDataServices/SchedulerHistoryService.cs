// ---------------------------------------------------------------------------------------------------
// <copyright file="SchedulerHistoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-29</date>
// <summary>
//     The SchedulerHistoryService class
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
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SchedulerHistoryService class
    /// </summary>
    public class SchedulerHistoryService : GlobalService<TblSchedulerHistoryDto, TblSchedulerHistory>, ISchedulerHistoryService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The test queue service
        /// </summary>
        private readonly ITestQueueService testQueueService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerHistoryService" /> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="testQueueService">The test queue service.</param>
        public SchedulerHistoryService(IMapperFactory mapperFactory, IRepository<TblSchedulerHistory> table, ITestQueueService testQueueService)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.testQueueService = testQueueService;
        }

        /// <summary>
        /// Gets the by scheduler identifier.
        /// </summary>
        /// <param name="schedulerId">The scheduler identifier.</param>
        /// <returns>TblSchedulerHistoryDto object</returns>
        public ResultMessage<IEnumerable<TblSchedulerHistoryDto>> GetBySchedulerId(long schedulerId)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerHistoryDto>>();

            var entity = this.Table.Find(x => x.SchedulerId == schedulerId && x.IsDeleted != true).OrderByDescending(x => x.CreatedOn).ToList();

            var mapper = this.mapperFactory.GetMapper<TblSchedulerHistory, TblSchedulerHistoryDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }

        /// <summary>
        /// Updates the name of the status by group.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="status">The status.</param>
        /// <param name="emailStatus">The email status.</param>
        /// <returns>
        /// Updated TblSchedulerHistoryDto object
        /// </returns>
        public ResultMessage<IEnumerable<TblSchedulerHistoryDto>> UpdateStatusByGroupName(long userId, string groupName, SchedulerExecutionStatus? status, SchedulerHistoryEmailStatus? emailStatus)
        {
            var result = new ResultMessage<IEnumerable<TblSchedulerHistoryDto>>();

            var entity = this.Table.Find(x => x.GroupName == groupName && x.IsDeleted != true).ToList();

            entity.ForEach(
                x =>
                {
                    x.Status = status ?? x.Status;
                    x.EmailStatus = emailStatus ?? x.EmailStatus;
                    x.ModifiedOn = DateTime.Now;
                    x.ModifiedBy = userId;
                });

            this.Table.Commit();

            if (status.HasValue && status.Value == SchedulerExecutionStatus.Completed)
            {
                // Mark tests processed
                this.testQueueService.UpdateTestQueueProcessingFlag(userId, groupName, true);
            }

            var mapper = this.mapperFactory.GetMapper<TblSchedulerHistory, TblSchedulerHistoryDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }
    }
}
