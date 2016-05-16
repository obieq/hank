// ---------------------------------------------------------------------------------------------------
// <copyright file="RequestTypesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-05-16</date>
// <summary>
//     The RequestTypesService class
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
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Elephant.Hank.Resources.ViewModal;

    /// <summary>
    /// The RequestTypesService class
    /// </summary>
    public class RequestTypesService : GlobalService<TblRequestTypesDto, TblRequestTypes>, IRequestTypesService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestTypesService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public RequestTypesService(IMapperFactory mapperFactory, IRepository<TblRequestTypes> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }
    }
}
