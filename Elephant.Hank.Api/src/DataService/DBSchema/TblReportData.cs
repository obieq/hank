// ---------------------------------------------------------------------------------------------------
// <copyright file="TblReportData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-30</date>
// <summary>
//     The TblReportData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{   
    using System.ComponentModel.DataAnnotations.Schema;
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblReportData class
    /// </summary>
    [EfIgnoreDbLog]
    public class TblReportData : BaseTable
    {
        /// <summary>
        /// Gets or sets the test queue identifier.
        /// </summary>
        public long TestQueueId { get; set; }
      
        /// <summary>
        /// Gets or sets the execution group.
        /// </summary>
        public string ExecutionGroup { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the suite.
        /// </summary>
        [ForeignKey("TestQueueId")]
        public virtual TblTestQueue TestQueue { get; set; }      
    }
}