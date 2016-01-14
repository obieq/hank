// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupModuleAccessService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-13</date>
// <summary>
//     The GroupModuleAccessService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;
    using System;
    using Elephant.Hank.Common.LogService;

    /// <summary>
    /// The GroupModuleAccessService class
    /// </summary>
    public class GroupModuleAccessService : GlobalService<TblGroupModuleAccessDto, TblGroupModuleAccess>, IGroupModuleAccessService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The Module service
        /// </summary>
        private readonly IModuleService moduleService;

        /// <summary>
        /// The Module service
        /// </summary>
        private readonly IWebsiteService websiteService;

        /// <summary>
        /// The Module service
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblGroupModuleAccess> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupModuleAccessService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public GroupModuleAccessService(IMapperFactory mapperFactory, IRepository<TblGroupModuleAccess> table, IModuleService moduleService, ILoggerService loggerService, IWebsiteService websiteService)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.moduleService = moduleService;
            this.websiteService = websiteService;
            this.table = table;
        }

        /// <summary>
        /// Add/Update the website to group
        /// </summary>
        /// <param name="GroupId">the Group Identifier</param>
        /// <param name="websiteIdList">Array of Website</param>
        /// <returns>Added entries in table TblGroupModuleAccessDto</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> AddUpdateWebsiteToGroup(long groupId, long[] websiteIdList)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            try
            {
                var groupModuleAccessList = this.table.Find(x => x.GroupId == groupId).ToList();

                if (groupModuleAccessList.Count > 0)
                {
                    groupModuleAccessList.Where(x => websiteIdList.All(web => web != x.WebsiteId)).ToList().ForEach(x => { x.IsDeleted = true; x.CanRead = false; x.CanWrite = false; x.CanDelete = false; });
                    groupModuleAccessList.Where(x => websiteIdList.Any(web => web == x.WebsiteId)).ToList().ForEach(x => { x.IsDeleted = false; x.CanRead = true; x.CanWrite = false; x.CanDelete = false; });
                    this.table.Commit();
                }
                var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccess, TblGroupModuleAccessDto>();
                result.Item = groupModuleAccessList.Select(mapper.Map).ToList();
            }
            catch (Exception ex)
            {
                this.loggerService.LogException(ex);
                result.Messages.Add(new Message("function exception", ex.Message));
            }
            return result;
        }

        /// <summary>
        /// Add the GroupModuleAccess entries in bulk
        /// </summary>
        /// <param name="groupModuleAccessList">Group module access list to be entered in table</param>
        /// <param name="userId">id of user who perform this action</param>
        /// <returns>List of added entries</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> AddInBulk(IEnumerable<TblGroupModuleAccessDto> groupModuleAccessList, long userId)
        {
            var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccessDto, TblGroupModuleAccess>();
            var groupModuleAccessListForDB = groupModuleAccessList.Select(mapper.Map).ToList();
            groupModuleAccessListForDB.ForEach(x => this.table.Insert(x));
            this.table.Commit();
            return this.GetByGroupId(groupModuleAccessList.FirstOrDefault().GroupId);
        }

        /// <summary>
        /// Get the TblGroupModuleAccessDto list by group id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <returns>List if TblGroupModuleAccessDto that matched the groupidentifier provided in parameter</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> GetByGroupId(long groupId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccess, TblGroupModuleAccessDto>();
            IEnumerable<TblGroupModuleAccess> groupModuleAccess = this.table.Find(x => x.GroupId == groupId && x.IsDeleted != true).ToList();
            result.Item = groupModuleAccess.Select(mapper.Map).ToList();
            return result;
        }
    }
}
