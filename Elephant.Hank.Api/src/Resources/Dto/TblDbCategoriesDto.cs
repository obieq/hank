// ---------------------------------------------------------------------------------------------------
// <copyright file="TblDbCategoriesDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-11</date>
// <summary>
//     The TblDbCategoriesDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblDbCategoriesDto class
    /// </summary>
    public class TblDBCategoriesDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the EnvironmentId
        /// </summary>
        public long EnvironmentId { get; set; }

        /// <summary>
        /// Gets or sets the CategoryName
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the ServerName
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Type
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
        /// Gets or sets the EnvironmentName
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the website name
        /// </summary>
        public string WebsiteName { get; set; }
    }
}
