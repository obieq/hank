// ---------------------------------------------------------------------------------------------------
// <copyright file="TestCaseAccessStatus.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-02-08</date>
// <summary>
//     The TestCaseAccessStatus enum
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Enum
{
    /// <summary>
    /// The TestCaseAccessStatus enum
    /// </summary>
    public enum TestCaseAccessStatus
    {
        /// <summary>
        /// Accesccible to everyone
        /// </summary>
        Public = 1,

        /// <summary>
        /// Accessible to everyone as read only
        /// </summary>
        ReadOnly = 2,

        /// <summary>
        /// Accessible only to the creator
        /// </summary>
        Private = 3
    }
}
