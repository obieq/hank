// ---------------------------------------------------------------------------------------------------
// <copyright file="IReportDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-30</date>
// <summary>
//     The IReportDataService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System;
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;   

    /// <summary>
    /// The IReportDataService interface
    /// </summary>
    public interface IReportDataService : IBaseService<TblReportDataDto>
    {
        /// <summary>
        /// Get the search report data
        /// </summary>
        /// <param name="searchReportObject">the searchReportObject object</param>
        /// <returns>the ReportData object</returns>
        ResultMessage<IEnumerable<TblReportDataDto>> GetReportData(SearchReportObject searchReportObject);

        /// <summary>
        /// Get the ReportData By Id
        /// </summary>
        /// <param name="id">report identifier</param>
        /// <returns>TblReportDataDto object</returns>
        ResultMessage<TblReportDataDto> GetReportDataById(long id);

        /// <summary>
        /// Get Report Data by Group Name
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>TblReportDataDto objects</returns>
        ResultMessage<IEnumerable<TblReportDataDto>> GetByGroupName(string groupName);

        /// <summary>
        /// Get Report Data by Group Name where screen shot array exist
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>TblReportDataDto objects</returns>
        ResultMessage<TblReportDataDto> GetByGroupNameWhereScreenShotArrayExist(string groupName);

        /// <summary>
        /// get all unprocessed data item for group
        /// </summary>
        /// <param name="groupName">group identifier</param>
        /// <returns>list of report data</returns>
        ResultMessage<IEnumerable<TblReportDataDto>> GetAllUnprocessedForGroup(string groupName);
    }
}
