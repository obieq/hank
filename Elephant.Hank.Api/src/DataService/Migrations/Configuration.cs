// ---------------------------------------------------------------------------------------------------
// <copyright file="Configuration.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-23</date>
// <summary>
//     The Configuration class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.DataService.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Rekurant.DataService.DBSchema;
    using Rekurant.Resources.Enum;

    /// <summary>
    ///     The Configuration class
    /// </summary>
    public sealed class Configuration : DbMigrationsConfiguration<AuthContext>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Seeding Method for Making necessary DB Entries required if not exists.
        /// </summary>
        /// <param name="context">Db context</param>
        protected override void Seed(AuthContext context)
        {
            //// For Creating Roles and admin user when they doesn't exist on each DB update using Code first Approach,
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Vendor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Vendor" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName.ToLower() == "admin"))
            {
                var store = new UserStore<IdentityUser>(context);
                var manager = new UserManager<IdentityUser>(store);
                var user = new IdentityUser { UserName = "admin" };

                manager.Create(user, "urockedit");

                // manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Clients.Any())
            {
                context.Clients.AddRange(BuildClientsList());
                context.SaveChanges();
            }

            //// For Adding List of countries in DB 
            if (!context.CountryLists.Any())
            {
                context.CountryLists.AddRange(BuildCountryList());
                context.SaveChanges();
            }

            //// For Adding List of Gateways in DB
            if (!context.GatewayLists.Any())
            {
                context.GatewayLists.AddRange(BuildGatewayList());
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Builds the clients list.
        /// </summary>
        /// <returns>
        /// List of Client object
        /// </returns>
        private static IEnumerable<Client> BuildClientsList()
        {
            var clientsList = new List<Client>
                                  {
                                      new Client
                                          {
                                              Id = "ngAuthApp", 
                                              Secret = DbHelper.GetHash("abc@123"), 
                                              Name = "AngularJS front-end Application", 
                                              ApplicationType = ApplicationTypes.JavaScript, 
                                              Active = true, 
                                              RefreshTokenLifeTime = 7200, 
                                              AllowedOrigin = "http://api.rekurant.com"
                                          }, 
                                      new Client
                                          {
                                              Id = "consoleApp", 
                                              Secret = DbHelper.GetHash("123@abc"), 
                                              Name = "Console Application", 
                                              ApplicationType = ApplicationTypes.NativeConfidential, 
                                              Active = true, 
                                              RefreshTokenLifeTime = 14400, 
                                              AllowedOrigin = "*"
                                          }
                                  };

            return clientsList;
        }

        /// <summary>
        ///     Country List to be saved in DB
        /// </summary>
        /// <returns>
        ///     List of countries
        /// </returns>
        private static IEnumerable<CountryList> BuildCountryList()
        {
            var countryList = new List<CountryList>
                                  {
                                      new CountryList
                                          {
                                              Country = "United States", 
                                              CountryId = "country-us", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Andorra", 
                                              CountryId = "country-ad", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Australia", 
                                              CountryId = "country-au", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Austria", 
                                              CountryId = "country-at", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Bangladesh", 
                                              CountryId = "country-bd", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Belgium", 
                                              CountryId = "country-be", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Brazil", 
                                              CountryId = "country-br", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Brunei Darussalam", 
                                              CountryId = "country-bn", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Bulgaria", 
                                              CountryId = "country-bg", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Canada", 
                                              CountryId = "country-ca", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "China", 
                                              CountryId = "country-cn", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Croatia", 
                                              CountryId = "country-hr", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Cyprus", 
                                              CountryId = "country-cy", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Czech Republic", 
                                              CountryId = "country-cz", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Denmark", 
                                              CountryId = "country-dk", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Egypt", 
                                              CountryId = "country-eg", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Estonia", 
                                              CountryId = "country-ee", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Faroe Islands", 
                                              CountryId = "country-fo", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Finland", 
                                              CountryId = "country-fi", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "France", 
                                              CountryId = "country-fr", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Germany", 
                                              CountryId = "country-de", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Gibraltar", 
                                              CountryId = "country-gi", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Greece", 
                                              CountryId = "country-gr", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Holy See (Vatican City State)", 
                                              CountryId = "country-va", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Hong Kong", 
                                              CountryId = "country-hk", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Hungary", 
                                              CountryId = "country-hu", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Iceland", 
                                              CountryId = "country-is", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "India", 
                                              CountryId = "country-in", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Indonesia", 
                                              CountryId = "country-id", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Ireland", 
                                              CountryId = "country-ie", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Isle of Man", 
                                              CountryId = "country-im", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Israel", 
                                              CountryId = "country-il", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Italy", 
                                              CountryId = "country-it", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Japan", 
                                              CountryId = "country-jp", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Jordan", 
                                              CountryId = "country-jo", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Kuwait", 
                                              CountryId = "country-kw", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Latvia", 
                                              CountryId = "country-lv", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Lebanon", 
                                              CountryId = "country-lb", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Liechtenstein", 
                                              CountryId = "country-li", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Lithuania", 
                                              CountryId = "country-lt", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Luxembourg", 
                                              CountryId = "country-lu", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Malaysia", 
                                              CountryId = "country-my", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Maldives", 
                                              CountryId = "country-mv", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Malta", 
                                              CountryId = "country-mt", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Mauritius", 
                                              CountryId = "country-mu", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Mexico", 
                                              CountryId = "country-mx", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Monaco", 
                                              CountryId = "country-mc", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Netherlands", 
                                              CountryId = "country-nl", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "New Zealand", 
                                              CountryId = "country-nz", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Norway", 
                                              CountryId = "country-no", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Oman", 
                                              CountryId = "country-om", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Philippines", 
                                              CountryId = "country-ph", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Poland", 
                                              CountryId = "country-pl", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Portugal", 
                                              CountryId = "country-pt", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Puerto Rico", 
                                              CountryId = "country-pr", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Qatar", 
                                              CountryId = "country-qa", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Romania", 
                                              CountryId = "country-ro", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "San Marino", 
                                              CountryId = "country-sm", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Saudi Arabia", 
                                              CountryId = "country-sa", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Singapore", 
                                              CountryId = "country-sg", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Slovakia", 
                                              CountryId = "country-sk", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Slovenia", 
                                              CountryId = "country-si", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "South Africa", 
                                              CountryId = "country-za", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Spain", 
                                              CountryId = "country-es", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Sri Lanka", 
                                              CountryId = "country-lk", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Sweden", 
                                              CountryId = "country-se", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Switzerland", 
                                              CountryId = "country-ch", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Trinidad and Tobago", 
                                              CountryId = "country-tt", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Turkey", 
                                              CountryId = "country-tr", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "United Arab Emirates", 
                                              CountryId = "country-ae", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "United Kingdom", 
                                              CountryId = "country-gb", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country =
                                                  "United States Minor Outlying Islands", 
                                              CountryId = "country-um", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new CountryList
                                          {
                                              Country = "Viet Nam", 
                                              CountryId = "country-vn", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                  };
            return countryList;
        }

        /// <summary>
        ///     Gateay List to be saved in DB
        /// </summary>
        /// <returns> List of gateways</returns>
        private static IEnumerable<GatewayList> BuildGatewayList()
        {
            var gatewayList = new List<GatewayList>
                                  {
                                      new GatewayList
                                          {
                                              Gateway = "Authorize.Net", 
                                              GatewayType = "authorize_net", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Balanced", 
                                              GatewayType = "balanced", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Banwire", 
                                              GatewayType = "banwire", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Barclays ePDQ Extra Plus", 
                                              GatewayType = "barclays_epdq_extra_plus", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Barclays ePDQ MPI", 
                                              GatewayType = "barclays_epdq", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Beanstream", 
                                              GatewayType = "beanstream", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "BluePay", 
                                              GatewayType = "blue_pay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Borgun", 
                                              GatewayType = "borgun", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Braintree", 
                                              GatewayType = "braintree", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "BridgePay", 
                                              GatewayType = "bridge_pay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "CASHNet", 
                                              GatewayType = "cashnet", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "CataluynaCaixa (via Redsys)", 
                                              GatewayType = "redsys", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Cecabank", 
                                              GatewayType = "cecabank", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Conekta", 
                                              GatewayType = "conekta", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "CyberSource", 
                                              GatewayType = "cyber_source", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Dwolla", 
                                              GatewayType = "dwolla", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Elavon", 
                                              GatewayType = "elavon", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "ePay", 
                                              GatewayType = "epay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "eWAY", 
                                              GatewayType = "eway", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "eWAY Rapid", 
                                              GatewayType = "eway_rapid", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "First Data e4", 
                                              GatewayType = "first_data_e4", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "First Data Global Gateway", 
                                              GatewayType = "global", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "First Giving", 
                                              GatewayType = "first_giving", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "First Pay", 
                                              GatewayType = "first_pay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "HDFC", 
                                              GatewayType = "hdfc", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Heartland Payment Systems", 
                                              GatewayType = "hps", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "iATS Payments", 
                                              GatewayType = "iats_payments", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Iridium", 
                                              GatewayType = "iridium", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "JetPay", 
                                              GatewayType = "jetpay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Litle", 
                                              GatewayType = "litle", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway =
                                                  "MasterCard Internet Gateway Service (MiGS)", 
                                              GatewayType = "migs", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Merchant e-Solutions", 
                                              GatewayType = "merchant_e_solutions", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Merchant Warrior", 
                                              GatewayType = "merchant_warrior", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Mercury", 
                                              GatewayType = "mercury", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Moneris", 
                                              GatewayType = "moneris", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "NAB Transact", 
                                              GatewayType = "nab_transact", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "NETbilling", 
                                              GatewayType = "netbilling", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "NetPay", 
                                              GatewayType = "net_pay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "NMI", 
                                              GatewayType = "nmi", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Ogone", 
                                              GatewayType = "ogone", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Openpay", 
                                              GatewayType = "openpay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Optimal Payments", 
                                              GatewayType = "optimal_payments", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "PayEx", 
                                              GatewayType = "payex", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Payflow Pro", 
                                              GatewayType = "payflow_pro", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Payment Express", 
                                              GatewayType = "payment_express", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Paymentech Orbital Gateway", 
                                              GatewayType = "didn’t specify", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Paymill", 
                                              GatewayType = "paymill", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "PayPal", 
                                              GatewayType = "paypal", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Pin Payments", 
                                              GatewayType = "pin", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Plug'n Pay", 
                                              GatewayType = "plugnpay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "PSiGate", 
                                              GatewayType = "psi_gate", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Quantum", 
                                              GatewayType = "quantum", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "QuickBooks Merchant Services", 
                                              GatewayType = "qbms", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Quickpay", 
                                              GatewayType = "quickpay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Realex", 
                                              GatewayType = "realex", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Sage Payment Solutions", 
                                              GatewayType = "sage", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "SagePay", 
                                              GatewayType = "sage_pay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "SecureNet", 
                                              GatewayType = "secure_net", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "SecurePay Australia", 
                                              GatewayType = "secure_pay_au", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Stripe", 
                                              GatewayType = "stripe", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "TransFirst", 
                                              GatewayType = "trans_first", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "TrustCommerce", 
                                              GatewayType = "trust_commerce", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "USA ePay", 
                                              GatewayType = "usa_epay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "WePay", 
                                              GatewayType = "wepay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "Wirecard", 
                                              GatewayType = "wirecard", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                      new GatewayList
                                          {
                                              Gateway = "WorldPay", 
                                              GatewayType = "worldpay", 
                                              IsActive = true, 
                                              IsDeleted = false
                                          }, 
                                  };
            return gatewayList;
        }

        #endregion
    }
}