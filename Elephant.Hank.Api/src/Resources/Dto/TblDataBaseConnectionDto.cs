// ---------------------------------------------------------------------------------------------------
// <copyright file="TblDataBaseConnectionDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The TblDataBaseConnectionDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblDataBaseConnectionDto class
    /// </summary>
    public class TblDataBaseConnectionDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets EnvironmentIds
        /// </summary>
        public long EnvironmentId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the Server Name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Authentication
        /// </summary>
        public int Authentication { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteId
        /// </summary>
        public long WebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the DataBaseName
        /// </summary>
        public string DataBaseName { get; set; }

        /// <summary>
        /// Gets or sets the WebsiteName
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Gets or sets the Environment Name
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the Category Name
        /// </summary>
        public string CategoryName { get; set; }
    }
}
