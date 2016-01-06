// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseApiModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The BaseApiModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.ApiModels
{
    using System;

    /// <summary>
    /// The BaseApiModel class
    /// </summary>
    public class BaseApiModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiModel"/> class.
        /// </summary>
        public BaseApiModel()
        {
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
            this.IsDeleted = false;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public long ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }   
    }
}
