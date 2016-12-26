// ---------------------------------------------------------------------------------------------------
// <copyright file="TblReportExecutionLinkData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-12-23</date>
// <summary>
//     The TblReportExecutionLinkData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    /// <summary>
    /// TblReportExecutionLinkData class
    /// </summary>
    public class TblReportExecutionLinkData : BaseTable
    {       
        /// <summary>
        /// Gets or sets the report identifier.
        /// </summary>
        /// <value>
        /// The report identifier.
        /// </value>
        public long ReportDataId { get; set; }

        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        /// <value>
        /// The test identifier.
        /// </value>
        public long TestId { get; set; }
    }
}
