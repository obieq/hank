// ---------------------------------------------------------------------------------------------------
// <copyright file="TestCategoryService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-01</date>
// <summary>
//     The TestCategoryService class
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
    /// The TestCategoryService class
    /// </summary>
    public class TestCategoryService : GlobalService<TblTestCategoriesDto, TblTestCategories>, ITestCategoryService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCategoryService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TestCategoryService(IMapperFactory mapperFactory, IRepository<TblTestCategories> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        #region Implementation of ITestCategoryService

        /// <summary>
        /// Gets the by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblTestCategoriesDto object
        /// </returns>
        public ResultMessage<TblTestCategoriesDto> GetByName(string name, long websiteId)
        {
            var result = new ResultMessage<TblTestCategoriesDto>();

            name = (name + string.Empty).ToLower();

            var entity = this.Table.Find(x => (x.Name + string.Empty).ToLower() == name && x.WebsiteId == websiteId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblTestCategories, TblTestCategoriesDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblTestCategoriesDto object
        /// </returns>
        public ResultMessage<IEnumerable<TblTestCategoriesDto>> GetByWebSiteId(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblTestCategoriesDto>>();

            var entities = this.Table.Find(x => x.WebsiteId == websiteId && x.IsDeleted != true).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblTestCategories, TblTestCategoriesDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.Id);
            }

            return result;
        }

        #endregion
    }
}
