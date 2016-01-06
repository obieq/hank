// ---------------------------------------------------------------------------------------------------
// <copyright file="SuiteService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The SuiteService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SuiteService class
    /// </summary>
    public class SuiteService : GlobalService<TblSuiteDto, TblSuite>, ISuiteService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuiteService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public SuiteService(IMapperFactory mapperFactory, IRepository<TblSuite> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the by website identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblSuiteDto object
        /// </returns>
        public ResultMessage<IEnumerable<TblSuiteDto>> GetByWebsiteId(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblSuiteDto>>();

            var entity = this.Table.Find(x => x.WebsiteId == websiteId && x.IsDeleted != true).ToList();

            var mapper = this.mapperFactory.GetMapper<TblSuite, TblSuiteDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }
    }
}