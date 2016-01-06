// ---------------------------------------------------------------------------------------------------
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