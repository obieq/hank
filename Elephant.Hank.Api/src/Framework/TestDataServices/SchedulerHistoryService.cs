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
        /// Cancels the execution.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="schedulerHistoryId">The scheduler history identifier.</param>
        /// <returns>
        /// TblSchedulerHistoryDto object
        /// </returns>
        public ResultMessage<TblSchedulerHistoryDto> CancelExecution(long userId, long schedulerHistoryId)
        {
            var data = this.GetById(schedulerHistoryId);

            if (!data.IsError && (data.Item.Status == SchedulerExecutionStatus.InProgress || data.Item.Status == SchedulerExecutionStatus.InQueue))
            {
                data.Item.IsCancelled = !data.Item.IsCancelled;

                data = this.SaveOrUpdate(data.Item, userId);
            }
            else if (!data.IsError)
            {
                data.Messages.Add(new Message("Invalid state of scheduler history, may be changed by someother process, kindly refresh the page!"));
            }

            return data;
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
        /// Gets the name of the by group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>TblSchedulerHistoryDto object</returns>
        public ResultMessage<TblSchedulerHistoryDto> GetByGroupName(string groupName)
        {
            var result = new ResultMessage<TblSchedulerHistoryDto>();

            var entity = this.Table.Find(x => x.GroupName == groupName && x.IsDeleted != true).FirstOrDefault();

            if (entity != null)
            {
                var mapper = this.mapperFactory.GetMapper<TblSchedulerHistory, TblSchedulerHistoryDto>();
                result.Item = mapper.Map(entity);
            }
            else
            {
                result.Messages.Add(new Message("Record not found!"));
            }

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
            else if (status == SchedulerExecutionStatus.Cancelled)
            {
                this.testQueueService.UpdateTestQueueStatusByGroupName(userId, groupName, ExecutionReportStatus.Cancelled);
            }

            var mapper = this.mapperFactory.GetMapper<TblSchedulerHistory, TblSchedulerHistoryDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }
    }
}
