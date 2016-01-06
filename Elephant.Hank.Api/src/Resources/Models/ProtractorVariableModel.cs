// ---------------------------------------------------------------------------------------------------
// <copyright file="ProtractorVariableModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-29</date>
// <summary>
//     The ProtractorVariableModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    /// <summary>
    /// the ProtractorVariableModel class
    /// </summary>
    public class ProtractorVariableModel
    {
        /// <summary>
        /// Gets or sets the id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public int ExecutionSequence { get; set; }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is shared test.
        /// </summary>
        public bool IsSharedTest { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestId
        /// </summary>
        public long? SharedTestId { get; set; }

        /// <summary>
        /// Gets or sets the VariableId
        /// </summary>
        public long? VariableId { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestName
        /// </summary>
        public string SharedTestName { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestDataId
        /// </summary>
        public int? SharedTestDataId { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }
    }
}
