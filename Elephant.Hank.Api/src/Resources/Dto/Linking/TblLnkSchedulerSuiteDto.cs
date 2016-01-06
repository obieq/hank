// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLnkSchedulerSuiteDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-20</date>
// <summary>
//     The TblLnkSchedulerSuiteDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto.Linking
{
    /// <summary>
    /// the TblSchedulerSuiteDto class
    /// </summary>
    public class TblLnkSchedulerSuiteDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the SuiteId
        /// </summary>
        public long SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the SchedulerId
        /// </summary>
        public long SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the Suite
        /// </summary>
        public string SuiteName { get; set; }
    }
}
