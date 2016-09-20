// ---------------------------------------------------------------------------------------------------
// <copyright file="IApiConnectionService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The IApiConnectionService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ApiConnectionService interface.
    /// </summary>
    public interface IApiConnectionService : IBaseService<TblApiConnectionDto>
    {
        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>TblApiConnectionDto objects</returns>
        ResultMessage<IEnumerable<TblApiConnectionDto>> GetByCategoryId(long categoryId);

        /// <summary>
        /// Gets the by environment and category identifier.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>TblApiConnectionDto objects</returns>
        ResultMessage<TblApiConnectionDto> GetByEnvironmentAndCategoryId(long environmentId, long categoryId);

        /// <summary>
        /// Gets the by environment.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns>Return ApiConnection object that mathes recieved environmentid</returns>
        ResultMessage<IEnumerable<TblApiConnectionDto>> GetByEnvironmentId(long environmentId);
    }
}
