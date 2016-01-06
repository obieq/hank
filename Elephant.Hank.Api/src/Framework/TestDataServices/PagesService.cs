// ---------------------------------------------------------------------------------------------------
// <copyright file="PagesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-04</date>
// <summary>
//     The PagesService class
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
    /// The PagesService class
    /// </summary>
   public class PagesService : GlobalService<TblPagesDto, TblPages>, IPagesService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public PagesService(IMapperFactory mapperFactory, IRepository<TblPages> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblDisplayNameDto object
        /// </returns>
        public ResultMessage<TblPagesDto> GetByValue(string value, long websiteId)
        {
            var result = new ResultMessage<TblPagesDto>();

            value = (value + string.Empty).ToLower();

            var entity = this.Table.Find(x => (x.Value + string.Empty).ToLower() == value && x.WebsiteId == websiteId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblPages, TblPagesDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="websiteId">The website identifier.</param>
        /// <returns>
        /// TblDisplayNameDto object
        /// </returns>
        public ResultMessage<IEnumerable<TblPagesDto>> GetByWebSiteId(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<TblPagesDto>>();

            var entities = this.Table.Find(x => x.WebsiteId == websiteId && x.IsDeleted != true).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblPages, TblPagesDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.Id);
            }

            return result;
        }
    }
}