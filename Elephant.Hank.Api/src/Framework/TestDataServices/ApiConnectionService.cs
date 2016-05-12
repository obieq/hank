// ---------------------------------------------------------------------------------------------------
// <copyright file="ApiConnectionService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-05-12</date>
// <summary>
//     The ApiConnectionService class
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
    /// The api connection service.
    /// </summary>
    public class ApiConnectionService : GlobalService<TblApiConnectionDto, TblApiConnection>, IApiConnectionService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnectionService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public ApiConnectionService(IMapperFactory mapperFactory, IRepository<TblApiConnection> table)
            : base(mapperFactory, table)
        {
        }

        #region Implementation of IApiConnectionService

        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>TblApiConnectionDto objects</returns>
        public ResultMessage<IEnumerable<TblApiConnectionDto>> GetByCategoryId(long categoryId)
        {
            var result = new ResultMessage<IEnumerable<TblApiConnectionDto>>();

            var entities = this.Table.Find(x => x.CategoryId == categoryId && x.IsDeleted != true).ToList();

            var mapper = this.MapperFactory.GetMapper<TblApiConnection, TblApiConnectionDto>();
            result.Item = entities.Select(mapper.Map);

            return result;
        }

        /// <summary>
        /// Gets the by environment and category identifier.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>TblApiConnectionDto objects</returns>
        public ResultMessage<TblApiConnectionDto> GetByEnvironmentAndCategoryId(long environmentId, long categoryId)
        {
            var result = new ResultMessage<TblApiConnectionDto>();

            var entity = this.Table.Find(x => x.EnvironmentId == environmentId && x.CategoryId == categoryId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.MapperFactory.GetMapper<TblApiConnection, TblApiConnectionDto>().Map(entity);
            }

            return result;
        }

        #endregion
    }
}
