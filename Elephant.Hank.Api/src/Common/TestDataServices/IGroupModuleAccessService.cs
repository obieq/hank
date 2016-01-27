// ---------------------------------------------------------------------------------------------------
// <copyright file="IGroupModuleAccessService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The IGroupModuleAccessService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The IGroupModuleAccessService interface
    /// </summary>
    public interface IGroupModuleAccessService : IBaseService<TblGroupModuleAccessDto>
    {
        /// <summary>
        /// Add/Update the website to group
        /// </summary>
        /// <param name="groupId">the Group Identifier</param>
        /// <param name="websiteIdList">Array of Website</param>
        /// <param name="userId">the user identifier</param>
        /// <returns>Added entries in table TblGroupModuleAccessDto</returns>
        ResultMessage<IEnumerable<TblGroupModuleAccessDto>> AddUpdateWebsiteToGroup(long groupId, long[] websiteIdList, long userId);

        /// <summary>
        /// Add the GroupModuleAccess entries in bulk
        /// </summary>
        /// <param name="groupModuleAccessList">Group module access list to be entered in table</param>
        /// <param name="userId">id of user who perform this action</param>
        /// <returns>List of added entries</returns>
        ResultMessage<IEnumerable<TblGroupModuleAccessDto>> AddInBulk(IEnumerable<TblGroupModuleAccessDto> groupModuleAccessList, long userId);

        /// <summary>
        /// Get the TblGroupModuleAccessDto list by group id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        /// <returns>List if TblGroupModuleAccessDto that matched the groupidentifier provided in parameter</returns>
        ResultMessage<IEnumerable<TblGroupModuleAccessDto>> GetByGroupId(long groupId);

        /// <summary>
        /// Get the TblGroupModuleAccessDto list by group id and website id
        /// </summary>
        /// <param name="groupId">the group identifier</param>
        ///  <param name="websiteId">the website identifier</param>
        /// <returns>List if TblGroupModuleAccessDto that matched the groupidentifier provided in parameter</returns>
        ResultMessage<IEnumerable<TblGroupModuleAccessDto>> GetByGroupIdAndWebsiteId(long groupId, long websiteId);

        /// <summary>
        /// Update the TblGroupModuleAccessDto table entries in one go
        /// </summary>
        /// <param name="groupModuleAccessList">TblGroupModuleAccessDto list object</param>
        /// <param name="userId">user identifier</param>
        /// <returns>list of updated entries</returns>
        ResultMessage<IEnumerable<TblGroupModuleAccessDto>> UpdateModuleAccessBulk(IEnumerable<TblGroupModuleAccessDto> groupModuleAccessList, long userId);

        /// <summary>
        /// Get all module authenticated to user
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <returns>List of authenticated module for each website</returns>
        ResultMessage<IEnumerable<ModuleAuthenticationModel>> GetModuleAuthenticatedToUser(long userId);
    }
}
