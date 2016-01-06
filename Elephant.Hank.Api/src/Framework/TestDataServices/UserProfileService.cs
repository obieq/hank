// ---------------------------------------------------------------------------------------------------
// <copyright file="UserProfileService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The UserProfileService class
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
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    
    /// <summary>
    /// The UserProfileService class
    /// </summary>
    public class UserProfileService : GlobalService<TblUserProfileDto, TblUserProfile>, IUserProfileService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IRepository<TblUserProfile> table;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly CustomUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="userManager">user manager instance.</param>
        public UserProfileService(IMapperFactory mapperFactory, IRepository<TblUserProfile> table, CustomUserManager userManager)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.table = table;
            this.userManager = userManager;
        }

        /// <summary>
        /// Gets the by userid.
        /// </summary>
        /// <param name="userid">The user identifier.</param>
        /// <returns>TblUserProfileDto object</returns>
        public ResultMessage<TblUserProfileDto> GetByUserId(long userid)
        {
            var result = new ResultMessage<TblUserProfileDto>();

            var entity = this.Table.Find(x => x.UserId == userid && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblUserProfile, TblUserProfileDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by userid.
        /// </summary>
        /// <param name="userProfileDto">The user profile object.</param>
        /// <param name="userid">The user identifier.</param>
        /// <returns>TblUserProfileDto object</returns>
        public ResultMessage<TblUserProfileDto> SaveUpdateCustom(TblUserProfileDto userProfileDto, long userid)
        {
            var result = new ResultMessage<TblUserProfileDto>();
            var entity = this.mapperFactory.GetMapper<TblUserProfileDto, TblUserProfile>().Map(userProfileDto);
            entity.ModifiedBy = userid;

            if (entity.Id == 0)
            {
                entity.CreatedBy = userid;
                entity.CreatedOn = DateTime.Now;
                this.table.Insert(entity);
            }
            else
            {
                var user = this.userManager.FindByIdAsync(entity.UserId);
                user.Result.FirstName = entity.User.FirstName;
                user.Result.LastName = entity.User.LastName;
                entity.ModifiedOn = DateTime.Now;
                this.userManager.UpdateAsync(user.Result);
                this.table.Update(entity);
            }

            var resultCount = this.table.Commit();

            if (resultCount == 0)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblUserProfile, TblUserProfileDto>().Map(entity);
            }

            return result;
        }
    }
}
