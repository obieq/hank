// ---------------------------------------------------------------------------------------------------
// <copyright file="ISharedTestService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The ISharedTestService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    
    /// <summary>
    /// The ISharedTestService interface
    /// </summary>
    public interface ISharedTestService : IBaseService<TblSharedTestDto>
    {
        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>TblTestDataDto object</returns>
        ResultMessage<TblSharedTestDto> GetByName(string name, long webSiteId);

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>
        /// TblTestDto objects
        /// </returns>
        ResultMessage<IEnumerable<TblSharedTestDto>> GetByWebSiteId(long webSiteId);
    }
}
