// ---------------------------------------------------------------------------------------------------
// <copyright file="IGroupService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The IGroupService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IGroupService interface
    /// </summary>
    public interface IGroupService : IBaseService<TblGroupDto>
    {
        /// <summary>
        /// Add new Group
        /// </summary>
        /// <param name="group">group object</param>
        /// <param name="userId">the user identifier</param>
        /// <returns>Added Group object</returns>
        ResultMessage<TblGroupDto> AddNewGroup(TblGroupDto group, long userId);
        
        /// <summary>
        /// Get the group by name 
        /// </summary>
        /// <param name="name">name of the group</param>
        /// <returns>TblGroup Object</returns>
        ResultMessage<TblGroupDto> GetByGroupName(string name);
    }
}
