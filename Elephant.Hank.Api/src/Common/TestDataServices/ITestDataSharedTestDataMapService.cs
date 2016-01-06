// ---------------------------------------------------------------------------------------------------
// <copyright file="ITestDataSharedTestDataMapService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-09</date>
// <summary>
//     The ITestDataSharedTestDataMapService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// the ITestDataSharedTestDataMapService interface
    /// </summary>
    public interface ITestDataSharedTestDataMapService : IBaseService<TblLnkTestDataSharedTestDataDto>
    {
        /// <summary>
        /// get all shared test step by TestDataId
        /// </summary>
        /// <param name="testDataId">the testData identifier</param>
        /// <returns>TblLnkTestDataSharedTestDataDto object</returns>
        ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>> GetByTestDataId(long testDataId);

        /// <summary>
        /// Save Or Update the TestDataSharedTestData steps
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testDataId">the testData identifier</param>
        /// <param name="sourceData">List of TblLnkTestDataSharedTestDataDto</param>
        /// <returns>
        /// TblLnkTestDataSharedTestDataDto object list
        /// </returns>
        ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>> SaveOrUpdate(long userId, long testDataId, List<TblLnkTestDataSharedTestDataDto> sourceData);

        /// <summary>
        /// delete all entries in table by sharedTestDataId
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="sharedTestDataId">shared test identifier</param>
        /// <returns>
        /// List of all deleted TblLnkTestDataSharedTestDataDto
        /// </returns>
        ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>> DeleteBySharedTestDataId(long userId, long sharedTestDataId);
    }
}
