// ---------------------------------------------------------------------------------------------------
// <copyright file="TblUserProfile.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The TblUserProfile class
// </summary>

namespace Elephant.Hank.DataService.DBSchema
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Resources.Json;
    using Newtonsoft.Json;

    /// <summary>
    /// The TblUserProfile class
    /// </summary>
    public class TblUserProfile : BaseTable
    {
        /// <summary>
        /// Gets or sets the User settings
        /// </summary>
        [NotMapped]
        public UserProfileSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the Designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the SettingsJson
        /// </summary>
        public string SettingsJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.Settings);
            }

            set
            {
                this.Settings = string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<UserProfileSettings>(value);
            }
        }

        /// <summary>
        /// Gets or sets the User
        /// </summary>
        [ForeignKey("UserId")]
        public virtual CustomUser User { get; set; }
    }
}
