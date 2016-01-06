// ---------------------------------------------------------------------------------------------------
// <copyright file="SqlAuthenticationType.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-129</date>
// <summary>
//     The SqlAuthenticationType class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Enum
{
    /// <summary>
    /// The SqlAuthenticationType enum
    /// </summary>
    public enum SqlAuthenticationType
    {
        /// <summary>
        /// The unknown type
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The windows Authentication
        /// </summary>
        WindowsAuthentication = 1,

        /// <summary>
        /// The Sql Server Authentication
        /// </summary>
        SqlServerAuthentication = 2
    }
}
