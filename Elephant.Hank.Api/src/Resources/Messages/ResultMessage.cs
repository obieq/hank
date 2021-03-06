﻿// ---------------------------------------------------------------------------------------------------
// <copyright file="ResultMessage.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-15</date>
// <summary>
//     The ResultMessage class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Messages
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultMessage{T}"/> class.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public ResultMessage(List<Message> messages)
        {
            this.Messages = messages ?? new List<Message>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultMessage{T}" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ResultMessage(Message message)
        {
            this.Messages = new List<Message>();
            this.Messages.Add(message);
        }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? Total { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? StartedAt { get; set; }

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