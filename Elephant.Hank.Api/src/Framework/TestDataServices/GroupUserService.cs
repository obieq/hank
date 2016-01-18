// ---------------------------------------------------------------------------------------------------
// <copyright file="GroupUserService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-12</date>
// <summary>
//     The GroupUserService class
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
    /// The Group User class.
    /// </summary>
    public class GroupUserService : GlobalService<TblGroupUserDto, TblGroupUser>, IGroupUserService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IRepository<TblGroupUser> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupUserService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public GroupUserService(IMapperFactory mapperFactory, IRepository<TblGroupUser> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.table = table;
        }

        /// <summary>
        /// the group user
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <returns>TblGroupUserDto list object with the worvided group id</returns>
        public ResultMessage<IEnumerable<TblGroupUserDto>> GetByGroupId(long groupId)
        {
            var result = new ResultMessage<IEnumerable<TblGroupUserDto>>();

            var entity = this.table.Find(x => x.GroupId == groupId && x.IsDeleted != true).ToList();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblGroupUser, TblGroupUserDto>();
                result.Item = entity.Select(mapper.Map);
            }

            return result;
        }

        /// <summary>
        /// Get the GroupUser entry with groupid and user is whose isdeleted is either true or false
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <param name="userId">the user identifier</param>
        /// <returns>TblGroupUserDto object</returns>
        public ResultMessage<TblGroupUserDto> GetByGroupIdAndUserIdEitherDeletedOrNonDeleted(long groupId, long userId)
        {
            var result = new ResultMessage<TblGroupUserDto>();

            var entity = this.Table.Find(x => x.GroupId == groupId).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblGroupUser, TblGroupUserDto>().Map(entity);
            }

            return result;
        }
    }
}
