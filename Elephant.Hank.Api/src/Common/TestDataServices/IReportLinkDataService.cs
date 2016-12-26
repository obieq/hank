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
        /// Gets the report link data.
        /// </summary>
        /// <param name="dayTillPastByDateCbx">if set to <c>true</c> [day till past by date CBX].</param>
        /// <param name="dayTillPast">The day till past.</param>
        /// <param name="testId">The test identifier.</param>
        /// <param name="dayTillPastDate">The day till past date.</param>
        /// <returns>
        /// Unused report data
        /// </returns>
        ResultMessage<IEnumerable<TblReportExecutionLinkDataDto>> GetReportLinkData(bool dayTillPastByDateCbx, long dayTillPast, long testId, DateTime dayTillPastDate);

        /// <summary>
        /// Adds the specified report link data dto.
        /// </summary>
        /// <param name="reportLinkDataDto">The report link data dto.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Added object</returns>
        ResultMessage<TblReportExecutionLinkDataDto> Add(TblReportExecutionLinkDataDto reportLinkDataDto, long userId);
    }
}
