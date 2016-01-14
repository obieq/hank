// ---------------------------------------------------------------------------------------------------
// <copyright file="IGroupUserService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The IGroupUserService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IGroupUserService interface
    /// </summary>
    public interface IGroupUserService : IBaseService<TblGroupUserDto>
    {
        /// <summary>
        /// the group user
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <returns>TblGroupUserDto list object with the worvided group id</returns>
        ResultMessage<IEnumerable<TblGroupUserDto>> GetByGroupId(long groupId);

        /// <summary>
        /// Get the GroupUser entry with groupid and user is whose isdeleted is either true or false
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <param name="userId">the user identifier</param>
        /// <returns>TblGroupUserDto object</returns>
        ResultMessage<TblGroupUserDto> GetByGroupIdAndUserIdEitherDeletedOrNonDeleted(long groupId, long userId);
    }
}
