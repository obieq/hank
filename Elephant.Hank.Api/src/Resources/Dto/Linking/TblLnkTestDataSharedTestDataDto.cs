// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLnkTestDataSharedTestDataDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-09</date>
// <summary>
//     The TblLnkTestDataSharedTestDataDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto.Linking
{
    using Elephant.Hank.Resources.Dto.CustomIdentity;

    /// <summary>
    /// The TblLnkTestDataSharedTestDataDto class
    /// </summary>
    public class TblLnkTestDataSharedTestDataDto : BaseTableDto
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
    }
}
