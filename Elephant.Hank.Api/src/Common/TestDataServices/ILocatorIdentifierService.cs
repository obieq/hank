// ---------------------------------------------------------------------------------------------------
// <copyright file="ILocatorIdentifierService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ILocatorIdentifierService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ILocatorIdentifierService interface
    /// </summary>
    public interface ILocatorIdentifierService : IBaseService<TblLocatorIdentifierDto>
    {
        /// <summary>
        /// Determines whether the specified locator identifier dto is existing.
        /// </summary>
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <returns>
        /// TblLocatorIdentifierDto object
        /// </returns>
        ResultMessage<TblLocatorIdentifierDto> IsExisting(TblLocatorIdentifierDto locatorIdentifierDto);

        /// <summary>
        /// Get By Website Id
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// List TblLocatorIdentifierDto
        /// </returns>
        ResultMessage<IEnumerable<TblLocatorIdentifierDto>> GetByPageId(long pageId);
    }
}
