// ---------------------------------------------------------------------------------------------------
// <copyright file="RepositoryClientMock.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The RepositoryClientMock class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Test.Common.Mocks
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.DataService.DBSchema;

    using Rhino.Mocks;

    /// <summary>
    /// The IRepositoryClientMock interface
    /// </summary>
    public class RepositoryClientMock : BaseMock
    {
        /// <summary>
        /// The get client repository.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>
        /// The IRepository of Client
        /// </returns>
        public static IRepository<TblAuthClients> GetRepository(TblAuthClients client)
        {
            var repo = MockRepository.StrictMock<IRepository<TblAuthClients>>();
            repo.Expect(v => v.Find(x => x.Id == client.Id)).IgnoreArguments().Return(new List<TblAuthClients> { client });
            repo.Expect(v => v.Insert(Arg<TblAuthClients>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Delete(Arg<TblAuthClients>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Commit());

            return repo;
        }
    }
}
