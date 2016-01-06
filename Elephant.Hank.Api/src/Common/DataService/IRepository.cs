// ---------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The IRepository interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// As the query able.
        /// </summary>
        /// <returns>IQueryable &lt;T&gt;.</returns>
        IQueryable<T> AsQueryable();

        /// <summary>
        /// As the enumerable.
        /// </summary>
        /// <returns>IEnumerable &lt;T&gt;.</returns>
        IEnumerable<T> AsEnumerable();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable &lt;T&gt;.</returns>
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Finds the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IEnumerable &lt;T&gt;.</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Singles the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>T of object.</returns>
        T Single(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Firsts the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>T of object.</returns>
        T First(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        void Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Attach(T entity);

        /// <summary>
        /// Executes the SQL command.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Int32 value.</returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable &lt;T&gt;.</returns>
        IEnumerable<T> SqlQuery(string sql, params object[] parameters);

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="T1">The type of the t1.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable &lt;T1&gt;.</returns>
        IEnumerable<T1> SqlQuery<T1>(string sql, params object[] parameters);

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="T1">The type of the t1.</typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parms">The parameters.</param>
        /// <returns>IEnumerable &lt;T1&gt;.</returns>
        IEnumerable<T1> SqlQuery<T1>(string sql, Dictionary<string, object> parms);

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        int Commit();
    }
}
