// ---------------------------------------------------------------------------------------------------
// <copyright file="BrowserService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-09</date>
// <summary>
//     The BrowserService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;

    /// <summary>
    /// The BrowserService class
    /// </summary>
    public class BrowserService : GlobalService<TblBrowsersDto, TblBrowsers>, IBrowserService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public BrowserService(IMapperFactory mapperFactory, IRepository<TblBrowsers> table)
            : base(mapperFactory, table)
        {
        }
    }
}
