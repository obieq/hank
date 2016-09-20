// ---------------------------------------------------------------------------------------------------
// <copyright file="IValidationService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-09-06</date>
// <summary>
//     The IValidationService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.CustomValidationService
{
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IValidationService Interface
    /// </summary>
    /// <typeparam name="T">Type of class on which validation to be checked</typeparam>
    public interface IValidationService<T>
    {
        /// <summary>
        /// Validates the delete.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Returnd the validation result with messages</returns>
        ResultMessage<bool> ValidateDelete(T model);
    }
}
