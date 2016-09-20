// ---------------------------------------------------------------------------------------------------
// <copyright file="DataBaseConnectionService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-12-16</date>
// <summary>
//     The DataBaseConnectionService class
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
    using Elephant.Hank.Resources.Dto.InternalDtos;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The DataBaseConnectionService class
    /// </summary>
    public class DataBaseConnectionService : GlobalService<TblDataBaseConnectionDto, TblDataBaseConnection>, IDataBaseConnectionService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseConnectionService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public DataBaseConnectionService(IMapperFactory mapperFactory, IRepository<TblDataBaseConnection> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Get the Sensitive data from data base return the database username password
        /// </summary>
        /// <param name="environmentId">environment Identifier</param>
        /// <param name="categoryId">database category identifier</param>
        /// <returns>TblDataBaseConnectionDto object</returns>
        public ResultMessage<InternalTblDataBaseConnectionDto> GetSensitiveDataByEnvironmentAndCategoryId(long environmentId, long categoryId)
        {
            var result = new ResultMessage<InternalTblDataBaseConnectionDto>();

            var entity = this.Table.Find(x => x.EnvironmentId == environmentId && x.CategoryId == categoryId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblDataBaseConnection, InternalTblDataBaseConnectionDto>().Map(entity);
            }

            return result;
        }

        /// <summary>
        /// Get the list of all data bases
        /// </summary>
        /// <param name="dataBaseConnectionDto">dataBaseConnectionDto object</param>
        /// <returns>string list with all database name</returns>
        public ResultMessage<List<string>> GetAllDataBaseList(TblDataBaseConnectionDto dataBaseConnectionDto)
        {
            ResultMessage<List<string>> result = new ResultMessage<List<string>>();
            result.Item = new List<string>();
            SqlConnection con = new SqlConnection();
            if (dataBaseConnectionDto.Authentication == (int)SqlAuthenticationType.WindowsAuthentication)
            {
                con.ConnectionString = "Server=" + dataBaseConnectionDto.ServerName + ";Integrated Security=SSPI;";
            }
            else
            {
                con.ConnectionString = "Server=" + dataBaseConnectionDto.ServerName + ";user id=" + dataBaseConnectionDto.UserName + ";password=" + dataBaseConnectionDto.Password + ";";
            }

            SqlCommand cmd = new SqlCommand("select name from Sys.Databases", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string strtxt = dr["name"].ToString();
                result.Item.Add(dr["name"].ToString());
            }

            con.Close();
            return result;
        }

        /// <summary>
        /// Get the DataBaseConnection by
        /// </summary>
        /// <param name="categoryId">database category identifier</param>
        /// <returns>TblDataBaseConnectionDto object</returns>
        public ResultMessage<IEnumerable<TblDataBaseConnectionDto>> GetByCategoryId(long categoryId)
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseConnectionDto>>();

            var entities = this.Table.Find(x => x.CategoryId == categoryId && x.IsDeleted != true).ToList();

            if (entities == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblDataBaseConnection, TblDataBaseConnectionDto>();
                result.Item = entities.Select(mapper.Map);
            }

            return result;
        }

        /// <summary>
        /// Gets the by environment identifier.
        /// </summary>
        /// <param name="environmentId">The environment identifier.</param>
        /// <returns>List of database connection that match the environmentid</returns>
        public ResultMessage<IEnumerable<TblDataBaseConnectionDto>> GetByEnvironmentId(long environmentId)
        {
            var result = new ResultMessage<IEnumerable<TblDataBaseConnectionDto>>();

            var entities = this.Table.Find(x => x.EnvironmentId == environmentId && x.IsDeleted != true).ToList();

            if (entities.Count == 0)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblDataBaseConnection, TblDataBaseConnectionDto>();
                result.Item = entities.Select(mapper.Map);
            }

            return result;
        }
    }
}
