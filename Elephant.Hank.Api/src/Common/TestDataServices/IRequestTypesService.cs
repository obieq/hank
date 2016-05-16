// ---------------------------------------------------------------------------------------------------
// <copyright file="IRequestTypesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-05-16</date>
// <summary>
//     The IRequestTypesService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The IRequestTypesService interface
    /// </summary>
    public interface IRequestTypesService : IBaseService<TblRequestTypesDto>
    {
    }
}
