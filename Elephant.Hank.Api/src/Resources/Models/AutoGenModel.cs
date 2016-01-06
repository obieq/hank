// ---------------------------------------------------------------------------------------------------
// <copyright file="AutoGenModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-27</date>
// <summary>
//     The AutoGenModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    /// <summary>
    /// The AutoGenModel class
    /// </summary>
    public class AutoGenModel
    {
        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the previous text.
        /// </summary>
        public string PreviousText { get; set; }

        /// <summary>
        /// Gets or sets the automatic gen text.
        /// </summary>
        public string AutoGenText { get; set; }
    }
}
