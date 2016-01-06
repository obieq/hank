// ---------------------------------------------------------------------------------------------------
// <copyright file="ILocatorService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ILocatorService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The ILocatorService interface
    /// </summary>
    public interface ILocatorService : IBaseService<TblLocatorDto>
    {
        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// TblLocatorDto object
        /// </returns>
        ResultMessage<TblLocatorDto> GetByValue(string value);
    }
}
