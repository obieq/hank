// ---------------------------------------------------------------------------------------------------
// <copyright file="ExecutableTestData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-27</date>
// <summary>
//     The ExecutableTestData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The ExecutableTestData class
    /// </summary>
    public class ExecutableTestData
    {
        /// <summary>
        /// Gets or sets the test data identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the locator.
        /// </summary>
        public string Locator { get; set; }

        /// <summary>
        /// Gets or sets the locator identifier.
        /// </summary>
        public string LocatorIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the execution sequence.
        /// </summary>
        public long ExecutionSequence { get; set; }
       
        /// <summary>
        /// Gets or sets the variable name
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the step type.
        /// </summary>
        public long StepType { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public long? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public IEnumerable<NameValuePair> VariablesUsedInQuery { get; set; }

        /// <summary>
        /// Gets or sets the shared test data identifier.
        /// </summary>
        /// <value>
        /// The shared test data identifier.
        /// </value>
        public long SharedTestDataId { get; set; }
    }
}
