// ---------------------------------------------------------------------------------------------------
// <copyright file="ChangePasswordModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-13</date>
// <summary>
//     The ChangePasswordModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    /// <summary>
    /// The ChangePasswordModel class
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// Gets or sets the current password
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the NewPassword
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the Confirm new password
        /// </summary>
        public string ConfirmNewPassword { get; set; }
    }
}
