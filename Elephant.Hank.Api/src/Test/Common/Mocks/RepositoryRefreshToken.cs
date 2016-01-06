// ---------------------------------------------------------------------------------------------------
// <copyright file="RepositoryRefreshToken.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The RepositoryRefreshToken class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Test.Common.Mocks
{
    using System.Collections.Generic;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.DataService.DBSchema;

    using Rhino.Mocks;

    /// <summary>
    /// The repository refresh token.
    /// </summary>
    public class RepositoryRefreshToken : BaseMock
    {
        /// <summary>
        /// The get repository.
        /// </summary>
        /// <param name="authTokens">The token.</param>
        /// <returns>
        /// The <see cref="IRepository" />.
        /// </returns>
        public static IRepository<TblRefreshAuthTokens> GetRepository(TblRefreshAuthTokens authTokens)
        {
            var repo = MockRepository.StrictMock<IRepository<TblRefreshAuthTokens>>();
            repo.Expect(v => v.Find(r => r.Subject == authTokens.Subject)).IgnoreArguments().Return(new List<TblRefreshAuthTokens> { authTokens });
            repo.Expect(v => v.Insert(Arg<TblRefreshAuthTokens>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Delete(Arg<TblRefreshAuthTokens>.Is.Anything)).IgnoreArguments();
            repo.Expect(v => v.Commit());

            return repo;
        }
    }
}
