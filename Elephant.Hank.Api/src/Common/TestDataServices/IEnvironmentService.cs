// ---------------------------------------------------------------------------------------------------
// <copyright file="IEnvironmentService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-04</date>
// <summary>
//     The IEnvironmentService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IEnvironmentService interface
    /// </summary>
    public interface IEnvironmentService : IBaseService<TblEnvironmentDto>
    {
        /// <summary>
        /// Gets the by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>TblEnvironmentDto object</returns>
        ResultMessage<TblEnvironmentDto> GetByName(string name);

        /// <summary>
        /// Gets the default environment.
        /// </summary>
        /// <returns>return the object of default environment</returns>
        ResultMessage<TblEnvironmentDto> GetDefaultEnvironment();
    }
}
