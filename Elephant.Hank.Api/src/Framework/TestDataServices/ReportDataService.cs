// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-30</date>
// <summary>
//     The ReportDataService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.CustomIdentity;
    using Elephant.Hank.Resources.Enum;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Messages;
    using Elephant.Hank.Resources.Models;
    using Elephant.Hank.Resources.ViewModal;

    /// <summary>
    /// The ReportDataService service
    /// </summary>
    public class ReportDataService : GlobalService<TblReportDataDto, TblReportData>, IReportDataService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly CustomUserManager userManager;

        /// <summary>
        /// The browser service.
        /// </summary>
        private readonly IBrowserService browserService;

        /// <summary>
        /// The suite service.
        /// </summary>
        private readonly ISuiteService suiteService;

        /// <summary>
        /// The test service.
        /// </summary>
        private readonly ITestService testService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDataService" /> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="browserService">The browser service.</param>
        /// <param name="suiteService">The suite service.</param>
        /// <param name="testService">The test service.</param>
        public ReportDataService(
            IMapperFactory mapperFactory,
            IRepository<TblReportData> table,
            CustomUserManager userManager,
            IBrowserService browserService,
            ISuiteService suiteService,
            ITestService testService)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.userManager = userManager;
            this.browserService = browserService;
            this.suiteService = suiteService;
            this.testService = testService;
        }

        /// <summary>
        /// The get search criteria data by web site.
        /// </summary>
        /// <param name="websiteId">The website id.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// SearchCriteriaData object
        /// </returns>
        public async Task<ResultMessage<SearchCriteriaData>> GetSearchCriteriaDataByWebSite(long websiteId, long userId)
        {
            var result = new ResultMessage<SearchCriteriaData> { Item = new SearchCriteriaData() };

            var userData = this.userManager.Users.Where(x => x.IsActive).Select(x => new CustomUserDto { FirstName = x.FirstName, Id = x.Id, LastName = x.LastName, UserName = x.UserName }).ToList();

            result.Item.TestStatus = (new ExecutionReportStatus()).ToNameValueList();
            result.Item.Users = userData.Select(x => new NameValuePair { Name = x.FullName, Value = x.Id.ToString() }).ToList();

            var testCasesData = this.testService.GetByWebSiteId(websiteId, userId);
            if (!testCasesData.IsError)
            {
                result.Item.TestCases = testCasesData.Item.Select(x => new NameValuePair { Name = x.TestName, Value = x.Id.ToString() }).ToList();
            }

            var testSuiteData = this.suiteService.GetByWebsiteId(websiteId);
            if (!testSuiteData.IsError)
            {
                result.Item.Suites = testSuiteData.Item.Select(x => new NameValuePair { Name = x.Name, Value = x.Id.ToString() }).ToList();
            }

            var browserData = this.browserService.GetAll();
            if (!browserData.IsError)
            {
                result.Item.Browsers = browserData.Item.Select(x => new NameValuePair { Name = x.DisplayName, Value = x.ConfigName }).ToList();
                result.Item.OperationSystems = new List<NameValuePair> { new NameValuePair { Value = "XP", Name = "XP" } };

                foreach (var browser in browserData.Item.Where(browser => result.Item.OperationSystems.All(x => x.Name != browser.Platform)))
                {
                    result.Item.OperationSystems.Add(new NameValuePair { Value = browser.Platform, Name = browser.Platform });
                }
            }

            return result;
        }

        /// <summary>
        /// Get the search report data
        /// </summary>
        /// <param name="searchReportObject">the searchReportObject object</param>
        /// <returns>the ReportData object</returns>
        public ResultMessage<SearchReportResult> GetReportData(SearchReportObject searchReportObject)
        {
            var result = new ResultMessage<SearchReportResult>();

            int pageSize = searchReportObject.PageSize <= 0 ? 0 : searchReportObject.PageSize;
            long startAt = searchReportObject.PageNum <= 0 ? 0 : (searchReportObject.PageNum - 1) * pageSize;

            Dictionary<string, object> dictionary = new Dictionary<string, object>
                                                        {
                                                            {
                                                                "createdOn",
                                                                searchReportObject.CreatedOn
                                                            },
                                                            {
                                                                "websiteid",
                                                                searchReportObject.WebsiteId
                                                            },
                                                            {
                                                                "suiteid",
                                                                searchReportObject.SuiteId ?? 0
                                                            },
                                                            {
                                                                "testid",
                                                                searchReportObject.TestId ?? 0
                                                            },
                                                            {
                                                                "osname",
                                                                searchReportObject.OsName.IsBlank() ? null : searchReportObject.OsName
                                                            },
                                                            {
                                                                "browser",
                                                                searchReportObject.Browser.IsBlank() ? null : searchReportObject.Browser
                                                            },
                                                            {
                                                                "teststatus",
                                                                searchReportObject.TestStatus.HasValue ? (int)searchReportObject.TestStatus : 0
                                                            },
                                                            {
                                                                "userid",
                                                                searchReportObject.UserId.HasValue ? (int)searchReportObject.UserId : 0
                                                            },
                                                            {
                                                                "startat",
                                                                startAt
                                                            },
                                                            {
                                                                "pagesize",
                                                                pageSize
                                                            },
                                                            {
                                                                "extraData",
                                                                searchReportObject.ExtraData
                                                            },
                                                            {
                                                                "executiongroup",
                                                                searchReportObject.ExecutionGroup.IsBlank() ? null : searchReportObject.ExecutionGroup
                                                            }
                                                        };

            var entities = this.Table.SqlQuery<TblReportDataDto>("Select * from procsearchreportv2(@createdOn, @websiteid, @suiteid, @testid, @osname, @browser, @teststatus, @userid, @startat, @pagesize, @extraData, @executiongroup);", dictionary).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = new SearchReportResult { Data = entities };

                var resultStart = entities.First();

                result.Total = resultStart.Count;

                result.Item.CountPassed = resultStart.CountPassed;
                result.Item.CountFailed = resultStart.CountFailed;

                result.PageSize = pageSize == 0 ? result.Total : pageSize;

                result.StartedAt = startAt;
            }

            return result;
        }

        /// <summary>
        /// Get the ReportData By Id
        /// </summary>
        /// <param name="id">report identifier</param>
        /// <returns>TblReportDataDto object</returns>
        public ResultMessage<TblReportDataDto> GetReportDataById(long id)
        {
            var result = new ResultMessage<TblReportDataDto>();
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "id", id } };

            var entities = this.Table.SqlQuery<TblReportDataDto>("Select * from procgetreportbyid(@id);", dictionary).FirstOrDefault();

            if (entities == null)
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
        /// Gets the group status.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>GroupStatusReport object</returns>
        public ResultMessage<GroupStatusReport> GetExecutionGroupStatus(string groupName)
        {
            var result = new ResultMessage<GroupStatusReport>();
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "groupName", groupName } };

            var entities = this.Table.SqlQuery<GroupStatusReportData>("Select * from procGetGroupStatus(@groupName);", dictionary).ToList();

            if (entities.Any())
            {
                var firstRec = entities.First();
                result.Item = new GroupStatusReport
                                  {
                                      IsComplete = firstRec.IsComplete,
                                      ProcessedCount = firstRec.ProcessedCount,
                                      TestCount = firstRec.TestCount
                                  };

                foreach (var entity in entities)
                {
                    result.Item.CountByStatus.Add(new NameValuePair { Name = entity.ExecutionStatus.ToString(), Value = entity.StatusCount.ToString() });
                }
            }
            else
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }

            return result;
        }

        /// <summary>
        /// Get Report Data by Group Name
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>
        /// TblReportDataDto objects
        /// </returns>
        public ResultMessage<IEnumerable<TblReportDataDto>> GetByGroupName(string groupName)
        {
            var result = new ResultMessage<IEnumerable<TblReportDataDto>>();

            var entities = this.Table.Find(x => x.ExecutionGroup == groupName && x.IsDeleted != true).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblReportData, TblReportDataDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.Description);
            }

            return result;
        }

        /// <summary>
        /// Get Report Data by Group Name
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>
        /// TblReportDataDto objects
        /// </returns>
        public ResultMessage<TblReportDataDto> GetByGroupNameWhereScreenShotArrayExist(string groupName)
        {
            var result = new ResultMessage<TblReportDataDto>();
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "executiongroup", groupName } };

            var entities = this.Table.SqlQuery<TblReportDataDto>("select * from getbygroupnamewherescreenshotarrayexist(@executiongroup);", dictionary).FirstOrDefault();

            if (entities == null)
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
        /// get all unprocessed data item for group
        /// </summary>
        /// <param name="groupName">group identifier</param>
        /// <returns>list of report data</returns>
        public ResultMessage<IEnumerable<TblReportDataDto>> GetAllUnprocessedForGroup(string groupName)
        {
            var result = new ResultMessage<IEnumerable<TblReportDataDto>>();
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "executiongroup", groupName } };
            var entities = this.Table.SqlQuery<TblReportDataDto>("Select * from procgetallunprocessedforgroup(@executiongroup);", dictionary).ToList();
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
