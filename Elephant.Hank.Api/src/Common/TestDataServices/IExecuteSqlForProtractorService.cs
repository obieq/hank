// ---------------------------------------------------------------------------------------------------
// <copyright file="IExecuteSqlForProtractorService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-012-17</date>
// <summary>
//     The IExecuteSqlForProtractorService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// the IExecuteSqlForProtractorService interface
    /// </summary>
    public interface IExecuteSqlForProtractorService 
    {
        /// <summary>
        /// Execute the Sql Querry requested buy protractor
        /// </summary>
        /// <param name="executableTestDataStep">the ExecutableTestData object</param>
        /// <param name="testQueueId">the testqueue identifier</param>
        /// <returns>result of querry in string</returns>
        ResultMessage<object> ExecuteQuery(ExecutableTestData executableTestDataStep, long testQueueId);
    }
}
