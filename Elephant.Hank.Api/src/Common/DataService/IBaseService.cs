// ---------------------------------------------------------------------------------------------------
// <copyright file="IBaseService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The IBaseService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.DataService
{
    using System.Collections.Generic;

    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IBaseService class
    /// </summary>
    /// <typeparam name="Tin">The type of the in.</typeparam>
    public interface IBaseService<Tin>
    {
        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        ResultMessage<Tin> DeleteById(long id, long userId);

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        ResultMessage<Tin> SaveOrUpdate(Tin sourceData, long userId);

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="sourceDataList">The source data list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin objects
        /// </returns>
        ResultMessage<IEnumerable<Tin>> SaveOrUpdate(IEnumerable<Tin> sourceDataList, long userId);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>Tin object</returns>
        ResultMessage<Tin> GetById(long entityId);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Tin list of full data</returns>
        ResultMessage<IEnumerable<Tin>> GetAll();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>custom object</returns>
        ResultMessage<IEnumerable<T>> GetAllCustom<T>() where T : BaseTableDto;
    }
}
