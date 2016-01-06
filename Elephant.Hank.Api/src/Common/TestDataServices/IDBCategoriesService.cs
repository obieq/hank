// ---------------------------------------------------------------------------------------------------
// <copyright file="IDBCategoriesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-11</date>
// <summary>
//     The IDBCategoriesService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IDBCategoriesService Interface
    /// </summary>
    public interface IDBCategoriesService : IBaseService<TblDBCategoriesDto>
    {
        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>
        /// TblDBCategoriesDto objects
        /// </returns>
        ResultMessage<IEnumerable<TblDBCategoriesDto>> GetByWebSiteId(long webSiteId);

        /// <summary>
        /// Get the list of data base
        /// </summary>
        /// <param name="dBCategoriesDto">data base identifier details</param>
        /// <returns>List of all data base</returns>
        ResultMessage<List<string>> GetDatabaseList(TblDBCategoriesDto dBCategoriesDto);
    }
}
