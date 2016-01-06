// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLnkSuiteTestDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblLnkSuiteTestDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto.Linking
{
    /// <summary>
    /// The TblLnkSuiteTestDto class
    /// </summary>
    public class TblLnkSuiteTestDto : BaseTableDto
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
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the suite name.
        /// </summary>
        public string SuiteName { get; set; }
    }
}
