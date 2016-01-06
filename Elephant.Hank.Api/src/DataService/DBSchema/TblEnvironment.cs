// ---------------------------------------------------------------------------------------------------
// <copyright file="TblEnvironment.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-04</date>
// <summary>
//     The TblEnvironment class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    /// <summary>
    /// The TblEnvironment class
    /// </summary>
    public class TblEnvironment : BaseTable
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }
    }
}
