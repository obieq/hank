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
        public GroupService(IMapperFactory mapperFactory, IRepository<TblGroup> table)
            : base(mapperFactory, table)
        {
            this.table = table;
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
