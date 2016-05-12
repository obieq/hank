// ---------------------------------------------------------------------------------------------------
// <copyright file="ApiCategoriesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The ApiCategoriesService class
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
    /// The ApiCategoriesService class
    /// </summary>
    public class ApiCategoriesService : GlobalService<TblApiCategoriesDto, TblApiCategories>, IApiCategoriesService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiCategoriesService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public ApiCategoriesService(IMapperFactory mapperFactory, IRepository<TblApiCategories> table)
            : base(mapperFactory, table)
        {
        }

        #region Implementation of IApiCategoriesService

        /// <summary>
        /// Get the categories by name
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// TblApiCategoriesDto object
        /// </returns>
        public ResultMessage<TblApiCategoriesDto> GetByName(string name)
        {
            var result = new ResultMessage<TblApiCategoriesDto>();

            var entity = this.Table.Find(x => x.Name.ToLower() == name.ToLower() && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.MapperFactory.GetMapper<TblApiCategories, TblApiCategoriesDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by website identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblApiCategoriesDto list object
        /// </returns>
        public ResultMessage<IEnumerable<TblApiCategoriesDto>> GetByWebsiteId(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblApiCategoriesDto>>();

            var entity = this.Table.Find(x => x.WebsiteId == websiteId && x.IsDeleted != true).ToList();

            var mapper = this.MapperFactory.GetMapper<TblApiCategories, TblApiCategoriesDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }

        #endregion
    }
}
