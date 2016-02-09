// ---------------------------------------------------------------------------------------------------
// <copyright file="ITestService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ITestService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ITestService interface
    /// </summary>
    public interface ITestService : IBaseService<TblTestDto>
    {
        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>TblTestDataDto object</returns>
        ResultMessage<TblTestDto> GetByName(string name, long webSiteId);

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>
        /// TblTestDto objects
        /// </returns>
        ResultMessage<IEnumerable<TblTestDto>> GetByWebSiteId(long webSiteId, long userId);

        /// <summary>
        /// Gets the by category.
        /// </summary>
        /// <param name="testCatId">The test cat identifier.</param>
        /// <returns>TblTestDto objects</returns>
        ResultMessage<IEnumerable<TblTestDto>> GetByCategory(long testCatId, long userId);

        /// <summary>
        /// Gets by id.
        /// </summary>
        /// <param name="testCatId">The test cat identifier.</param>
        /// <returns>TblTestDto objects</returns>
        ResultMessage<TblTestDto> GetById(long id, long userId);
    }
}
