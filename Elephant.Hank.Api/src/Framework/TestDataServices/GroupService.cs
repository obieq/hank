// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-12</date>
// <summary>
//     The GroupService class
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
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The GroupService class.
    /// </summary>
    public class GroupService : GlobalService<TblGroupDto, TblGroup>, IGroupService
    {
        /// <summary>
        /// The module service
        /// </summary>
        private readonly IModuleService moduleService;

        /// <summary>
        /// The website service
        /// </summary>
        private readonly IWebsiteService websiteService;

        /// <summary>
        /// The group module access service
        /// </summary>
        private readonly IGroupModuleAccessService groupModuleAccessService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblGroup> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="groupModuleAccessService">the group module access service</param>
        /// <param name="moduleService">the module service</param>
        /// <param name="websiteService">the website service</param>
        public GroupService(IMapperFactory mapperFactory, IRepository<TblGroup> table, IGroupModuleAccessService groupModuleAccessService, IModuleService moduleService, IWebsiteService websiteService)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.groupModuleAccessService = groupModuleAccessService;
            this.moduleService = moduleService;
            this.websiteService = websiteService;
            this.table = table;
        }

        /// <summary>
        /// Add new Group
        /// </summary>
        /// <param name="group">group object</param>
        /// <returns>Added Group object</returns>
        public ResultMessage<TblGroupDto> AddNewGroup(TblGroupDto group, long userId)
        {
            var result = new ResultMessage<TblGroupDto>();
            var checkGroupExist = this.GetByGroupName(group.GroupName);
            if (checkGroupExist.IsError)
            {
                result = this.SaveOrUpdate(group, userId);
                result = this.GetByGroupName(group.GroupName);
                var moduleResult = this.moduleService.GetAll();
                var websiteResult = this.websiteService.GetAll();
                List<TblGroupModuleAccessDto> groupModuleAccessList = new List<TblGroupModuleAccessDto>();
                if (moduleResult.Item != null && websiteResult.Item != null)
                {
                    foreach (var website in websiteResult.Item)
                    {
                        foreach (var module in moduleResult.Item)
                        {
                            TblGroupModuleAccessDto groupModuleAccess = new TblGroupModuleAccessDto { GroupId = result.Item.Id, IsDeleted = true, ModifiedBy = userId, CreatedBy = userId, ModuleId = module.Id, WebsiteId = website.Id, CanDelete = false, CanRead = false, CanWrite = false, CanExecute = false };
                            groupModuleAccessList.Add(groupModuleAccess);
                        }
                    }

                    this.groupModuleAccessService.AddInBulk(groupModuleAccessList, userId);
                }
            }
            else
            {
                return checkGroupExist;
            }

            return result;
        }

        /// <summary>
        /// Get the group by name 
        /// </summary>
        /// <param name="name">name of the group</param>
        /// <returns>TblGroupObject </returns>
        public ResultMessage<TblGroupDto> GetByGroupName(string name)
        {
            var result = new ResultMessage<TblGroupDto>();

            var entity = this.Table.Find(x => x.GroupName.ToLower() == name.ToLower() && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblGroup, TblGroupDto>().Map(entity);
            }

            return result;
        }
    }
}
