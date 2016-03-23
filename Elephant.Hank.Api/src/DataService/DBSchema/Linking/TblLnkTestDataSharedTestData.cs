// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLnkTestDataSharedTestData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-09</date>
// <summary>
//     The TblLnkTestDataSharedTestData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema.Linking
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblLnkTestDataSharedTestData class
    /// </summary>
    public class TblLnkTestDataSharedTestData : BaseTable
    {
        /// <summary>
        /// Gets or sets the TestDataId
        /// </summary>
        public long TestDataId { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestDataId
        /// </summary>
        public long SharedTestDataId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ignored.
        /// </summary>
        public bool? IsIgnored { get; set; }

        /// <summary>
        /// Gets or sets the NewValue
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the NewOrder
        /// </summary>
        public long NewOrder { get; set; }

        /// <summary>
        /// Gets or sets the new variable.
        /// </summary>
        public string NewVariable { get; set; }

        /// <summary>
        /// Gets or sets the TestData
        /// </summary>
        [ForeignKey("TestDataId")]
        public virtual TblTestData TestData { get; set; }
    }
}
