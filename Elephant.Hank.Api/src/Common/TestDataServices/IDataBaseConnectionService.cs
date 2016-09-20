// ---------------------------------------------------------------------------------------------------
// <copyright file="IDataBaseConnectionService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The IDataBaseConnectionService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.InternalDtos;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IDataBaseConnectionService interface
    /// </summary>
    public interface IDataBaseConnectionService : IBaseService<TblDataBaseConnectionDto>
    {
        /// <summary>
        ///  Get the Sensitive data from data base return the database username password
        /// </summary>
        /// <param name="environmentId">environment Identifier</param>
        /// <param name="categoryId">database category identifier</param>
        /// <returns>TblDataBaseConnectionDto object</returns>
        ResultMessage<InternalTblDataBaseConnectionDto> GetSensitiveDataByEnvironmentAndCategoryId(long environmentId, long categoryId);

        /// <summary>
        /// Get the list of all data bases
        /// </summary>
        /// <param name="dataBaseConnectionDto">dataBaseConnectionDto object</param>
        /// <returns>string list with all database name</returns>
        ResultMessage<List<string>> GetAllDataBaseList(TblDataBaseConnectionDto dataBaseConnectionDto);

        /// <summary>
        /// Get the DataBaseConnection
        /// </summary>
        /// <param name="categoryId">database category identifier</param>
        /// <returns>TblDataBaseConnectionDto object</returns>
        ResultMessage<IEnumerable<TblDataBaseConnectionDto>> GetByCategoryId(long categoryId);

        /// <summary>
        /// Gets the by environment identifier.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns>List of DataBase connection by environment id</returns>
        ResultMessage<IEnumerable<TblDataBaseConnectionDto>> GetByEnvironmentId(long environmentId);
    }
}
