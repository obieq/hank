// ---------------------------------------------------------------------------------------------------
// <copyright file="ITestCategoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-01</date>
// <summary>
//     The ITestCategoryService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ITestCategoryService interface
    /// </summary>
    public interface ITestCategoryService : IBaseService<TblTestCategoriesDto>
    {
        /// <summary>
        /// Gets the by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblTestCategoriesDto object
        /// </returns>
        ResultMessage<TblTestCategoriesDto> GetByName(string name, long websiteId);

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblTestCategoriesDto object
        /// </returns>
        ResultMessage<IEnumerable<TblTestCategoriesDto>> GetByWebSiteId(long websiteId);
    }
}
