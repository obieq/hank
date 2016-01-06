// ---------------------------------------------------------------------------------------------------
// <copyright file="RefreshTokenDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The RefreshTokenDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System;

    /// <summary>
    /// The RefreshTokenDto class
    /// </summary>
    public class RefreshTokenDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        public string ClientId { get; set; }

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
        public string ProtectedTicket { get; set; }
    }
}