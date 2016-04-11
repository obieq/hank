// ---------------------------------------------------------------------------------------------------
// <copyright file="WebsiteService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The WebsiteService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The WebsiteService class
    /// </summary>
    public class WebsiteService : GlobalService<TblWebsiteDto, TblWebsite>, IWebsiteService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The group Module Access Service
        /// </summary>
        private readonly GroupModuleAccessService groupModuleAccessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebsiteService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="groupModuleAccessService">The groupModuleAccessService.</param>
        public WebsiteService(IMapperFactory mapperFactory, IRepository<TblWebsite> table, GroupModuleAccessService groupModuleAccessService)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.groupModuleAccessService = groupModuleAccessService;
        }

        /// <summary>
        /// Get All website Authenticated to user
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <param name="isAdminUser">if set to <c>true</c> [is admin user].</param>
        /// <returns>
        /// List of WebsiteDto
        /// </returns>
        public ResultMessage<IEnumerable<TblWebsiteDto>> GetAllUserAuthenticatedWebsites(long userId, bool isAdminUser)
        {
            if (isAdminUser)
            {
                return this.GetAll();
            }

            var result = new ResultMessage<IEnumerable<TblWebsiteDto>>();
            var resultAuthenticatedModule = this.groupModuleAccessService.GetModuleAuthenticatedToUser(userId);
            var allowedWebSites = resultAuthenticatedModule.Item == null ? new List<long>() : resultAuthenticatedModule.Item.GroupBy(x => x.WebsiteId).Select(g => g.Key).ToList();

            IEnumerable<TblWebsiteDto> websitesDto = allowedWebSites.Any() ? this.GetAll().Item.Where(x => allowedWebSites.Any(y => y == x.Id)) : new List<TblWebsiteDto>();

            result.Item = websitesDto;
            return result;
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>TblWebsiteDto object</returns>
        public ResultMessage<TblWebsiteDto> GetByName(string name)
        {
            var result = new ResultMessage<TblWebsiteDto>();

            name = (name + string.Empty).ToLower();

            var entity = this.Table.Find(x => (x.Name + string.Empty).ToLower() == name && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblWebsite, TblWebsiteDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Get all variable related to website for autocomplete help
        /// </summary>
        /// <param name="websiteId">the website identifier</param>
        /// <returns>list of variable name</returns>
        public ResultMessage<IEnumerable<string>> GetAllVariableByWebsiteIdForAutoComplete(long websiteId)
        {
            var result = new ResultMessage<IEnumerable<string>>();
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "websiteid", websiteId } };

            var entities = this.Table.SqlQuery<string>("Select * from procgetallvariablerelatedtowebsite(@websiteid);", dictionary).ToList();

            if (entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = entities;
            }

            return result;
        }
    }
}