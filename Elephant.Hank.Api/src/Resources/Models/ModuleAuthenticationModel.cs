// ---------------------------------------------------------------------------------------------------
// <copyright file="ModuleAuthenticationModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-25</date>
// <summary>
//     The ModuleAuthenticationModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using Elephant.Hank.Resources.Enum;

    /// <summary>
    /// The ModuleAuthenticationModel class.
    /// </summary>
    public class ModuleAuthenticationModel
    {
        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public int WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the ModuleId.
        /// </summary>
        public FrameworkModules ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the ModuleName.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteName
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        ///  Gets or sets the a value indicating whether user can write or not
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        ///  Gets or sets the a value indicating whether user can write or not
        /// </summary>
        public bool CanWrite { get; set; }

        /// <summary>
        ///  Gets or sets the a value indicating whether user can delete or not
        /// </summary>
        public bool CanExecute { get; set; }
    }
}
