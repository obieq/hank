// ---------------------------------------------------------------------------------------------------
// <copyright file="SharedTestDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-05</date>
// <summary>
//     The SharedTestDataService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Constants;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SharedTestDataService class
    /// </summary>
    public class SharedTestDataService : GlobalService<TblSharedTestDataDto, TblSharedTestData>, ISharedTestDataService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The apiTestDataService
        /// </summary>
        private readonly IApiTestDataService apiTestDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedTestDataService" /> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="apiTestDataService">The API test data service.</param>
        public SharedTestDataService(IMapperFactory mapperFactory, IRepository<TblSharedTestData> table, IApiTestDataService apiTestDataService)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.apiTestDataService = apiTestDataService;
        }

        /// <summary>
        /// Gets the test data by test case.
        /// </summary>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>TblSharedTestDataDto objects</returns>
        public ResultMessage<IEnumerable<TblSharedTestDataDto>> GetTestDataByTestCase(long testCaseId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDataDto>>();

            var entities = this.Table.Find(x => x.SharedTestId == testCaseId && x.IsDeleted != true).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblSharedTestData, TblSharedTestDataDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.ExecutionSequence);
            }

            return result;
        }

        /// <summary>
        /// Resets the execution sequence.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="sharedTestCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <param name="testDataNewSeq">The test data new seq.</param>
        /// <returns>
        /// execution status
        /// </returns>
        public ResultMessage<bool> ResetExecutionSequence(long userId, long sharedTestCaseId, long testDataId, long testDataNewSeq)
        {
            var result = new ResultMessage<bool>();

            var testData = this.GetById(testDataId);

            if (testData.IsError)
            {
                result.Messages.AddRange(testData.Messages);
            }
            else if (testData.Item.SharedTestId != sharedTestCaseId)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }

            if (testDataId == 0 || !result.IsError)
            {
                long existingSeqNum = testDataId == 0 ? 0 : testData.Item.ExecutionSequence;

                string updateQuery = "update \"TblSharedTestData\" as \"t1\" set \"ModifiedBy\"=" + userId + ", \"ModifiedOn\" = current_timestamp, \"ExecutionSequence\" = \"t2\".\"RowNum\" + "
                                     + "(" + existingSeqNum + "- 1)"
                                     + " from (select row_number() over(order by \"ExecutionSequence\") as \"RowNum\", * from \"TblSharedTestData\" "
                                     + " Where " + existingSeqNum + " > 0 and \"SharedTestId\" = " + sharedTestCaseId
                                     + " and \"ExecutionSequence\" > " + existingSeqNum + " and \"IsDeleted\" <> 't') \"t2\" "
                                     + " where \"t1\".\"Id\" = \"t2\".\"Id\";"

                                     + " update \"TblSharedTestData\" as \"t1\" set \"ModifiedBy\"=" + userId + ", \"ModifiedOn\" = current_timestamp, \"ExecutionSequence\" = \"t2\".\"RowNum\" + " + testDataNewSeq
                                     + " from (select row_number() over(order by \"ExecutionSequence\") as \"RowNum\", * from \"TblSharedTestData\" "
                                     + " Where " + testDataNewSeq + " > 0 and \"SharedTestId\" = " + sharedTestCaseId + " and \"Id\" <> " + testDataId
                                     + " and \"ExecutionSequence\" >= " + testDataNewSeq + " and \"IsDeleted\" <> 't') \"t2\" "
                                     + " where \"t1\".\"Id\" = \"t2\".\"Id\"";

                var entities = this.Table.ExecuteSqlCommand(updateQuery);

                if (entities == 0)
                {
                    result.Messages.Add(new Message(null, "There was no record!"));
                }
                else
                {
                    result.Item = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Add List of test data
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testDataList">list of test data to be added</param>
        /// <returns>
        /// TblSharedTestDataDto added List
        /// </returns>
        public ResultMessage<IEnumerable<TblSharedTestDataDto>> AddTestDataList(long userId, IEnumerable<TblSharedTestDataDto> testDataList)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDataDto>>();
            List<TblSharedTestDataDto> addedTestDataList = new List<TblSharedTestDataDto>();
            foreach (var item in testDataList)
            {
                var baseResult = this.SaveOrUpdate(item, userId);
                if (baseResult.IsError)
                {
                    foreach (var message in baseResult.Messages)
                    {
                        result.Messages.Add(new Message(message.Name, "At testData id " + item.Id + " " + message.Value));
                    }
                }
                else
                {
                    addedTestDataList.Add(baseResult.Item);
                }
            }

            result.Item = addedTestDataList;
            return result;
        }

        /// <summary>
        /// Get the Variable type test steps
        /// </summary>
        /// <param name="sharedTestCaseId">test case identifier</param>
        /// <returns>TblTestDataDto List object</returns>
        public ResultMessage<IEnumerable<TblSharedTestDataDto>> GetVariableTypeSharedTestDataBySharedTestCase(long sharedTestCaseId)
        {
            var result = new ResultMessage<IEnumerable<TblSharedTestDataDto>>();

            int decalareVariableActionId = AppSettings.Get(ConfigConstants.DbLogEntryFlag, 0);

            var entities = this.Table.Find(x => x.SharedTestId == sharedTestCaseId && x.IsDeleted != true && x.ActionId == decalareVariableActionId).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblSharedTestData, TblSharedTestDataDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.ExecutionSequence);
            }

            return result;
        }

        /// <summary>
        /// Saves the update custom.
        /// </summary>
        /// <param name="sharedTestDataDto">The shared test data dto.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Added/Updated TblSharedTestDataDto object
        /// </returns>
        public ResultMessage<TblSharedTestDataDto> SaveUpdateCustom(TblSharedTestDataDto sharedTestDataDto, long userId)
        {
            var result = new ResultMessage<TblSharedTestDataDto>();
            if (sharedTestDataDto.StepType == (int)LinkTestType.ApiTestStep && sharedTestDataDto.Id > 0)
            {
                var apiTestData = this.apiTestDataService.SaveOrUpdate(sharedTestDataDto.ApiTestData, userId);
                if (!apiTestData.IsError)
                {
                    result = this.SaveOrUpdate(sharedTestDataDto, userId);
                }
                else
                {
                    result.Messages.Add(new Message("Eror in updating child elements!!"));
                }
            }
            else
            {
                result = this.SaveOrUpdate(sharedTestDataDto, userId);
            }

            return result;
        }
    }
}
