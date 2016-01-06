// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestQueueDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-06</date>
// <summary>
//     The TblTestQueueDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Elephant.Hank.Resources.Json;   

    /// <summary>
    /// The TblTestQueueDto class
    /// </summary>
    public class TblTestQueueDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the test identifier
        /// </summary>
        [Required]
        public long TestId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of Suite
        /// </summary>
        public long? SuiteId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of Scheduler
        /// </summary>
        public long? SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the Processed status of TestQueue
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the execution group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        public List<TblBrowsersDto> Browsers { get; set; }

        /// <summary>
        /// Gets or sets the Settings
        /// </summary>
        public TestQueueSettings Settings { get; set; }
    }
}
