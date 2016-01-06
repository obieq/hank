// ---------------------------------------------------------------------------------------------------
// <copyright file="DBCategoriesService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-11</date>
// <summary>
//     The DBCategoriesService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The DBCategoriesService class
    /// </summary>
    public class DBCategoriesService : GlobalService<TblDBCategoriesDto, TblDBCategories>, IDBCategoriesService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBCategoriesService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public DBCategoriesService(IMapperFactory mapperFactory, IRepository<TblDBCategories> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the by web site identifier.
        /// </summary>
        /// <param name="webSiteId">The web site identifier.</param>
        /// <returns>
        /// TblTestDto objects
        /// </returns>
        public ResultMessage<IEnumerable<TblDBCategoriesDto>> GetByWebSiteId(long webSiteId)
        {
            var result = new ResultMessage<IEnumerable<TblDBCategoriesDto>>();
            var entity = this.Table.Find(x => x.WebsiteId == webSiteId && x.IsDeleted != true).ToList();
            var mapper = this.mapperFactory.GetMapper<TblDBCategories, TblDBCategoriesDto>();
            result.Item = entity.Select(mapper.Map);
            return result;
        }

        /// <summary>
        /// Get the list of data base
        /// </summary>
        /// <param name="dBCategoriesDto">data base idebtifier details</param>
        /// <returns>List of all databases</returns>
        public ResultMessage<List<string>> GetDatabaseList(TblDBCategoriesDto dBCategoriesDto)
        {
            ResultMessage<List<string>> result = new ResultMessage<List<string>>();
            result.Item = new List<string>();
            SqlConnection con = new SqlConnection();
            if (dBCategoriesDto.Authentication == (int)SqlAuthenticationType.WindowsAuthentication)
            {
                con.ConnectionString = "Server=" + dBCategoriesDto.ServerName + ";Integrated Security=SSPI;";
            }
            else
            {
                con.ConnectionString = "Server=" + dBCategoriesDto.ServerName + ";user id=" + dBCategoriesDto.UserName + ";password=" + dBCategoriesDto.Password + ";";
            }

            SqlCommand cmd = new SqlCommand("select name from Sys.Databases", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string strtxt = dr["name"].ToString();
                result.Item.Add(dr["name"].ToString());
            }

            return result;
        }
    }
}
