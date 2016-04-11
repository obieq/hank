// ---------------------------------------------------------------------------------------------------
// <copyright file="IdentityResultExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-01-21</date>
// <summary>
//     The IdentityResultExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Resources.Messages;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The IdentityResultExtensions class
    /// </summary>
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// To the messages.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <returns>
        /// Message objects
        /// </returns>
        public static List<Message> ToMessages(this IdentityResult src)
        {
            List<Message> result = null;

            if (src != null && !src.Succeeded)
            {
                result = src.Errors.Select(error => new Message(error)).ToList();
            }

            return result ?? new List<Message>();
        }
    }
}
