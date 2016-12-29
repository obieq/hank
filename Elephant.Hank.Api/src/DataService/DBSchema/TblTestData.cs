// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-17</date>
// <summary>
//     The TblTestData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.DataService.DBSchema.Linking;
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblTestData class
    /// </summary>
    public class TblTestData : BaseTable
    {
        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        [Required]
        public long TestId { get; set; }

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
        /// Gets or sets a value indicating whether this instance is shared test.
        /// </summary>
        public int LinkTestType { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestId
        /// </summary>
        public long? SharedTestId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public long? DataBaseCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        [ForeignKey("DataBaseCategoryId")]
        public virtual TblDataBaseCategories DataBaseCategories { get; set; }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        [ForeignKey("SharedTestId")]
        public virtual TblSharedTest SharedTest { get; set; }

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        [ForeignKey("TestId")]
        public virtual TblTest Test { get; set; }

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
        /// Gets or sets the SharedTestSteps
        /// </summary>
        public virtual ICollection<TblLnkTestDataSharedTestData> SharedTestSteps { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the variable id
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// Gets or sets the SharedStepWebsiteTestId
        /// </summary>
        public long? SharedStepWebsiteTestId { get; set; }

        /// <summary>
        /// Gets or sets the SharedStepWebsiteId
        /// </summary>
        public long? SharedStepWebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the API test data identifier.
        /// </summary>
        /// <value>
        /// The API test data identifier.
        /// </value>
        public long? ApiTestDataId { get; set; }

        /// <summary>
        /// Gets or sets the day till past.
        /// </summary>
        /// <value>
        /// The day till past.
        /// </value>
        public long? DayTillPast { get; set; }

        /// <summary>
        /// Gets or sets the day till past by date.
        /// </summary>
        /// <value>
        /// The day till past by date.
        /// </value>
        public DateTime? DayTillPastByDate { get; set; }

        /// <summary>
        /// Gets or sets the API test data.
        /// </summary>
        /// <value>
        /// The API test data.
        /// </value>
        [ForeignKey("ApiTestDataId")]
        public virtual TblApiTestData ApiTestData { get; set; }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        [ForeignKey("SharedStepWebsiteId")]
        public virtual TblWebsite Website { get; set; }

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        [ForeignKey("SharedStepWebsiteTestId")]
        public virtual TblTest SharedStepWebsiteTest { get; set; }

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
