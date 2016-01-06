// ---------------------------------------------------------------------------------------------------
// <copyright file="IPagesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-04</date>
// <summary>
//     The IPagesService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IPagesService interface
    /// </summary>
    public interface IPagesService : IBaseService<TblPagesDto>
    {
        /// <summary>
        /// Gets the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblPagesDto object
        /// </returns>
        ResultMessage<TblPagesDto> GetByValue(string value, long websiteId);

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblPagesDto object
        /// </returns>
        ResultMessage<IEnumerable<TblPagesDto>> GetByWebSiteId(long websiteId);
    }
}
