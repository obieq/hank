// ---------------------------------------------------------------------------------------------------
// <copyright file="ISuiteService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ISuiteService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ISuiteService interface
    /// </summary>
    public interface ISuiteService : IBaseService<TblSuiteDto>
    {
        /// <summary>
        /// Gets the by website identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblSuiteDto object
        /// </returns>
        ResultMessage<IEnumerable<TblSuiteDto>> GetByWebsiteId(long websiteId);
    }
}
