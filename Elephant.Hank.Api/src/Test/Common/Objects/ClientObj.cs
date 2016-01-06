// ---------------------------------------------------------------------------------------------------
// <copyright file="ClientObj.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The ClientObj class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Test.Common.Objects
{
    using System;

    using Elephant.Hank.DataService.DBSchema;

    /// <summary>
    /// The client obj.
    /// </summary>
    public class ClientObj
    {
        /// <summary>
        /// The get client obj.
        /// </summary>
        /// <returns>
        /// The <see cref="TblAuthClients"/>.
        /// </returns>
        public static TblAuthClients GetClientObj()
        {
            return new TblAuthClients
                       {
                           ClientId = Guid.NewGuid().ToString(),
                           Name = "TestUser",
                           IsActive = true
                       };
        }
    }
}
