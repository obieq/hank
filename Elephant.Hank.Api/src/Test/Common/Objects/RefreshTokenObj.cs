// ---------------------------------------------------------------------------------------------------
// <copyright file="RefreshTokenObj.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The RefreshTokenObj class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Test.Common.Objects
{
    using System;

    using Elephant.Hank.DataService.DBSchema;

    /// <summary>
    /// The refresh token obj.
    /// </summary>
    public class RefreshTokenObj
    {
        /// <summary>
        /// The get refresh token.
        /// </summary>
        /// <returns>
        /// The <see cref="TblRefreshAuthTokens"/>.
        /// </returns>
        public static TblRefreshAuthTokens GetRefreshToken()
        {
            return new TblRefreshAuthTokens
                       {
                           Token = Guid.NewGuid().ToString(),
                           Subject = "TestSub"
                       };
        }
    }
}
