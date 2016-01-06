// ---------------------------------------------------------------------------------------------------
// <copyright file="LocatorIdentifierService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The LocatorIdentifierService class
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
    /// The LocatorIdentifierService class
    /// </summary>
    public class LocatorIdentifierService : GlobalService<TblLocatorIdentifierDto, TblLocatorIdentifier>, ILocatorIdentifierService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatorIdentifierService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public LocatorIdentifierService(IMapperFactory mapperFactory, IRepository<TblLocatorIdentifier> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Determines whether the specified locator identifier dto is existing.
        /// </summary>
        /// <param name="locatorIdentifierDto">The locator identifier dto.</param>
        /// <returns>
        /// TblLocatorIdentifierDto object
        /// </returns>
        public ResultMessage<TblLocatorIdentifierDto> IsExisting(TblLocatorIdentifierDto locatorIdentifierDto)
        {
            var result = new ResultMessage<TblLocatorIdentifierDto>();

            locatorIdentifierDto.DisplayName = locatorIdentifierDto.DisplayName + string.Empty;

            var entity = this.Table.Find(x =>
                x.PageId == locatorIdentifierDto.PageId
                && (x.DisplayName + string.Empty).ToLower() == locatorIdentifierDto.DisplayName.ToLower()
                && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblLocatorIdentifier, TblLocatorIdentifierDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Get By Web site Id
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// List of TblLocatorIdentifierDto
        /// </returns>
        public ResultMessage<IEnumerable<TblLocatorIdentifierDto>> GetByPageId(long pageId)
        {
            var result = new ResultMessage<IEnumerable<TblLocatorIdentifierDto>>();
            var entity = this.Table.Find(x => x.PageId == pageId && x.IsDeleted != true).ToList();
            var mapper = this.mapperFactory.GetMapper<TblLocatorIdentifier, TblLocatorIdentifierDto>();
            result.Item = entity.Select(mapper.Map);
            return result;
        }
    }
}