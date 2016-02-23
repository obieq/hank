// ---------------------------------------------------------------------------------------------------
// <copyright file="ITestQueueExecutableService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-25</date>
// <summary>
//     The ITestQueueExecutableService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Elephant.Hank.Resources.ViewModal;

    /// <summary>
    /// The ITestQueueExecutableService interface
    /// </summary>
    public interface ITestQueueExecutableService
    {
        /// <summary>
        /// Gets the Test Queue executable data by test queue identifier.
        /// </summary>
        /// <param name="testQueueId">test queue identifier</param>
        /// <returns>GetTestQueueExecutableData object</returns>
        ResultMessage<TestQueue_FullTestData> GetTestQueueExecutableData(long testQueueId);

        /// <summary>
        /// Updates the automatic increment.
        /// </summary>
        /// <param name="executableTestData">The executable test data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>incremented value</returns>
        ResultMessage<string> UpdateAutoIncrement(ExecutableTestData executableTestData, long userId);
    }
}
