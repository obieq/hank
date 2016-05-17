// ---------------------------------------------------------------------------------------------------
// <copyright file="ISharedTestDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The ISharedTestDataService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ISharedTestDataService interface
    /// </summary>
    public interface ISharedTestDataService : IBaseService<TblSharedTestDataDto>
    {
        /// <summary>
        /// Gets the test data by test case.
        /// </summary>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>TblSharedTestDataDto objects</returns>
        ResultMessage<IEnumerable<TblSharedTestDataDto>> GetTestDataByTestCase(long testCaseId);

        /// <summary>
        /// Resets the execution sequence.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <param name="testDataNewSeq">The test data new seq.</param>
        /// <returns>
        /// execution status
        /// </returns>
        ResultMessage<bool> ResetExecutionSequence(long userId, long testCaseId, long testDataId, long testDataNewSeq);

        /// <summary>
        /// Add List of test data
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testDataList">list of shared test data to be added</param>
        /// <returns>
        /// TblSharedTestDataDto added List
        /// </returns>
        ResultMessage<IEnumerable<TblSharedTestDataDto>> AddTestDataList(long userId, IEnumerable<TblSharedTestDataDto> testDataList);

        /// <summary>
        /// Get the Variable type test steps
        /// </summary>
        /// <param name="sharedTestCaseId">test case identifier</param>
        /// <returns>TblTestDataDto List object</returns>
        ResultMessage<IEnumerable<TblSharedTestDataDto>> GetVariableTypeSharedTestDataBySharedTestCase(long sharedTestCaseId);

        /// <summary>
        /// Saves the update custom.
        /// </summary>
        /// <param name="sharedTestDataDto">The shared test data dto.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Added/Updated TblSharedTestDataDto object
        /// </returns>
        ResultMessage<TblSharedTestDataDto> SaveUpdateCustom(TblSharedTestDataDto sharedTestDataDto, long userId);
    }
}
