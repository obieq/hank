// ---------------------------------------------------------------------------------------------------
// <copyright file="LinkTestType.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-05</date>
// <summary>
//     The LinkTestType enum
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Enum
{
    /// <summary>
    /// The LinkTestType enum
    /// </summary>
    public enum LinkTestType
    {
        /// <summary>
        /// for simple test step
        /// </summary>
        TestStep = 0,

        /// <summary>
        /// for shared test as test step
        /// </summary>
        SharedTest = 1,

        /// <summary>
        /// for test itself as test step
        /// </summary>
        SharedWebsiteTest = 2,

        /// <summary>
        /// for simple test step
        /// </summary>
        SqlTestStep = 3,

        /// <summary>
        /// /// for Api Test Step      
        /// </summary>
        ApiTestStep = 4
    }
}
