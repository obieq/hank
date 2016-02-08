// ---------------------------------------------------------------------------------------------------
// <copyright file="IWebsiteService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The IWebsiteService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IWebsiteService interface
    /// </summary>
    public interface IWebsiteService : IBaseService<TblWebsiteDto>
    {
        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>TblWebsiteDto object</returns>
        ResultMessage<TblWebsiteDto> GetByName(string name);

        /// <summary>
        /// Get all variable related to website for auto complete help
        /// </summary>
        /// <param name="websiteId">the website identifier</param>
        /// <returns>list of variable name</returns>
        ResultMessage<IEnumerable<string>> GetAllVariableByWebsiteIdForAutoComplete(long websiteId);

        /// <summary>
        /// Get All website Authenticated to user
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="isAdminUser">if set to <c>true</c> [is admin user].</param>
        /// <returns>
        /// List of WebsiteDto
        /// </returns>
        ResultMessage<IEnumerable<TblWebsiteDto>> GetAllUserAuthenticatedWebsites(long userId, bool isAdminUser);
    }
}
