// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLnkSuiteTest.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-17</date>
// <summary>
//     The TblLnkSuiteTest class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.Linking
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.DataService.DBSchema;

    /// <summary>
    /// The TblSuiteTest class
    /// </summary>
    public class TblLnkSuiteTest : BaseTable
    {
        /// <summary>
        /// Gets or sets the suite identifier.
        /// </summary>
        public long SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        public long TestId { get; set; }

        /// <summary>
        /// Gets or sets the state of the test.
        /// </summary>
        public long TestState { get; set; }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        [ForeignKey("TestId")]
        public virtual TblTest Test { get; set; }

        /// <summary>
        /// Gets or sets the suite.
        /// </summary>
        [ForeignKey("SuiteId")]
        public virtual TblSuite Suite { get; set; }
    }
}
