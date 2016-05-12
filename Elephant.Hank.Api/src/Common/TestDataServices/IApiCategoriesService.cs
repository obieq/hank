// ---------------------------------------------------------------------------------------------------
// <copyright file="IApiCategoriesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The IApiCategoriesService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IApiCategoriesService interface
    /// </summary>
    public interface IApiCategoriesService : IBaseService<TblApiCategoriesDto>
    {
        /// <summary>
        /// Get the categories by name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// TblApiCategoriesDto object
        /// </returns>
        ResultMessage<TblApiCategoriesDto> GetByName(string name);

        /// <summary>
        /// Gets the by website identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblApiCategoriesDto list object
        /// </returns>
        ResultMessage<IEnumerable<TblApiCategoriesDto>> GetByWebsiteId(long websiteId);
    }
}
