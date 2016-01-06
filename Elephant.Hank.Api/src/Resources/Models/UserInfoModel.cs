// ---------------------------------------------------------------------------------------------------
// <copyright file="UserInfoModel.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The UserInfoModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Models
{
    using System;

    /// <summary>
    /// the UserInfoModel class
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoModel"/> class.
        /// </summary>
        public UserInfoModel()
        {
            this.CreatedOn = DateTime.Now;
            this.ModifiedOn = DateTime.Now;
            this.PrimaryPhType = 1;
            this.SecondaryPhType = 1;
            this.Suffix = 1;
            this.Gender = 1;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        public int Suffix { get; set; }

        /// <summary>
        /// Gets or sets the dob.
        /// </summary>
        public DateTime DOB { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the Primary Ph Type.
        /// </summary>
        public int PrimaryPhType { get; set; }

        /// <summary>
        /// Gets or sets the Primary Ph.
        /// </summary>
        public string PrimaryPh { get; set; }

        /// <summary>
        /// Gets or sets the Secondary Ph Type.
        /// </summary>
        public int SecondaryPhType { get; set; }

        /// <summary>
        /// Gets or sets the Secondary Ph.
        /// </summary>
        public string SecondaryPh { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
