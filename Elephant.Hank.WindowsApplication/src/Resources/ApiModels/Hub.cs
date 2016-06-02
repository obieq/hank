// ---------------------------------------------------------------------------------------------------
// <copyright file="Hub.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-06-02</date>
// <summary>
//     The Hub class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System;

    /// <summary>
    /// The hub.
    /// </summary>
    public class Hub
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hub"/> class.
        /// </summary>
        public Hub()
        {
            this.StartedAt = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the process id.
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// Gets or sets the selenium address.
        /// </summary>
        public string SeleniumAddress { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// Gets or sets the scheduler name.
        /// </summary>
        public long? SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the test queue identifier.
        /// </summary>
        public long TestQueueId { get; set; }
    }
}
