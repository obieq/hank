// ---------------------------------------------------------------------------------------------------
// <copyright file="ResultMessage.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The ResultMessage class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The result message.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    public class ResultMessage<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultMessage{T}"/> class.
        /// </summary>
        public ResultMessage()
        {
            this.Messages = new List<Message>();
            this.CreatedOn = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the selenium address.
        /// </summary>
        public string SeleniumAddress { get; set; }

        /// <summary>
        /// Gets or sets the name of the scheduler.
        /// </summary>
        public long? SchedulerId { get; set; }

        /// <summary>
        /// Gets or sets the test queue identifier.
        /// </summary>
        public long TestQueueId { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public List<Message> Messages { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        public bool IsError
        {
            get
            {
                return this.Messages.Any();
            }
        }
    }
}