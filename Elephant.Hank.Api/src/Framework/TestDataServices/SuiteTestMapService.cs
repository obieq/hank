// ---------------------------------------------------------------------------------------------------
// <copyright file="SuiteTestMapService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The SuiteTestMapService class
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
    using Elephant.Hank.DataService.DBSchema.Linking;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The SuiteTestMapService class
    /// </summary>
    public class SuiteTestMapService : GlobalService<TblLnkSuiteTestDto, TblLnkSuiteTest>, ISuiteTestMapService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuiteTestMapService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public SuiteTestMapService(IMapperFactory mapperFactory, IRepository<TblLnkSuiteTest> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
        }

        /// <summary>
        /// Gets the link by suite identifier.
        /// </summary>
        /// <param name="suiteId">The suite identifier.</param>
        /// <returns>TblLnkSuiteTestDto objects</returns>
        public ResultMessage<IEnumerable<TblLnkSuiteTestDto>> GetLinkBySuiteId(long suiteId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSuiteTestDto>>();

            var suiteTestMap = this.Table.Find(x => x.SuiteId == suiteId && !x.IsDeleted).ToList();

            var mapper = this.mapperFactory.GetMapper<TblLnkSuiteTest, TblLnkSuiteTestDto>();

            result.Item = suiteTestMap.Select(mapper.Map);

            return result;
        }

        /// <summary>
        /// Gets the link by suite identifier and test identifier.
        /// </summary>
        /// <param name="suiteId">The suite identifier.</param>
        /// <param name="testCaseId">The test case identifier.</param>
        /// <returns>TblLnkSuiteTestDto object</returns>
        public ResultMessage<TblLnkSuiteTestDto> GetLinkBySuiteIdAndTestId(long suiteId, long testCaseId)
        {
            var result = new ResultMessage<TblLnkSuiteTestDto>();

            var suiteTestMap = this.Table.Find(x => x.SuiteId == suiteId && x.TestId == testCaseId && !x.IsDeleted).FirstOrDefault();

            var mapper = this.mapperFactory.GetMapper<TblLnkSuiteTest, TblLnkSuiteTestDto>();

            result.Item = mapper.Map(suiteTestMap);

            return result;
        }

        /// <summary>
        /// Saves the or update.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="suiteId">The suite identifier.</param>
        /// <param name="sourceData">The source data.</param>
        /// <returns>
        /// TblLnkSuiteTestDto objects
        /// </returns>
        public ResultMessage<IEnumerable<TblLnkSuiteTestDto>> SaveOrUpdate(long userId, long suiteId, List<TblLnkSuiteTestDto> sourceData)
        {
            sourceData.ForEach(
                x =>
                {
                    x.SuiteId = suiteId;
                    x.CreatedBy = userId;
                    x.CreatedOn = DateTime.Now;
                    x.ModifiedBy = userId;
                    x.ModifiedOn = DateTime.Now;
                });

            var suiteTestMap = this.Table.Find(x => x.SuiteId == suiteId).ToList();

            if (suiteTestMap.Count > 0)
            {
                suiteTestMap.Where(x => sourceData.All(src => src.TestId != x.TestId)).ToList().ForEach(x =>
                {
                    x.IsDeleted = true;
                    x.ModifiedBy = userId;
                    x.ModifiedOn = DateTime.Now;
                });
                suiteTestMap.Where(x => sourceData.Any(src => src.TestId == x.TestId)).ToList().ForEach(x =>
                {
                    x.IsDeleted = false;
                    x.ModifiedBy = userId;
                    x.ModifiedOn = DateTime.Now;
                });
            }

            var mapper = this.mapperFactory.GetMapper<TblLnkSuiteTestDto, TblLnkSuiteTest>();

            var newLinks = sourceData.Where(x => suiteTestMap.All(src => src.TestId != x.TestId)).Select(mapper.Map).ToList();

            newLinks.ForEach(x => this.Table.Insert(x));

            this.Table.Commit();

            return this.GetLinkBySuiteId(suiteId);
        }

        /// <summary>
        /// Gets the link by suite identifier list.
        /// </summary>
        /// <param name="suiteIdList">The suite identifier.</param>
        /// <returns>TblLnkSuiteTestDto objects</returns>
        public ResultMessage<IEnumerable<TblLnkSuiteTestDto>> GetLinksBySuiteIdList(string suiteIdList)
        {
            var result = new ResultMessage<IEnumerable<TblLnkSuiteTestDto>>();
            string[] strIdList = suiteIdList.Split(',');
            long[] longIdList = strIdList.Select(x => x.Trim() != string.Empty ? long.Parse(x) : 0).ToArray();
            var suiteTestMap = this.Table.Find(x => longIdList.Contains(x.SuiteId) && !x.IsDeleted).ToList();
            var mapper = this.mapperFactory.GetMapper<TblLnkSuiteTest, TblLnkSuiteTestDto>();
            result.Item = suiteTestMap.Select(mapper.Map);
            return result;
        }
    }
}
