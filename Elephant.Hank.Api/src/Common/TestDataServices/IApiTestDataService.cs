// ---------------------------------------------------------------------------------------------------
// <copyright file="IApiTestDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-13</date>
// <summary>
//     The IApiTestDataService Interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;

    /// <summary>
    /// The  IApiTestDataService interface
    /// </summary>
    public interface IApiTestDataService : IBaseService<TblApiTestDataDto>
    {
    }
}
