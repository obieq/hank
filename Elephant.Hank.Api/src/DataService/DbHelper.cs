// ---------------------------------------------------------------------------------------------------
// <copyright file="DbHelper.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The DbHelper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// The DbHelper class
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Hased value as string</returns>
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
       
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}