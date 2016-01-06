// ---------------------------------------------------------------------------------------------------
// <copyright file="ActionsService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The ActionsService class
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
    /// The ActionsService class
    /// </summary>
    public class ActionsService : GlobalService<TblActionDto, TblAction>, IActionsService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionsService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public ActionsService(IMapperFactory mapperFactory, IRepository<TblAction> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>TblActionDto object</returns>
        public ResultMessage<TblActionDto> GetByValue(string value)
        {
            var result = new ResultMessage<TblActionDto>();

            value = (value + string.Empty).ToLower();

            var entity = this.Table.Find(x => (x.Value + string.Empty).ToLower() == value && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblAction, TblActionDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Get allowed actions for sql steps
        /// </summary>
        /// <returns>TblActionDto list</returns>
        public ResultMessage<IEnumerable<TblActionDto>> GetActionForSqlTestStep()
        {
            var result = new ResultMessage<IEnumerable<TblActionDto>>();
            ActionConstants actionConstants = new ActionConstants();
            var entity = this.Table.Find(x => (x.Id == actionConstants.LogTextActionId || x.Id == actionConstants.SetVariableActionId) && x.IsDeleted != true).ToList();
            var mapper = this.mapperFactory.GetMapper<TblAction, TblActionDto>();
            result.Item = entity.Select(mapper.Map).ToList();
            return result;
        }
    }
}
