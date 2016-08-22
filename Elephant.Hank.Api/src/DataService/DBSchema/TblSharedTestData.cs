// ---------------------------------------------------------------------------------------------------
// <copyright file="TblSharedTestData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The TblSharedTestData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblSharedTestData class
    /// </summary>
    public class TblSharedTestData : BaseTable
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
        /// Gets or sets the action identifier.
        /// </summary>
        public long? ActionId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ignored.
        /// </summary>
        public bool IsIgnored { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        [ForeignKey("SharedTestId")]
        public virtual TblSharedTest SharedTest { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [ForeignKey("LocatorIdentifierId")]
        public virtual TblLocatorIdentifier LocatorIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        [ForeignKey("ActionId")]
        public virtual TblAction Action { get; set; }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        [ForeignKey("DataBaseCategoryId")]
        public virtual TblDataBaseCategories DataBaseCategories { get; set; }

        /// <summary>
        /// Gets or sets the Variable identifier
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public long? DataBaseCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the StepType
        /// </summary>
        public int StepType { get; set; }

        /// <summary>
        /// Gets or sets the API test data identifier.
        /// </summary>
        /// <value>
        /// The API test data identifier.
        /// </value>
        public long? ApiTestDataId { get; set; }

        /// <summary>
        /// Gets or sets the API test data.
        /// </summary>
        /// <value>
        /// The API test data.
        /// </value>
        [ForeignKey("ApiTestDataId")]
        public virtual TblApiTestData ApiTestData { get; set; }

        /// <summary>
        /// Gets or sets the name of the modified by user.
        /// </summary>
        /// <value>
        /// The name of the modified by user.
        /// </value>
        [ForeignKey("ModifiedBy")]
        public virtual CustomUser ModifiedByUser { get; set; }
    }
}
