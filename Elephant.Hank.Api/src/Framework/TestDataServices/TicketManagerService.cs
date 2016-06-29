// ---------------------------------------------------------------------------------------------------
// <copyright file="TicketManagerService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Mohammad Iliyash</author>
// <date>2016-06-22</date>
// <summary>
//     The TicketManagerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.TestDataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.TestDataServices;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Framework.Data;
    using Resources.Dto;
    using Resources.Dto.CustomIdentity;
    using Resources.Enum;
    using Resources.Extensions;
    using Resources.Json;
    using Resources.Messages;
    using Resources.Models;   

    /// <summary>
    /// The TicketManagerService class
    /// </summary>
    public class TicketManagerService : GlobalService<TblTicketMasterDto, TblTicketMaster>, ITicketManagerService
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
        /// Initializes a new instance of the <see cref="TicketManagerService"/> class.
        /// </summary>
        /// <param name="mapperFactory">The mapper factory.</param>
        /// <param name="table">The table.</param>
        /// <param name="userManager">The User Manager</param>
        public TicketManagerService(IMapperFactory mapperFactory, IRepository<TblTicketMaster> table, CustomUserManager userManager)
            : base(mapperFactory, table)
        {
            this.mapperFactory = mapperFactory;
            this.userManager = userManager;
        }
        
        /// <summary>
        /// Get allowed actions for sql steps
        /// </summary>
        /// <returns>TblTicketMasterDto list</returns>
        public ResultMessage<IEnumerable<TblTicketMasterDto>> GetActionForSqlTestStep()
        {
            var result = new ResultMessage<IEnumerable<TblTicketMasterDto>>();
            var entity = this.Table.Find(x => (x.Id == ActionConstants.Instance.LogTextActionId || x.Id == ActionConstants.Instance.SetVariableActionId) && x.IsDeleted != true).ToList();
            var mapper = this.mapperFactory.GetMapper<TblTicketMaster, TblTicketMasterDto>();
            result.Item = entity.Select(mapper.Map).ToList();
            return result;
        }

        /// <summary>
        /// Get Enum and Othe rrequired Data which is related to Tickets
        /// </summary>
        /// <returns>TblTicketMasterDto list</returns>
        public ResultMessage<TicketsData> GetTicketData()
        {
           var result = new ResultMessage<TicketsData> { Item = new TicketsData() };
            var userData = this.userManager.Users.Where(x => x.IsActive).Select(x => new CustomUserDto { FirstName = x.FirstName, Id = x.Id, LastName = x.LastName, UserName = x.UserName }).ToList();
            
              var ticketData = new TicketsData
                {
                    AssignedTo = userData.Select(x => new NameValueIntPair
                    {
                        Name = x.FullName,
                        Value = x.Id
                    }).ToList(),

                    Priority = new TicketPriority().ToNameValueIntList(),
                    Status = new TicketStatus().ToNameValueIntList(),
                    Type = new TicketType().ToNameValueIntList()
            };
            result.Item = ticketData;

            return result;
        }
    }
}
