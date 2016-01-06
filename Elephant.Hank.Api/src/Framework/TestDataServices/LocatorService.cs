// ---------------------------------------------------------------------------------------------------
// <copyright file="LocatorService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The LocatorService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The LocatorService class
    /// </summary>
    public class LocatorService : GlobalService<TblLocatorDto, TblLocator>, ILocatorService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatorService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public LocatorService(IMapperFactory mapperFactory, IRepository<TblLocator> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// TblLocatorDto object
        /// </returns>
        public ResultMessage<TblLocatorDto> GetByValue(string value)
        {
            var result = new ResultMessage<TblLocatorDto>();

            value = (value + string.Empty).ToLower();

            var entity = this.Table.Find(x => (x.Value + string.Empty).ToLower() == value && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblLocator, TblLocatorDto>().Map(entity);
            }

            return result;
        }
    }
}