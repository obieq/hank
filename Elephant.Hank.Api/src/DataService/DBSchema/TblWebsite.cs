// ---------------------------------------------------------------------------------------------------
// <copyright file="TblWebsite.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-04-17</date>
// <summary>
//     The TblWebsite class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Elephant.Hank.Resources.Json;
    using Newtonsoft.Json;

    /// <summary>
    /// The TblWebsite class
    /// </summary>
    public class TblWebsite : BaseTable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is angular.
        /// </summary>
        [Required]
        public bool IsAngular { get; set; }

        /// <summary>
        /// Gets or sets UrlList
        /// </summary>
        public string UrlList { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of TblUrl
        /// </summary>
        [NotMapped]
        public IEnumerable<WebsiteUrl> WebsiteUrlList
        {
            get
            {
                if (!string.IsNullOrEmpty(this.UrlList))
                {
                    return JsonConvert.DeserializeObject<List<WebsiteUrl>>(this.UrlList);
                }

                return null;
            }

            set
            {
                int index = 1;
                foreach (var item in value)
                {
                    item.Id = index++;
                }

                this.UrlList = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public string SettingsJson
        {
            get
            {
                return JsonConvert.SerializeObject(this.Settings);
            }

            set
            {
                this.Settings = string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<WebsiteSettings>(value);
            }
        }

        /// <summary>
        /// Gets or sets the setting.
        /// </summary>
        [NotMapped]
        public WebsiteSettings Settings { get; set; }
    }
}
