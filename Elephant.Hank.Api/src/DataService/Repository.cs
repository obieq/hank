// ---------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The Repository class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Resources.Attributes;

    using Npgsql;

    /// <summary>
    /// Class Repository.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object
    /// </typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Fields

        /// <summary>
        ///     The data base context
        /// </summary>
        private readonly DbContext dataBaseContext;

        /// <summary>
        ///     The data base set
        /// </summary>
        private readonly IDbSet<T> dataBaseSet;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="dataBaseContext">
        /// The database context.
        /// </param>
        public Repository(DbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
            this.dataBaseSet = this.dataBaseContext.Set<T>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// As the enumerable.
        /// </summary>
        /// <returns>
        /// IEnumerable &lt;T&gt;.
        /// </returns>
        public IEnumerable<T> AsEnumerable()
        {
            return this.dataBaseSet.AsEnumerable();
        }

        /// <summary>
        /// As the query able.
        /// </summary>
        /// <returns>
        /// IQueryable &lt;T&gt;.
        /// </returns>
        public IQueryable<T> AsQueryable()
        {
            return this.dataBaseSet.AsQueryable();
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Attach(T entity)
        {
            this.dataBaseSet.Attach(entity);
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public virtual int Commit()
        {
            return this.dataBaseContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        public void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = this.dataBaseSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
            {
                this.Delete(obj);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            this.dataBaseSet.Remove(entity);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.dataBaseContext.Dispose();
        }

        /// <summary>
        /// Executes the SQL command.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// System Int32 value.
        /// </returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return this.dataBaseContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// Finds the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// IEnumerable &lt;T&gt;.
        /// </returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.dataBaseSet.AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.Where(where);
        }

        /// <summary>
        /// Firsts the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// T of object.
        /// </returns>
        public T First(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.dataBaseSet.AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.FirstOrDefault(where);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// IEnumerable &lt;T&gt;.
        /// </returns>
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return PerformInclusions(includeProperties, this.dataBaseSet.AsQueryable());
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Insert(T entity)
        {
            this.dataBaseSet.Add(entity);
        }

        /// <summary>
        /// Singles the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>
        /// T of object.
        /// </returns>
        public T Single(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.dataBaseSet.AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.SingleOrDefault(where);
        }

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="T1">The type of the t1.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// IEnumerable &lt;T1&gt;.
        /// </returns>
        public IEnumerable<T1> SqlQuery<T1>(string sql, params object[] parameters)
        {
            return this.dataBaseContext.Database.SqlQuery<T1>(sql, parameters);
        }

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="T1">The type of the t1.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parms">The parameters.</param>
        /// <returns>
        /// IEnumerable &lt;T1&gt;.
        /// </returns>
        public IEnumerable<T1> SqlQuery<T1>(string sql, Dictionary<string, object> parms)
        {
            var parameters = new ArrayList();
            foreach (var p in parms)
            {
                parameters.Add(new NpgsqlParameter(p.Key, p.Value));
            }

            return this.dataBaseContext.Database.SqlQuery<T1>(sql, parameters.ToArray());
        }

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// IEnumerable &lt;T&gt;.
        /// </returns>
        public IEnumerable<T> SqlQuery(string sql, params object[] parameters)
        {
            return this.dataBaseContext.Database.SqlQuery<T>(sql, parameters);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            DbEntityEntry<T> entry = this.dataBaseContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                object pkey = this.dataBaseSet.Create().GetType().GetProperty("Id").GetValue(entity);

                DbSet<T> set = this.dataBaseContext.Set<T>();
                T attachedEntity = set.Find(pkey); // You need to have access to key

                if (attachedEntity != null)
                {
                    DbEntityEntry<T> attachedEntry = this.dataBaseContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                    this.dataBaseContext.Entry(attachedEntity as BaseTable).Property(x => x.CreatedOn).IsModified = false;

                    foreach (var property in attachedEntity.GetType().GetProperties())
                    {
                        var efIgnoreAttribute = property.GetCustomAttributes(false).OfType<EfIgnoreAttribute>().SingleOrDefault();

                        if (efIgnoreAttribute != null)
                        {
                            this.dataBaseContext.Entry(attachedEntity).Property(property.Name).IsModified = false;
                        }
                    }
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs the inclusions.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="query">The query.</param>
        /// <returns>
        /// IQueryable &lt;T&gt;.
        /// </returns>
        private static IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties, IQueryable<T> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        #endregion
    }
}