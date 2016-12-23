// ---------------------------------------------------------------------------------------------------
// <copyright file="TblReportLinkData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-12-23</date>
// <summary>
//     The TblReportLinkData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    /// <summary>
    /// TblReportLinkData class
    /// </summary>
    public class TblReportLinkData : BaseTable
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
