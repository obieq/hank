// ---------------------------------------------------------------------------------------------------
// <copyright file="RegexExpressionForValidation.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-30</date>
// <summary>
//     The RegexExpressionForValidation class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Constants
{
    /// <summary>
    /// The RegexExpressionForValidation class
    /// </summary>
    public class RegexExpressionForValidation
    {
        /// <summary>
        /// The email
        /// </summary>
        public const string Email = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// The no special character
        /// </summary>
        public const string NoSpecialChar = "^[a-zA-Z0-9- _]*$";

        /// <summary>
        /// The base64 string
        /// </summary>
        public const string Base64String = @"^[a-zA-Z0-9\+/]*={0,3}$";
    }
}
