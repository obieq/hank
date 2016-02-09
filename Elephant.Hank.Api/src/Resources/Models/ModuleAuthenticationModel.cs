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
        public long WebsiteId { get; set; }

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
        /// Gets or sets a value indicating whether this instance can delete.
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can write.
        /// </summary>
        public bool CanWrite { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can execute.
        /// </summary>
        public bool CanExecute { get; set; }

        /// <summary>
        /// Gets or sets the operation identifier.
        /// </summary>
        public int OperationId { get; set; }
    }
}
