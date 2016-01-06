// ---------------------------------------------------------------------------------------------------
// <copyright file="FullTraceJson.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-03-23</date>
// <summary>
//     The FullTraceJson class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    using Newtonsoft.Json;

    /// <summary>
    /// Trace class
    /// </summary>
    public class FullTraceJson
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the matcher.
        /// </summary>
        public string MatcherName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FullTraceJson"/> is passed.
        /// </summary>
        [JsonProperty("passed_")]
        public bool Passed { get; set; }

        /// <summary>
        /// Gets or sets the expected.
        /// </summary>
        public string Expected { get; set; }

        /// <summary>
        /// Gets or sets the actual.
        /// </summary>
        public string Actual { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the trace.
        /// </summary>
        public ErrorTraceJson Trace { get; set; }
    }
}
