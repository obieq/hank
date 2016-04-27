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

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
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
        /// Initializes a new instance of the <see cref="ReportDataService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public ReportDataService(IMapperFactory mapperFactory, IRepository<TblReportData> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Get the search report data
        /// </summary>
        /// <param name="searchReportObject">the searchReportObject object</param>
        /// <returns>the ReportData object</returns>
        public ResultMessage<IEnumerable<TblReportDataDto>> GetReportData(SearchReportObject searchReportObject)
        {
            var result = new ResultMessage<IEnumerable<TblReportDataDto>>();

            Dictionary<string, object> dictionary = new Dictionary<string, object>
                                                        {
                                                            {
                                                                "createdOn",
                                                                searchReportObject.CreatedOn
                                                            },
                                                            {
                                                                "executiongroup",
                                                                searchReportObject.ExecutionGroup.IsNotBlank() ? searchReportObject.ExecutionGroup : null
                                                            },
                                                            {
                                                                "websiteid",
                                                                searchReportObject.WebsiteId
                                                            }
                                                        };

            var entities = this.Table.SqlQuery<TblReportDataDto>("Select * from procsearchreport(@createdOn,@websiteid,@executiongroup);", dictionary).ToList();

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
