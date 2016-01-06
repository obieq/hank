// ---------------------------------------------------------------------------------------------------
// <copyright file="DataBaseCategoriesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The DataBaseCategoriesService class
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
    /// The  DataBaseCategoriesService class
    /// </summary>
    public class DataBaseCategoriesService : GlobalService<TblDataBaseCategoriesDto, TblDataBaseCategories>, IDataBaseCategoriesService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseCategoriesService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public DataBaseCategoriesService(IMapperFactory mapperFactory, IRepository<TblDataBaseCategories> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>TblDataBaseCategoriesDto object</returns>
        public ResultMessage<TblDataBaseCategoriesDto> GetByName(string name)
        {
            var result = new ResultMessage<TblDataBaseCategoriesDto>();

            var entity = this.Table.Find(x => x.Name.ToLower() == name.ToLower() && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblDataBaseCategories, TblDataBaseCategoriesDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Get all database categories by websiteid
        /// </summary>
        /// <param name="websiteId">the website identifier</param>
        /// <returns>TblDataBaseCategoriesDto object</returns>
        public ResultMessage<IEnumerable<TblDataBaseCategoriesDto>> GetByWebsiteId(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseCategoriesDto>>();

            var entity = this.Table.Find(x => x.WebsiteId == websiteId && x.IsDeleted != true).ToList();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblDataBaseCategories, TblDataBaseCategoriesDto>();
                result.Item = entity.Select(mapper.Map);
            }

            return result;
        }
    }
}
