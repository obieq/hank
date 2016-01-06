// ---------------------------------------------------------------------------------------------------
// <copyright file="ITestDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ITestDataService interface
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
    /// The ITestDataService interface
    /// </summary>
    public interface ITestDataService : IBaseService<TblTestDataDto>
    {
        /// <summary>
        /// Gets the test data by test case.
        /// </summary>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>TblTestDataDto objects</returns>
        ResultMessage<IEnumerable<TblTestDataDto>> GetTestDataByTestCase(long testCaseId);

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
        /// <param name="testDataList">list of test data to be added</param>
        /// <returns>
        /// TblTestDataDto added List
        /// </returns>
        ResultMessage<IEnumerable<TblTestDataDto>> AddTestDataList(long userId, IEnumerable<TblTestDataDto> testDataList);

        /// <summary>
        /// Save Testdata with shared steps
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testData">TblTestDataDto object</param>
        /// <returns>
        /// List object TblTestDataDto
        /// </returns>
        ResultMessage<TblTestDataDto> SaveOrUpdateWithSharedTest(long userId, TblTestDataDto testData);

        /// <summary>
        /// Copy the test steps from one test to another
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="copyTestDataModel">copyTestDataModel Object</param>
        /// <returns>
        /// TblTestDataDto List object
        /// </returns>
        ResultMessage<IEnumerable<TblTestDataDto>> CopyTestData(long userId, CopyTestDataModel copyTestDataModel);

        /// <summary>
        /// Get the Variable type test steps
        /// </summary>
        /// <param name="testCaseId">test case identifier</param>
        /// <returns>TblTestDataDto List object</returns>
        ResultMessage<IEnumerable<ProtractorVariableModel>> GetVariableTypeTestDataByTestCase(long testCaseId);
       
        /// <summary>
        /// Get all variable related to test for auto complete help
        /// </summary>
        /// <param name="testId">the test identifier</param>
        /// <returns>list of variable name</returns>
        ResultMessage<IEnumerable<string>> GetAllVariableByTestIdForAutoComplete(long testId);
    }
}
