// ---------------------------------------------------------------------------------------------------
// <copyright file="IGlobalService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The IGlobalService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.DataService
{
    /// <summary>
    /// The IGlobalRepository interface
    /// </summary>
    /// <typeparam name="Tin">The type of the in.</typeparam>
    /// <typeparam name="Tout">The type of the out.</typeparam>
    public interface IGlobalService<Tin, Tout> : IBaseService<Tin>
        where Tout : class
    {
        /// <summary>
        /// Gets the table.
        /// </summary>
        IRepository<Tout> Table { get; }
    }
}