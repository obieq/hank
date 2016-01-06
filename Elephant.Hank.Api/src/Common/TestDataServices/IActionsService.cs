// ---------------------------------------------------------------------------------------------------
// <copyright file="IActionsService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The IActionsService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IActionsService interface
    /// </summary>
    public interface IActionsService : IBaseService<TblActionDto>
    {
        /// <summary>
        /// Gets the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>TblActionDto object</returns>
        ResultMessage<TblActionDto> GetByValue(string value);

        /// <summary>
        /// Get allowed actions for sql steps
        /// </summary>
        /// <returns>TblActionDto list</returns>
        ResultMessage<IEnumerable<TblActionDto>> GetActionForSqlTestStep();
    }
}