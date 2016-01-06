// ---------------------------------------------------------------------------------------------------
// <copyright file="SharedTestService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The SharedTestService class
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
    /// The SharedTestService class
    /// </summary>
    public class SharedTestService : GlobalService<TblSharedTestDto, TblSharedTest>, ISharedTestService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedTestService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public SharedTestService(IMapperFactory mapperFactory, IRepository<TblSharedTest> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>TblSharedTestDataDto object</returns>
        public ResultMessage<TblSharedTestDto> GetByName(string name, long webSiteId)
        {
            var result = new ResultMessage<TblSharedTestDto>();

            name = (name + string.Empty).ToLower();

            var entity = this.Table.Find(x => (x.TestName + string.Empty).ToLower() == name && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblSharedTest, TblSharedTestDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>
        /// TblSharedTestDto objects
        /// </returns>
        public ResultMessage<IEnumerable<TblSharedTestDto>> GetByWebSiteId(long webSiteId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDto>>();

            var entity = this.Table.Find(x => x.WebsiteId == webSiteId && x.IsDeleted != true).ToList();

            var mapper = this.mapperFactory.GetMapper<TblSharedTest, TblSharedTestDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }
    }
}
