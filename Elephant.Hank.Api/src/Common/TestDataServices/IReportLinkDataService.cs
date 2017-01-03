// ---------------------------------------------------------------------------------------------------
// <copyright file="IReportLinkDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-12-23</date>
// <summary>
//     The IReportLinkDataService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System;
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// the IReportLinkDataService
    /// </summary>
    public interface IReportLinkDataService : IBaseService<TblReportExecutionLinkDataDto>
    {
        /// <summary>
        /// Adds the specified report link data dto.
        /// </summary>
        /// <param name="reportLinkDataDto">The report link data dto.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Added object</returns>
        ResultMessage<TblReportExecutionLinkDataDto> AddOrUpdate(TblReportExecutionLinkDataDto reportLinkDataDto, long userId);

        /// <summary>
        /// Gets the report link data by test identifier.
        /// </summary>
        /// <param name="testDataId">The test data identifier.</param>
        /// <param name="isSharedTestData">if set to <c>true</c> [is shared test data].</param>
        /// <returns>
        /// TblReportExecutionLinkDataDto list
        /// </returns>
        ResultMessage<TblReportExecutionLinkDataDto> GetReportLinkDataByTestDataId(long testDataId, bool isSharedTestData);
    }
}
