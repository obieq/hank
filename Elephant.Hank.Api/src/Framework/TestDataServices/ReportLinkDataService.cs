// ---------------------------------------------------------------------------------------------------
// <copyright file="ReportLinkDataService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-12-23</date>
// <summary>
//     The ReportLinkDataService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// ReportLinkDataService class
    /// </summary>
    public class ReportLinkDataService : GlobalService<TblReportLinkDataDto, TblReportLinkData>, IReportLinkDataService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportLinkDataService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public ReportLinkDataService(
            IMapperFactory mapperFactory,
            IRepository<TblReportLinkData> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Adds the specified report link data dto.
        /// </summary>
        /// <param name="reportLinkDataDto">The report link data dto.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>added object</returns>
        public ResultMessage<TblReportLinkDataDto> Add(TblReportLinkDataDto reportLinkDataDto, long userId)
        {
            ResultMessage<TblReportLinkDataDto> result = new ResultMessage<TblReportLinkDataDto>();
            result.Item = this.mapperFactory.GetMapper<TblReportLinkData, TblReportLinkDataDto>().Map(this.Table.First(x => x.ReportId == reportLinkDataDto.ReportId && x.TestId == reportLinkDataDto.TestId));
            if (result.Item == null)
            {
                result = this.SaveOrUpdate(reportLinkDataDto, userId);
            }

            return result;
        }

        /// <summary>
        /// Gets the report link data.
        /// </summary>
        /// <param name="dayTillPastByDateCbx">if set to <c>true</c> [day till past by date CBX].</param>
        /// <param name="dayTillPast">The day till past.</param>
        /// <param name="testId">The test identifier.</param>
        /// <param name="dayTillPastDate">The day till past date.</param>
        /// <returns>
        /// Unused report data
        /// </returns>
        public ResultMessage<IEnumerable<TblReportLinkDataDto>> GetReportLinkData(bool dayTillPastByDateCbx, long dayTillPast, long testId, DateTime dayTillPastDate)
        {
            var result = new ResultMessage<IEnumerable<TblReportLinkDataDto>>();
            dayTillPastDate = dayTillPastByDateCbx ? dayTillPastDate.Date : DateTime.Now.Subtract(TimeSpan.FromDays(dayTillPast)).Date;
            Dictionary<string, object> dictionary = new Dictionary<string, object> { { "DayTillPastDate", dayTillPastDate }, { "TestId", testId } };
            var entities = this.Table.SqlQuery<TblReportLinkDataDto>("Select * from procgetreportlinkdata(@DayTillPastDate,@TestId);", dictionary).ToList();
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
