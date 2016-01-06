// ---------------------------------------------------------------------------------------------------
// <copyright file="TblAuthClients.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The TblAuthClients class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations;

    using Elephant.Hank.Resources.Enum;

    /// <summary>
    /// The Client class
    /// </summary>
    public class TblAuthClients : BaseTable
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        [Required]
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the application.
        /// </summary>
        public ApplicationTypes ApplicationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TblAuthClients"/> is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the refresh token life time.
        /// </summary>
        public int RefreshTokenLifeTime { get; set; }

        /// <summary>
        /// Gets or sets the allowed origin.
        /// </summary>
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}