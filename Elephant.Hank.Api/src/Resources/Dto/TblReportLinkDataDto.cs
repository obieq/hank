// ---------------------------------------------------------------------------------------------------
// <copyright file="TblReportLinkDataDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-12-23</date>
// <summary>
//     The TblReportLinkDataDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// TblReportLinkDataDto class.
    /// </summary>
    public class TblReportLinkDataDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the report identifier.
        /// </summary>
        /// <value>
        /// The report identifier.
        /// </value>
        public long ReportId { get; set; }

        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        /// <value>
        /// The test identifier.
        /// </value>
        public long TestId { get; set; }
    }
}
