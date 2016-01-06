// ---------------------------------------------------------------------------------------------------
// <copyright file="ITestQueueService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-06</date>
// <summary>
//     The ITestQueueService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ITestQueueService Interface
    /// </summary>
    public interface ITestQueueService : IBaseService<TblTestQueueDto>
    {
        /// <summary>
        /// Get All unprocessed tests 
        /// </summary>
        /// <returns>List TblTestQueueDto</returns>
        ResultMessage<IEnumerable<TblTestQueueDto>> GetAllUnProcessed();
    }
}
