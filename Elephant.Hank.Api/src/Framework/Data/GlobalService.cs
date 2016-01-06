// ---------------------------------------------------------------------------------------------------
// <copyright file="GlobalService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The GlobalService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The GlobalRepository class
    /// </summary>
    /// <typeparam name="Tin">The type of the in.</typeparam>
    /// <typeparam name="Tout">The type of the out.</typeparam>
    public class GlobalService<Tin, Tout> : IGlobalService<Tin, Tout>
        where Tout : BaseTable
        where Tin : BaseTableDto
    {
        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<Tout> table;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalService{Tin, Tout}"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public GlobalService(IMapperFactory mapperFactory, IRepository<Tout> table)
        {
            this.mapperFactory = mapperFactory;
            this.table = table;
        }

        /// <summary>
        /// Gets the table.
        /// </summary>
        public IRepository<Tout> Table
        {
            get
            {
                return this.table;
            }
        }

        /// <summary>
        /// Gets the mapper factory.
        /// </summary>
        public IMapperFactory MapperFactory
        {
            get
            {
                return this.mapperFactory;
            }
        }

        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Deleted object
        /// </returns>
        public ResultMessage<Tin> DeleteById(long id, long userId)
        {
            var result = userId > 0 ? this.GetById(id) : null;

            if (result != null && !result.IsError && result.Item != null)
            {
                result.Item.IsDeleted = true;
                result.Item.ModifiedBy = userId;

                result = this.SaveOrUpdate(result.Item, userId);
            }

            return result;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        public ResultMessage<Tin> SaveOrUpdate(Tin sourceData, long userId)
        {
            var result = new ResultMessage<Tin>();

            var dbResult = this.SaveOrUpdate(new List<Tin> { sourceData }, userId);

            if (dbResult != null)
            {
                result.Messages.AddRange(dbResult.Messages);

                if (!result.IsError && dbResult.Item != null && dbResult.Item.Any())
                {
                    result.Item = dbResult.Item.FirstOrDefault();
                }
            }

            return result;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="sourceDataList">The source data list.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Tin object
        /// </returns>
        public ResultMessage<IEnumerable<Tin>> SaveOrUpdate(IEnumerable<Tin> sourceDataList, long userId)
        {
            var result = new ResultMessage<IEnumerable<Tin>>();

            var entities = sourceDataList.Select(this.mapperFactory.GetMapper<Tin, Tout>().Map).ToList();

            if (userId == 0 || !entities.Any())
            {
                result.Messages.Add(new Message(null, "Data can't be null!"));
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.ModifiedBy = userId;

                    if (entity.Id == 0)
                    {
                        entity.CreatedBy = userId;
                        entity.CreatedOn = DateTime.Now;
                        this.table.Insert(entity);
                    }
                    else
                    {
                        entity.ModifiedOn = DateTime.Now;
                        this.table.Update(entity);
                    }
                }

                var resultCount = this.table.Commit();

                if (resultCount == 0)
                {
                    result.Messages.Add(new Message(null, "Record not found!"));
                }
                else
                {
                    result.Item = entities.Select(this.mapperFactory.GetMapper<Tout, Tin>().Map);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns>Tin object</returns>
        public ResultMessage<Tin> GetById(long entityId)
        {
            var result = new ResultMessage<Tin>();

            var entity = this.table.Find(x => x.Id == entityId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<Tout, Tin>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Tin list of full data</returns>
        public ResultMessage<IEnumerable<Tin>> GetAll()
        {
            var result = new ResultMessage<IEnumerable<Tin>>();

            try
            {
                var entity = this.table.Find(x => !x.IsDeleted).ToList();

                var mapper = this.mapperFactory.GetMapper<Tout, Tin>();

                result.Item = entity.Select(mapper.Map).OrderBy(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>
        /// Tin list of full data
        /// </returns>
        public ResultMessage<IEnumerable<T>> GetAllCustom<T>() where T : BaseTableDto
        {
            var result = new ResultMessage<IEnumerable<T>>();

            try
            {
                var entity = this.table.Find(x => !x.IsDeleted).ToList();

                var mapper = this.mapperFactory.GetMapper<Tout, T>();

                result.Item = entity.Select(mapper.Map).OrderBy(x => x.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
