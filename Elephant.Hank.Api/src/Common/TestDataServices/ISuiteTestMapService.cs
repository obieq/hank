// ---------------------------------------------------------------------------------------------------
// <copyright file="ISuiteTestMapService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ISuiteTestMapService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ISuiteTestMapService interface
    /// </summary>
    public interface ISuiteTestMapService : IBaseService<TblLnkSuiteTestDto>
    {
        /// <summary>
        /// Gets the link by suite identifier.
        /// </summary>
        /// <param name="suiteId">The suite identifier.</param>
        /// <returns>TblLnkSuiteTestDto objects</returns>
        ResultMessage<IEnumerable<TblLnkSuiteTestDto>> GetLinkBySuiteId(long suiteId);

        /// <summary>
        /// Gets the link by suite identifier and test identifier.
        /// </summary>
        /// <param name="suiteId">The suite identifier.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>TblLnkSuiteTestDto object</returns>
        ResultMessage<TblLnkSuiteTestDto> GetLinkBySuiteIdAndTestId(long suiteId, long testCaseId);

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="suiteId">The suite identifier.</param>
        /// <param name="sourceData">The source data.</param>
        /// <returns>
        /// TblLnkSuiteTestDto objects
        /// </returns>
        ResultMessage<IEnumerable<TblLnkSuiteTestDto>> SaveOrUpdate(long userId, long suiteId, List<TblLnkSuiteTestDto> sourceData);

        /// <summary>
        /// Gets the link by suite identifier list.
        /// </summary>
        /// <param name="suiteIdList">The suite identifier.</param>
        /// <returns>TblLnkSuiteTestDto objects</returns>
        ResultMessage<IEnumerable<TblLnkSuiteTestDto>> GetLinksBySuiteIdList(string suiteIdList);
    }
}
