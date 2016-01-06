// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSharedTestDataDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The TblSharedTestDataDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.ComponentModel.DataAnnotations;
    using Elephant.Hank.Resources.Dto.CustomIdentity;    

    /// <summary>
    /// The TblSharedTestDataDto class
    /// </summary>
    public class TblSharedTestDataDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        [Required]
        public long SharedTestId { get; set; }

        /// <summary>
        /// Gets or sets the execution sequence.
        /// </summary>
        [Required]
        public long ExecutionSequence { get; set; }

        /// <summary>
        /// Gets or sets the locator identifier identifier.
        /// </summary>
        public long? LocatorIdentifierId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ignored.
        /// </summary>
        public bool IsIgnored { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        public long PageId { get; set; }

        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        public long? ActionId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayNameValue { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string ActionValue { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string LocatorValue { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string LocatorIdentifierValue { get; set; }       

        /// <summary>
        /// Gets or sets the variable name
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the StepType
        /// </summary>
        public int StepType { get; set; }

        /// <summary>
        /// Gets or sets the DataBaseCategoryId
        /// </summary>
        public long? DataBaseCategoryId { get; set; }
    }
}
