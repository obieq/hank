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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Json;
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
        /// The tableGroup
        /// </summary>
        private readonly IRepository<TblGroup> tableGroup;

        /// <summary>
        ///  Initializes a new instance of the <see cref="GroupModuleAccessService"/> class.
        /// </summary>
        /// <param name="mapperFactory">the mapper factory </param>
        /// <param name="table">the table</param>
        /// <param name="moduleService">the module service object</param>
        /// <param name="loggerService">the logger service</param>
        /// <param name="websiteService">the website service</param>
        /// <param name="tableGroup">the table group</param>
        public GroupModuleAccessService(IMapperFactory mapperFactory, IRepository<TblGroupModuleAccess> table, IModuleService moduleService, ILoggerService loggerService, IWebsiteService websiteService, IRepository<TblGroup> tableGroup)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.moduleService = moduleService;
            this.websiteService = websiteService;
            this.table = table;
            this.tableGroup = tableGroup;
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
            try
            {
                var groupModuleAccessList = this.table.Find(x => x.GroupId == groupId).ToList();

                if (groupModuleAccessList.Count > 0)
                {
                    groupModuleAccessList.Where(x => websiteIdList.All(web => web != x.WebsiteId)).ToList().ForEach(x => { x.IsDeleted = true; x.CanRead = false; x.CanWrite = false; x.CanDelete = false; x.CanExecute = false; });
                    groupModuleAccessList.Where(x => websiteIdList.Any(web => web == x.WebsiteId)).ToList().ForEach(x => { x.IsDeleted = false; x.CanRead = true; x.CanWrite = false; x.CanDelete = false; x.CanExecute = false; });
                }

                long[] websiteRefNotExist = websiteIdList.Where(x => groupModuleAccessList.All(y => y.WebsiteId != x)).ToArray();
                if (websiteRefNotExist.Count() > 0)
                {
                    var groupList = this.tableGroup.GetAll();
                    var moduleResult = this.moduleService.GetAll();
                    foreach (var website in websiteRefNotExist)
                    {
                        foreach (var group in groupList)
                        {
                            foreach (var module in moduleResult.Item)
                            {
                                TblGroupModuleAccess groupModuleAccess;
                                if (groupId == group.Id)
                                {
                                    TblGroupModuleAccessDto groupModuleAccessDto = new TblGroupModuleAccessDto { GroupId = group.Id, IsDeleted = false, ModifiedBy = userId, CreatedBy = userId, ModuleId = module.Id, WebsiteId = website, CanDelete = false, CanRead = true, CanWrite = false, CanExecute = false };
                                    groupModuleAccess = this.mapperFactory.GetMapper<TblGroupModuleAccessDto, TblGroupModuleAccess>().Map(groupModuleAccessDto);
                                }
                                else
                                {
                                    TblGroupModuleAccessDto groupModuleAccessDto = new TblGroupModuleAccessDto { GroupId = group.Id, IsDeleted = true, ModifiedBy = userId, CreatedBy = userId, ModuleId = module.Id, WebsiteId = website, CanDelete = false, CanRead = false, CanWrite = false, CanExecute = false };
                                    groupModuleAccess = this.mapperFactory.GetMapper<TblGroupModuleAccessDto, TblGroupModuleAccess>().Map(groupModuleAccessDto);
                                }

                                this.table.Insert(groupModuleAccess);
                            }
                        }
                    }                   
                }

                this.table.Commit();
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

        /// <summary>
        /// Get the TblGroupModuleAccessDto list by group id and website id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        ///  <param name="websiteId">the website identifier</param>
        /// <returns>List if TblGroupModuleAccessDto that matched the groupidentifier provided in parameter</returns>
        public ResultMessage<IEnumerable<TblGroupModuleAccessDto>> GetByGroupIdAndWebsiteId(long groupId, long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupModuleAccessDto>>();
            var mapper = this.mapperFactory.GetMapper<TblGroupModuleAccess, TblGroupModuleAccessDto>();
            IEnumerable<TblGroupModuleAccess> groupModuleAccess = this.table.Find(x => x.GroupId == groupId && x.WebsiteId == websiteId && x.IsDeleted != true).ToList();
            result.Item = groupModuleAccess.Select(mapper.Map).ToList();
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
            if (moduleAccessDtoList.Count() > 0)
            {
                TblGroupModuleAccessDto defaultGroupModule = moduleAccessDtoList.FirstOrDefault();
                var moduleAccessList = this.table.Find(x => x.GroupId == defaultGroupModule.GroupId && x.WebsiteId == defaultGroupModule.WebsiteId).ToList();
                foreach (var item in moduleAccessDtoList)
                {
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
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "userid", userId } };

            var entities = this.Table.SqlQuery<ModuleAuthenticationModel>("Select * from procgetmoduleauthenticatedtouser(@userid);", dictionary).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = entities;
            }

            return result;
        }
    }
}
