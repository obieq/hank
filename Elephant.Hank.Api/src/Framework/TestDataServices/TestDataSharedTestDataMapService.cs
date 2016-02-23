// ---------------------------------------------------------------------------------------------------
// <copyright file="TestDataSharedTestDataMapService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-06-09</date>
// <summary>
//     The TestDataSharedTestDataMapService class
// </summary>

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
    /// the TestDataSharedTestDataMapService class
    /// </summary>
    public class TestDataSharedTestDataMapService : GlobalService<TblLnkTestDataSharedTestDataDto, TblLnkTestDataSharedTestData>, ITestDataSharedTestDataMapService
    {
        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblLnkTestDataSharedTestData> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDataSharedTestDataMapService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        public TestDataSharedTestDataMapService(IMapperFactory mapperFactory, IRepository<TblLnkTestDataSharedTestData> table)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.table = table;
        }

        /// <summary>
        /// get all shared test step by TestDataId
        /// </summary>
        /// <param name="testDataId">the testData identifier</param>
        /// <returns>TblLnkTestDataSharedTestDataDto object</returns>
        public ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>> GetByTestDataId(long testDataId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>>();

            var entities = this.table.Find(x => x.TestDataId == testDataId && x.IsDeleted != true).ToList();

            if (!entities.Any())
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                var mapper = this.mapperFactory.GetMapper<TblLnkTestDataSharedTestData, TblLnkTestDataSharedTestDataDto>();
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.Id);
            }

            return result;
        }

        /// <summary>
        /// Save Or Update the TestDataSharedTestData steps
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="testDataId">the testData identifier</param>
        /// <param name="sourceData">List of TblLnkTestDataSharedTestDataDto</param>
        /// <returns>
        /// TblLnkTestDataSharedTestDataDto object list
        /// </returns>
        public ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>> SaveOrUpdate(long userId, long testDataId, List<TblLnkTestDataSharedTestDataDto> sourceData)
        {
            sourceData.ForEach(
                x =>
                {
                    x.TestDataId = testDataId;
                    x.CreatedBy = userId;
                    x.ModifiedBy = userId;
                    x.ModifiedOn = DateTime.Now;
                });

            var testDataSharedTestDataMap = this.table.Find(x => x.TestDataId == testDataId).ToList();

            if (testDataSharedTestDataMap.Count > 0)
            {
                testDataSharedTestDataMap.Where(x => sourceData.All(src => src.SharedTestDataId != x.SharedTestDataId)).ToList().ForEach(x => x.IsDeleted = true);

                testDataSharedTestDataMap.Where(x => sourceData.Any(src => src.SharedTestDataId == x.SharedTestDataId)).ToList().ForEach(x => x.IsDeleted = false);

                foreach (var item in testDataSharedTestDataMap)
                {
                    var updatedObject = sourceData.FirstOrDefault(x => x.Id == item.Id);
                    if (updatedObject != null)
                    {
                        item.NewOrder = updatedObject.NewOrder;
                        item.IsIgnored = updatedObject.IsIgnored;
                        item.NewValue = updatedObject.NewValue;
                        item.ModifiedBy = userId;
                        item.ModifiedOn = DateTime.Now;
                    }
                }
            }

            var mapper = this.mapperFactory.GetMapper<TblLnkTestDataSharedTestDataDto, TblLnkTestDataSharedTestData>();

            var newLinks = sourceData.Where(x => testDataSharedTestDataMap.All(src => src.SharedTestDataId != x.SharedTestDataId)).Select(mapper.Map).ToList();

            newLinks.ForEach(x => this.table.Insert(x));

            this.table.Commit();

            return this.GetByTestDataId(testDataId);
        }

        /// <summary>
        /// delete all entries in table by sharedTestDataId
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="sharedTestDataId">shared test identifier</param>
        /// <returns>
        /// List of all deleted TblLnkTestDataSharedTestDataDto
        /// </returns>
        public ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>> DeleteBySharedTestDataId(long userId, long sharedTestDataId)
        {
            var result = new ResultMessage<IEnumerable<TblLnkTestDataSharedTestDataDto>>();
            var entities = this.table.Find(m => m.SharedTestDataId == sharedTestDataId).ToList();
            var mapper = this.mapperFactory.GetMapper<TblLnkTestDataSharedTestData, TblLnkTestDataSharedTestDataDto>();
            if (entities.Any())
            {
                result.Item = entities.Select(mapper.Map).OrderBy(x => x.Id);
                entities.ToList().ForEach(
                    x =>
                    {
                        x.IsDeleted = true;
                        x.ModifiedBy = userId;
                        x.ModifiedOn = DateTime.Now;
                    });
                this.table.Commit();
            }
            else
            {
                result.Messages.Add(new Message(null, "Record to delete not found!"));
            }

            return result;
        }

        /// <summary>
        /// Gets the by test data identifier and shared test data identifier.
        /// </summary>
        /// <param name="testDataId">The test data identifier.</param>
        /// <param name="sharedTestDataId">The shared test data identifier.</param>
        /// <returns>link data</returns>
        public ResultMessage<TblLnkTestDataSharedTestDataDto> GetByTestDataIdAndSharedTestDataId(long testDataId, long sharedTestDataId)
        {
            var result = new ResultMessage<TblLnkTestDataSharedTestDataDto>();

            var entity = this.table.Find(x => x.TestDataId == testDataId && x.SharedTestDataId == sharedTestDataId && x.IsDeleted != true).FirstOrDefault();

            if (entity == null)
            {
                result.Messages.Add(new Message(null, "Record not found!"));
            }
            else
            {
                result.Item = this.mapperFactory.GetMapper<TblLnkTestDataSharedTestData, TblLnkTestDataSharedTestDataDto>().Map(entity);
            }

            return result;
        }
    }
}
