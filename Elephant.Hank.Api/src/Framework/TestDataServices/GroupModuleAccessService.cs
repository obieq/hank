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
    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.Services;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

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
        /// The cache provider
        /// </summary>
        private readonly ICacheProvider cacheProvider;

        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblGroupModuleAccess> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupModuleAccessService" /> class.
        /// </summary>
        /// <param name="mapperFactory">the mapper factory</param>
        /// <param name="table">the table</param>
        /// <param name="moduleService">the module service object</param>
        /// <param name="cacheProvider">The cache provider.</param>
        public GroupModuleAccessService(IMapperFactory mapperFactory, IRepository<TblGroupModuleAccess> table, IModuleService moduleService, ICacheProvider cacheProvider)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.moduleService = moduleService;
            this.table = table;
            this.cacheProvider = cacheProvider;
        }

        /// <summary>
        /// Add/Update the website to group
        /// </summary>
        /// <param name="groupId">the Group Identifier</param>
        /// <param name="websiteIdList">Array of Website</param>
        /// <param name="userId">the User Identifier</param>
        /// <returns>Added entries in table TblGroupModuleAccessDto</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> AddUpdateWebsiteToGroup(long groupId, long[] websiteIdList, long userId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            var groupModuleAccessList = this.table.Find(x => x.GroupId == groupId && x.IsDeleted == false).ToList();
            var moduleResult = this.moduleService.GetAll();
            foreach (var website in websiteIdList)
            {
                foreach (var module in moduleResult.Item)
                {
                    if (groupModuleAccessList.Where(x => x.ModuleId == module.Id && x.WebsiteId == website).Count() == 0)
                    {
                        this.table.Insert(this.mapperFactory.GetMapper<TblGroupModuleAccessDto, TblGroupModuleAccess>().Map(new TblGroupModuleAccessDto { GroupId = groupId, IsDeleted = false, ModifiedBy = userId, CreatedBy = userId, ModuleId = module.Id, WebsiteId = website, CanDelete = false, CanRead = false, CanWrite = false, CanExecute = false }));
                    }
                }
            }

            this.table.Commit();
            groupModuleAccessList = this.table.Find(x => x.GroupId == groupId && x.IsDeleted == false).ToList();
            groupModuleAccessList.Where(x => websiteIdList.All(web => web != x.WebsiteId)).ToList().ForEach(x => { x.IsDeleted = false; x.CanRead = false; x.CanWrite = false; x.CanDelete = false; x.CanExecute = false; });
            groupModuleAccessList.Where(x => websiteIdList.Any(web => web == x.WebsiteId)).ToList().ForEach(x => { x.IsDeleted = false; x.CanRead = true; x.CanWrite = false; x.CanDelete = false; x.CanExecute = false; });
            this.table.Commit();
            var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccess, TblGroupModuleAccessDto>();
            result.Item = groupModuleAccessList.Select(mapper.Map).ToList();
            return result;
        }

        /// <summary>
        /// Get the TblGroupModuleAccessDto list by group id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <returns>List if TblGroupModuleAccessDto that matched the group identifier provided in parameter</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> GetByGroupId(long groupId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccess, TblGroupModuleAccessDto>();
            IEnumerable<TblGroupModuleAccess> groupModuleAccess = this.table.Find(x => x.GroupId == groupId && x.IsDeleted != true).ToList();
            result.Item = groupModuleAccess.Select(mapper.Map).ToList();
            return result;
        }

        /// <summary>
        /// Get the TblGroupModuleAccessDto list by group id and website id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        ///  <param name="websiteId">the website identifier</param>
        /// <returns>List if TblGroupModuleAccessDto that matched the group identifier provided in parameter</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> GetByGroupIdAndWebsiteId(long groupId, long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccess, TblGroupModuleAccessDto>();
            List<TblGroupModuleAccess> groupModuleAccessList = this.table.Find(x => x.GroupId == groupId && x.WebsiteId == websiteId && x.IsDeleted != true).ToList();
            IEnumerable<TblModuleDto> modules = this.moduleService.GetAll().Item;
            if (groupModuleAccessList.Count() < modules.Count())
            {
                IEnumerable<TblModuleDto> moduleNotAddedList = modules.Where(x => groupModuleAccessList.All(y => y.ModuleId != x.Id)).ToList();
                foreach (var item in moduleNotAddedList)
                {
                    TblGroupModuleAccess groupModuleAccess = new TblGroupModuleAccess { GroupId = groupId, WebsiteId = websiteId, ModuleId = item.Id, Module = this.mapperFactory.GetMapper<TblModuleDto, TblModule>().Map(item) };
                    groupModuleAccessList.Add(groupModuleAccess);
                }
            }

            result.Item = groupModuleAccessList.Select(mapper.Map).ToList();
            return result;
        }

        /// <summary>
        /// Update the TblGroupModuleAccessDto table entries in one go
        /// </summary>
        /// <param name="moduleAccessDtoList">TblGroupModuleAccessDto list object</param>
        /// <param name="userId">the user identifier</param>
        /// <returns>list of updated entries</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> UpdateModuleAccessBulk(IEnumerable<TblGroupModuleAccessDto> moduleAccessDtoList, long userId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            if (moduleAccessDtoList != null && moduleAccessDtoList.Any())
            {
                TblGroupModuleAccessDto defaultGroupModule = moduleAccessDtoList.FirstOrDefault();
                var moduleAccessList = this.table.Find(x => x.GroupId == defaultGroupModule.GroupId && x.WebsiteId == defaultGroupModule.WebsiteId).ToList();
                foreach (var item in moduleAccessDtoList)
                {
                    if (item.Id == 0)
                    {
                        this.table.Insert(new TblGroupModuleAccess { ModifiedBy = userId, CreatedBy = userId, CanRead =true, CanWrite = item.CanWrite, CanDelete = item.CanDelete, CanExecute = item.CanExecute, GroupId = item.GroupId, ModuleId = item.ModuleId, WebsiteId = item.WebsiteId });
                    }
                    moduleAccessList.Where(x => x.Id == item.Id).ToList().ForEach(y => { y.ModifiedBy = userId; y.CreatedBy = userId; y.CanRead = item.CanRead; y.CanWrite = item.CanWrite; y.CanDelete = item.CanDelete; y.CanExecute = item.CanExecute; });
                }

                this.table.Commit();
                var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccess, TblGroupModuleAccessDto>();
                result.Item = moduleAccessList.Select(mapper.Map).ToList();
            }
            else
            {
                result.Messages.Add(new Message(string.Empty, "No Record to update"));
            }

            return result;
        }

        /// <summary>
        /// Get all module authenticated to user
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <returns>List of authenticated module for each website</returns>
        public ResultMessage<IEnumerable<ModuleAuthenticationModel>> GetModuleAuthenticatedToUser(long userId)
        {
            var result = new ResultMessage<IEnumerable<ModuleAuthenticationModel>>();

            IEnumerable<ModuleAuthenticationModel> data;

            if (this.cacheProvider.TryGet(userId.ToString(), out data))
            {
                result.Item = data;
            }
            else
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object> { { "userid", userId } };

                var entities = this.Table.SqlQuery<ModuleAuthenticationModel>("Select * from procgetmoduleauthenticatedtouser(@userid);", dictionary).ToList();

                if (!entities.Any())
                {
                    result.Messages.Add(new Message(null, "Record not found!"));
                }
                else
                {
                    result.Item = entities;

                    this.cacheProvider.Set(userId.ToString(), entities, AppSettings.Get(ConfigConstants.PermissionCacheExpMnt, 30));
                }
            }

            return result;
        }
    }
}
