// ---------------------------------------------------------------------------------------------------
// <copyright file="IDataBaseCategoriesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The IDataBaseCategoriesService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IDataBaseCategoriesService interface
    /// </summary>
    public interface IDataBaseCategoriesService : IBaseService<TblDataBaseCategoriesDto>
    {
        /// <summary>
        /// Get the categories by name
        /// </summary>
        /// <param name="name">name of the database category</param>
        /// <returns>TblDataBaseCategoriesDto object</returns>
        ResultMessage<TblDataBaseCategoriesDto> GetByName(string name);

        /// <summary>
        /// Get all database categories by websiteid
        /// </summary>
        /// <param name="websiteId">the website identifier</param>
        /// <returns>TblDataBaseCategoriesDto list object</returns>
        ResultMessage<IEnumerable<TblDataBaseCategoriesDto>> GetByWebsiteId(long websiteId);
    }
}
