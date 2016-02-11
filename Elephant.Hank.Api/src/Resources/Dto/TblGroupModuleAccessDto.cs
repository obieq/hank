// ---------------------------------------------------------------------------------------------------
// <copyright file="TblGroupModuleAccessDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-01-11</date>
// <summary>
//     The TblGroupModuleAccessDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblGroupModuleAccessDto class
    /// </summary>
    public class TblGroupModuleAccessDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the GroupId
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or sets the ModuleId
        /// </summary>
        public long ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can read.
        /// </summary>
        public bool CanRead { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can write.
        /// </summary>
        public bool CanWrite { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can delete.
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can execute.
        /// </summary>
        public bool CanExecute { get; set; }

        /// <summary>
        /// Gets or sets the Group
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the ModuleId
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the Website
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is module executable.
        /// </summary>
        public bool IsModuleExecutable { get; set; }    
    }
}
