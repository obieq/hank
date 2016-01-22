// ---------------------------------------------------------------------------------------------------
// <copyright file="TestDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TestDataService class
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
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TestDataService class
    /// </summary>
    public class TestDataService : GlobalService<TblTestDataDto, TblTestData>, ITestDataService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The testDataSharedTestDataMapService
        /// </summary>
        private readonly ITestDataSharedTestDataMapService testDataSharedTestDataMapService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDataService" /> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="testDataSharedTestDataMapService">The testDataSharedTestDataMapService.</param>
        public TestDataService(IMapperFactory mapperFactory, IRepository<TblTestData> table, ITestDataSharedTestDataMapService testDataSharedTestDataMapService)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.testDataSharedTestDataMapService = testDataSharedTestDataMapService;
        }

        /// <summary>
        /// Gets the test data by test case.
        /// </summary>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>TblTestDataDto objects</returns>
        public ResultMessage<IEnumerable<TblTestDataDto>> GetTestDataByTestCase(long testCaseId)
        {
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();

            var entities = this.Table.Find(x => x.TestId == testCaseId && x.IsDeleted != true).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblTestData, TblTestDataDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.ExecutionSequence);
            }

            return result;
        }

        /// <summary>
        /// Resets the execution sequence.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <param name="testDataId">The test data identifier.</param>
        /// <param name="testDataNewSeq">The test data new seq.</param>
        /// <returns>
        /// execution status
        /// </returns>
        public ResultMessage<bool> ResetExecutionSequence(long userId, long testCaseId, long testDataId, long testDataNewSeq)
        {
            var result = new ResultMessage<bool>();

            var testData = this.GetById(testDataId);

            if (testData.IsError)
            {
                result.Messages.AddRange(testData.Messages);
            }
            else if (testData.Item.TestId != testCaseId)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }

            if (testDataId == 0 || !result.IsError)
            {
                long existingSeqNum = testDataId == 0 ? 0 : testData.Item.ExecutionSequence;

                string updateQuery = "update \"TblTestData\" as \"t1\" set \"ModifiedBy\"=" + userId + ", \"ModifiedOn\" = current_timestamp, \"ExecutionSequence\" = \"t2\".\"RowNum\" + "
                                     + "(" + existingSeqNum + "- 1)"
                                     + " from (select row_number() over(order by \"ExecutionSequence\") as \"RowNum\", * from \"TblTestData\" "
                                     + " Where " + existingSeqNum + " > 0 and \"TestId\" = " + testCaseId
                                     + " and \"ExecutionSequence\" > " + existingSeqNum + " and \"IsDeleted\" <> 't') \"t2\" "
                                     + " where \"t1\".\"Id\" = \"t2\".\"Id\";"

                                     + " update \"TblTestData\" as \"t1\" set \"ModifiedBy\"=" + userId + ", \"ModifiedOn\" = current_timestamp, \"ExecutionSequence\" = \"t2\".\"RowNum\" + " + testDataNewSeq
                                     + " from (select row_number() over(order by \"ExecutionSequence\") as \"RowNum\", * from \"TblTestData\" "
                                     + " Where " + testDataNewSeq + " > 0 and \"TestId\" = " + testCaseId + " and \"Id\" <> " + testDataId
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
        /// TblTestDataDto added List
        /// </returns>
        public ResultMessage<IEnumerable<TblTestDataDto>> AddTestDataList(long userId, IEnumerable<TblTestDataDto> testDataList)
        {
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();
            List<TblTestDataDto> addedTestDataList = new List<TblTestDataDto>();
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
        /// Save Test data with shared steps
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testData">TblTestDataDto object</param>
        /// <returns>
        /// List object TblTestDataDto
        /// </returns>
        public ResultMessage<TblTestDataDto> SaveOrUpdateWithSharedTest(long userId, TblTestDataDto testData)
        {
            var result = new ResultMessage<TblTestDataDto>();

            if (testData.LinkTestType == (int)LinkTestType.SharedTest)
            {
                testData.SharedTest = null;
                List<TblLnkTestDataSharedTestDataDto> sharedTestSteps = testData.SharedTestSteps;
                testData.SharedTestSteps = null;
                result = this.SaveOrUpdate(testData, userId);
                testData = result.Item;
                if (sharedTestSteps.Count > 0)
                {
                    var sharedTestResult = this.testDataSharedTestDataMapService.SaveOrUpdate(userId, testData.Id, sharedTestSteps);
                    result.Item.SharedTestSteps = sharedTestResult.Item.ToList();
                }

                return result;
            }

            result.Messages.Add(new Message("Invalid Data", "Invalid data "));
            return result;
        }

        /// <summary>
        /// Copy the test steps from one test to another
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="copyTestDataModel">copyTestDataModel Object</param>
        /// <returns>
        /// TblTestDataDto List object
        /// </returns>
        public bool CopyTestData(long userId, CopyTestDataModel copyTestDataModel)
        {
            var result = new ResultMessage<IEnumerable<TblTestDataDto>>();
            Dictionary<string, object> dictionary = new Dictionary<string, object>
                                                    {
                                                        { "fromtestid", copyTestDataModel.FromTestId },
                                                        { "totestid", copyTestDataModel.ToTestId },
                                                        { "createdby", userId }
                                                    };

            if (copyTestDataModel.CopyAll)
            {
                dictionary.Add("testdataids", string.Empty);
                dictionary.Add("copycompletetest", true);
            }
            else
            {
                string paramStrToSend = string.Empty;
                int i = 0;
                foreach (var item in copyTestDataModel.TestDataIdList)
                {
                    if (i == 0)
                    {
                        paramStrToSend = item.ToString();
                        i++;
                    }
                    else
                    {
                        paramStrToSend = paramStrToSend + "," + item.ToString();
                    }
                }

                dictionary.Add("testdataids", paramStrToSend);
                dictionary.Add("copycompletetest", false);
            }

            var mapper = this.mapperFactory.GetMapper<TblTestData, TblTestDataDto>();
            this.Table.SqlQuery<TblTestDataDto>("Select * from proccopytestdata(@fromtestid,@totestid,@createdby,@testdataids,@copycompletetest);", dictionary).ToList();
            return true;
        }

        /// <summary>
        /// Get the Variable type test steps
        /// </summary>
        /// <param name="testCaseId">test case identifier</param>
        /// <returns>TblTestDataDto List object</returns>
        public ResultMessage<IEnumerable<ProtractorVariableModel>> GetVariableTypeTestDataByTestCase(long testCaseId)
        {
            var result = new ResultMessage<IEnumerable<ProtractorVariableModel>>();

            int decalareVariableActionId = AppSettings.Get(ConfigConstants.DeclareVariableActionId, 0);

            Dictionary<string, object> dictionary = new Dictionary<string, object>
                                                    {
                                                        { "id", testCaseId },
                                                        { "declarevariableid", decalareVariableActionId }
                                                    };
            var entities = this.Table.SqlQuery<ProtractorVariableModel>("Select * from procgetdeclaredvariable(@id,@declarevariableid);", dictionary).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = entities;
            }

            return result;
        }

        /// <summary>
        /// Get all variable related to test for auto complete help
        /// </summary>
        /// <param name="testId">the test identifier</param>
        /// <returns>list of variable name</returns>
        public ResultMessage<IEnumerable<string>> GetAllVariableByTestIdForAutoComplete(long testId)
        {
            var result = new ResultMessage<IEnumerable<string>>();
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "testid", testId } };

            var entities = this.Table.SqlQuery<string>("Select * from procgetallvariablerelatedtotest(@testid);", dictionary).ToList();

            if (!entities.Any())
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