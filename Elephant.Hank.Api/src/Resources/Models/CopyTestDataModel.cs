// ---------------------------------------------------------------------------------------------------
// <copyright file="CopyTestDataModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-12</date>
// <summary>
//     The CopyTestDataModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The CopyTestDataModel class
    /// </summary>
    public class CopyTestDataModel
    {
        /// <summary>
        /// Gets or sets the FromTestId
        /// </summary>
        public long ToTestId { get; set; }

        /// <summary>
        /// Gets or sets the FromTestId
        /// </summary>
        public long FromTestId { get; set; }

        /// <summary>
        /// Gets or sets the TestDataIdList
        /// </summary>
        public List<long> TestDataIdList { get; set; }

        /// <summary>
        /// Gets or sets the a value indicating whether to copy all data of selected steps.
        /// </summary>
        public bool CopyAll { get; set; }
    }
}
