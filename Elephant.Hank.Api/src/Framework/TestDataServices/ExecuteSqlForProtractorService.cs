// ---------------------------------------------------------------------------------------------------
// <copyright file="ExecuteSqlForProtractorService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-02-17</date>
// <summary>
//     The ExecuteSqlForProtractorService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// The ExecuteSqlForProtractorService class
    /// </summary>
    public class ExecuteSqlForProtractorService : IExecuteSqlForProtractorService
    {
        /// <summary>
        /// The testQueueu service
        /// </summary>
        private readonly ITestQueueService testQueueService;

        /// <summary>
        /// The logger service
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// The scheduler service
        /// </summary>
        private readonly ISchedulerService schedulerService;

        /// <summary>
        /// The testQueueu service
        /// </summary>
        private readonly IDataBaseConnectionService dataBaseConnectionService;

        /// <summary>
        /// The environment service
        /// </summary>
        private readonly IEnvironmentService environmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteSqlForProtractorService" /> class.
        /// </summary>
        /// <param name="testQueueService">the test Queue Service</param>
        /// <param name="dataBaseConnectionService">the DataBase Connection Service</param>
        /// <param name="schedulerService">The Scheduler Service</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="environmentService">The environment service.</param>
        public ExecuteSqlForProtractorService(ITestQueueService testQueueService, IDataBaseConnectionService dataBaseConnectionService, ISchedulerService schedulerService, ILoggerService loggerService, IEnvironmentService environmentService)
        {
            this.testQueueService = testQueueService;
            this.dataBaseConnectionService = dataBaseConnectionService;
            this.schedulerService = schedulerService;
            this.loggerService = loggerService;
            this.environmentService = environmentService;
        }

        /// <summary>
        /// Execute the Sql Querry requested buy protractor
        /// </summary>
        /// <param name="executableTestDataStep">the ExecutableTestData object</param>
        /// <param name="testQueueId">the testQueue identifier</param>
        /// <returns>result of querry in string</returns>
        public ResultMessage<object> ExecuteQuery(ExecutableTestData executableTestDataStep, long testQueueId)
        {
            ResultMessage<TblTestQueueDto> resultTestQueue = this.testQueueService.GetById(testQueueId);
            if (resultTestQueue.Item.SchedulerId.HasValue)
            {
                ResultMessage<TblSchedulerDto> schedulerDto = this.schedulerService.GetById(resultTestQueue.Item.SchedulerId.Value);
                long environMentId = Convert.ToInt64(schedulerDto.Item.UrlId);
                if (environMentId == 0)
                {
                    environMentId = this.environmentService.GetDefaultEnvironment().Item.Id;
                }

                return this.ExecuteSql(executableTestDataStep, environMentId);
            }
            else
            {
                long environMentId = Convert.ToInt64(resultTestQueue.Item.Settings.UrlId);
                if (environMentId == 0)
                {
                    environMentId = this.environmentService.GetDefaultEnvironment().Item.Id;
                }

                return this.ExecuteSql(executableTestDataStep, environMentId);
            }
        }

        /// <summary>
        /// Execute Sql Command on Sql Server
        /// </summary>
        /// <param name="executableTestDataStep">the executable data step</param>
        /// <param name="environMentId">the Environment Identifier</param>
        /// <returns>resul of query execution</returns>
        private ResultMessage<object> ExecuteSql(ExecutableTestData executableTestDataStep, long environMentId)
        {
            var result = new ResultMessage<object>();
            try
            {
                var dataBaseConnectionDto = this.dataBaseConnectionService.GetSensitiveDataByEnvironmentAndCategoryId(environMentId, executableTestDataStep.CategoryId.Value);
                if (dataBaseConnectionDto.Item != null)
                {
                    SqlConnection con = new SqlConnection();
                    if (dataBaseConnectionDto.Item.Authentication == (int)SqlAuthenticationType.WindowsAuthentication)
                    {
                        con.ConnectionString = "Server=" + dataBaseConnectionDto.Item.ServerName + ";Database=" + dataBaseConnectionDto.Item.DataBaseName + ";Integrated Security=SSPI;";
                    }
                    else
                    {
                        con.ConnectionString = "Server=" + dataBaseConnectionDto.Item.ServerName + ";Database=" + dataBaseConnectionDto.Item.DataBaseName + ";user id=" + dataBaseConnectionDto.Item.UserName + ";password=" + dataBaseConnectionDto.Item.Password + ";";
                    }

                    SqlCommand cmd = this.ParseQueryForParameters(executableTestDataStep);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        string jsonString = "[{'RecordsAffected':" + dr.RecordsAffected + "}]";
                        object jobject = (object)JsonConvert.DeserializeObject(jsonString);
                        result.Item = jobject;
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        if (dt.Rows.Count > 0)
                        {
                            string jsonString = string.Empty;
                            DataTable dtNew = new DataTable();
                            dtNew = dt.AsEnumerable().Take(int.Parse(ConfigurationManager.AppSettings["ExecuteSqlRowsToTake"].ToString())).CopyToDataTable();
                            jsonString = JsonConvert.SerializeObject(dtNew);
                            object jobject = (object)JsonConvert.DeserializeObject(jsonString);
                            result.Item = jobject;
                        }
                        else
                        {
                            result.Item = "null";
                        }
                    }

                    con.Close();
                }
                else
                {
                    string jsonString = "[{'ExceptionMessage':'No connection strung found with the mentioned category and environment id'}]";
                    object jobject = (object)JsonConvert.DeserializeObject(jsonString);
                    result.Item = jobject;
                }

                return result;
            }
            catch (Exception ex)
            {
                this.loggerService.LogException("ExecuteSql: " + ex.Message);

                string jsonString = "[{\"ExceptionMessage\": \"" + ex.Message + "\"}]";
                object jobject = (object)JsonConvert.DeserializeObject(jsonString);
                result.Item = jobject;
                return result;
            }
        }

        /// <summary>
        /// Parse query for Sql
        /// </summary>
        /// <param name="executableTestDataStep">executableTestDataStep object</param>
        /// <returns>Sql command object for Execute Sql</returns>
        private SqlCommand ParseQueryForParameters(ExecutableTestData executableTestDataStep)
        {
            SqlCommand cmd = new SqlCommand();
            int i = 1;
            foreach (var item in executableTestDataStep.VariablesUsedInQuery)
            {
                string strToReplace = "{" + item.Name + "}";
                string strToPut = "@Param" + i++;
                if (executableTestDataStep.Value.IndexOf(strToReplace) > 0)
                {
                    executableTestDataStep.Value = executableTestDataStep.Value.Replace(strToReplace, strToPut);
                    cmd.Parameters.Add(strToPut, item.Value);
                }
            }

            cmd.CommandText = executableTestDataStep.Value;
            return cmd;
        }
    }
}
