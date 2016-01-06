// ---------------------------------------------------------------------------------------------------
// <copyright file="TblRefreshAuthTokens.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The TblRefreshAuthTokens class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The RefreshToken class
    /// </summary>
    [EfIgnoreDbLog]
    public class TblRefreshAuthTokens : BaseTable
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the issued UTC.
        /// </summary>
        public DateTime IssuedUtc { get; set; }

        /// <summary>
        /// Gets or sets the expires UTC.
        /// </summary>
        public DateTime ExpiresUtc { get; set; }

        /// <summary>
        /// Gets or sets the protected ticket.
        /// </summary>
        [Required]
        public string ProtectedTicket { get; set; }
    }
}