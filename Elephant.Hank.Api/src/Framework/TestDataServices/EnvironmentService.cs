// ---------------------------------------------------------------------------------------------------
// <copyright file="EnvironmentService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-04</date>
// <summary>
//     The EnvironmentService class
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
    /// The EnvironmentService class
    /// </summary>
    public class EnvironmentService : GlobalService<TblEnvironmentDto, TblEnvironment>, IEnvironmentService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public EnvironmentService(IMapperFactory mapperFactory, IRepository<TblEnvironment> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>TblEnvironmentDto object</returns>
        public ResultMessage<TblEnvironmentDto> GetByName(string name)
        {
            var result = new ResultMessage<TblEnvironmentDto>();

            var entity = this.Table.Find(x => x.Name == name && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblEnvironment, TblEnvironmentDto>().Map(entity);
            }

            return result;
        }
    }
}
