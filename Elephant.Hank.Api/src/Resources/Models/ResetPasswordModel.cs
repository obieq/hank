// ---------------------------------------------------------------------------------------------------
// <copyright file="ResetPasswordModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-09</date>
// <summary>
//     The ResetPasswordModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The ResetPassword class
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Required]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
