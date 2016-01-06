// ---------------------------------------------------------------------------------------------------
// <copyright file="TestQueue.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The TestQueue class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The TestQueue class
    /// </summary>
    public class TestQueue : BaseApiModel
    {
        /// <summary>
        /// Gets or sets the test identifier
        /// </summary>
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
        public List<Browsers> Browsers { get; set; }

        /// <summary>
        /// Gets or sets the Settings
        /// </summary>
        public TestQueueSettings Settings { get; set; }

        public Hub hubInfo { get; set; }
    }
}
