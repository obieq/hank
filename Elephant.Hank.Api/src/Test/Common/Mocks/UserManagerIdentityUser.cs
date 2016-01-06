// ---------------------------------------------------------------------------------------------------
// <copyright file="UserManagerIdentityUser.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The UserManagerIdentityUser class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Test.Common.Mocks
{
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Rhino.Mocks;

    /// <summary>
    /// The user manager identity user.
    /// </summary>
    public class UserManagerIdentityUser : BaseMock
    {
        /// <summary>
        /// The getre user manager.
        /// </summary>
        /// <param name="identityUser">The identity user.</param>
        /// <returns>
        /// The <see cref="UserManager" />.
        /// </returns>
        public static UserManager<IdentityUser> GetreUserManager(Task<IdentityUser> identityUser)
        {
            var repo = MockRepository.StrictMock<UserManager<IdentityUser>>();
            repo.Expect(v => v.CreateAsync(null, null)).IgnoreArguments();
            repo.Expect(v => v.AddToRole(null, null)).IgnoreArguments();
            repo.Expect(v => v.FindAsync(null, null)).IgnoreArguments().Return(identityUser);

            return repo;
        }
    }
}
