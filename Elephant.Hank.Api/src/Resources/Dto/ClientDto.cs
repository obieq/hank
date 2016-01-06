// ---------------------------------------------------------------------------------------------------
// <copyright file="ClientDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The ClientDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using Elephant.Hank.Resources.Enum;

    /// <summary>
    /// The ClientDto class
    /// </summary>
    public class ClientDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the application.
        /// </summary>
        public ApplicationTypes ApplicationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ClientDto"/> is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the refresh token life time.
        /// </summary>
        public int RefreshTokenLifeTime { get; set; }

        /// <summary>
        /// Gets or sets the allowed origin.
        /// </summary>
        public string AllowedOrigin { get; set; }
    }
}