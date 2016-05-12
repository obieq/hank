// ---------------------------------------------------------------------------------------------------
// <copyright file="HashTagDescriptionService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-12</date>
// <summary>
//     The HashTagDescriptionService class
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
    using Elephant.Hank.Resources.Json;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The HashTagDescriptionService class.
    /// </summary>
    public class HashTagDescriptionService : GlobalService<TblHashTagDescriptionDto, TblHashTagDescription>, IHashTagDescriptionService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashTagDescriptionService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public HashTagDescriptionService(IMapperFactory mapperFactory, IRepository<TblHashTagDescription> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }
    }
}
