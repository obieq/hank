// ---------------------------------------------------------------------------------------------------
// <copyright file="TestService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TestService class
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
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The TestService class
    /// </summary>
    public class TestService : GlobalService<TblTestDto, TblTest>, ITestService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TestService(IMapperFactory mapperFactory, IRepository<TblTest> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>TblTestDataDto object</returns>
        public ResultMessage<TblTestDto> GetByName(string name, long webSiteId)
        {
            var result = new ResultMessage<TblTestDto>();

            name = (name + string.Empty).ToLower();

            var entity = this.Table.Find(x => (x.TestName + string.Empty).ToLower() == name && x.IsDeleted != true && x.WebsiteId == webSiteId).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblTest, TblTestDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// TblTestDto objects
        /// </returns>
        public ResultMessage<IEnumerable<TblTestDto>> GetByWebSiteId(long webSiteId, long userId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDto>>();

            var entity = this.Table.Find(x => x.WebsiteId == webSiteId && x.IsDeleted != true && (x.TestCaseAccessStatus != (int)TestCaseAccessStatus.Private || x.CreatedBy == userId)).ToList();

            var mapper = this.mapperFactory.GetMapper<TblTest, TblTestDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }

        /// <summary>
        /// Gets the by category.
        /// </summary>
        /// <param name="testCatId">The test cat identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// TblTestDto objects
        /// </returns>
        public ResultMessage<IEnumerable<TblTestDto>> GetByCategory(long testCatId, long userId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDto>>();

            var entity = this.Table.Find(x => x.CategoryId == testCatId && x.IsDeleted != true && (x.TestCaseAccessStatus != (int)TestCaseAccessStatus.Private || x.CreatedBy == userId)).ToList();

            var mapper = this.mapperFactory.GetMapper<TblTest, TblTestDto>();
            result.Item = entity.Select(mapper.Map);

            return result;
        }

        /// <summary>
        /// Gets by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// TblTestDto objects
        /// </returns>
        public ResultMessage<TblTestDto> GetById(long id, long userId)
        {
            var result = new ResultMessage<TblTestDto>();

            var entity = this.Table.Find(x => x.Id == id && x.IsDeleted != true && (x.TestCaseAccessStatus != (int)TestCaseAccessStatus.Private || x.CreatedBy == userId)).FirstOrDefault();

            result.Item = this.mapperFactory.GetMapper<TblTest, TblTestDto>().Map(entity);

            return result;
        }
    }
}