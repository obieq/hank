// ---------------------------------------------------------------------------------------------------
// <copyright file="IDbLogService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-18</date>
// <summary>
//     The IDbLogService interface
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
    /// The IDbLogService interface
    /// </summary>
    public interface IDbLogService : IBaseService<TblDbLogDto>
    {
        /// <summary>
        /// Roll back/forward the data entry
        /// </summary>
        /// <param name="dbLogId">the logger id</param>
        /// <param name="toOldValue">rolling identifier</param>
        /// <returns>return procedure execution status</returns>
        int RollData(long dbLogId, bool toOldValue);

        /// <summary>
        /// Get Log data with in the range
        /// </summary>
        /// <param name="start">start date time</param>
        /// <param name="end">end date time</param>
        /// <returns>List of TBlLogDto Object with in the provided range</returns>
        ResultMessage<IEnumerable<TblDbLogDto>> GetDataWithInDateTimeRange(DateTime start, DateTime end);

        /// <summary>
        /// Get Data in chunks of 50
        /// </summary>
        /// <param name="chunk">last chunk identifier</param>
        /// <param name="tableType">Table type filter</param>
        /// <param name="model">date range filter</param>
        /// <returns>TblDbLog List filtered object</returns>
        ResultMessage<IEnumerable<TblDbLogDto>> GetChunk(long chunk, string tableType, SearchLogModel model);
    }
}
