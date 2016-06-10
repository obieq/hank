// ---------------------------------------------------------------------------------------------------
// <copyright file="SearchCriteriaData.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2016 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2016-06-08</date>
// <summary>
//     The SearchCriteriaData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The SearchCriteriaData class
    /// </summary>
    public class SearchCriteriaData
    {
        /// <summary>
        /// Gets or sets the operation systems.
        /// </summary>
        public List<NameValuePair> OperationSystems { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public List<NameValuePair> Users { get; set; }

        /// <summary>
        /// Gets or sets the test cases.
        /// </summary>
        public List<NameValuePair> TestCases { get; set; }

        /// <summary>
        /// Gets or sets the browsers.
        /// </summary>
        public List<NameValuePair> Browsers { get; set; }

        /// <summary>
        /// Gets or sets the suites.
        /// </summary>
        public List<NameValuePair> Suites { get; set; }

        /// <summary>
        /// Gets or sets the test status.
        /// </summary>
        public List<NameValuePair> TestStatus { get; set; }
    }
}
