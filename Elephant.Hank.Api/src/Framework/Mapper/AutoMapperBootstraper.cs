// ---------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperBootstraper.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The AutoMapperBootstraper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Mapper
{
    using System.Linq;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.DataService.DBSchema.Linking;
    using Elephant.Hank.Framework.Mapper.TypeConverters;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Dto.CustomIdentity;
    using Elephant.Hank.Resources.Dto.InternalDtos;
    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Extensions;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The auto mapper boot straper.
    /// </summary>
    public class AutoMapperBootstraper
    {
        /// <summary>
        /// Gets a value indicating whether is initialize.
        /// </summary>
        public static bool IsInitialize { get; private set; }

        /// <summary>
        /// The initialize.
        /// </summary>
        public static void Initialize()
        {
            if (IsInitialize)
            {
                return;
            }

            IsInitialize = true;

            MappingLookupDbToDto();
        }

        /// <summary>
        /// The mapping lookup Db to dto.
        /// </summary>
        private static void MappingLookupDbToDto()
        {
            AutoMapper.Mapper.CreateMap<TblDataBaseCategories, TblDataBaseCategoriesDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblDataBaseConnection, TblDataBaseConnectionDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.DataBaseCategories != null ? src.DataBaseCategories.Name : null))
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblDataBaseConnection, InternalTblDataBaseConnectionDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ForMember(
                    x => x.CategoryName,
                    opt => opt.MapFrom(src => src.DataBaseCategories != null ? src.DataBaseCategories.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblApiCategories, TblApiCategoriesDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblApiConnection, TblApiConnectionDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.ApiCategories != null ? src.ApiCategories.Name : null))
                .ForMember(x => x.EnvironmentName, opt => opt.MapFrom(src => src.Environment != null ? src.Environment.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<CustomUser, CustomUserDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<CustomUserLogin, CustomUserLoginDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<CustomUserRole, CustomUserRoleDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<CustomUserClaim, CustomUserClaimDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<CustomRole, CustomRoleDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblAuthClients, ClientDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TblRefreshAuthTokens, RefreshTokenDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblLnkSuiteTest, TblLnkSuiteTestDto>()
                .ForMember(x => x.TestName, opt => opt.MapFrom(src => src.Test != null ? src.Test.TestName : null))
                 .ForMember(x => x.SuiteName, opt => opt.MapFrom(src => src.Suite != null ? src.Suite.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblAction, TblActionDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TblLocator, TblLocatorDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TblSuite, TblSuiteDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TblWebsite, TblWebsiteDto>().ReverseMap();
            AutoMapper.Mapper.CreateMap<TblTicketMaster, TblTicketMasterDto>()
                .ForMember(x => x.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.FullName : null))
                .ForMember(x => x.AssignedToUserName, opt => opt.MapFrom(src => src.AssignedToUser != null ? src.AssignedToUser.FullName : null))
                .ReverseMap();
            AutoMapper.Mapper.CreateMap<TblTicketHistory, TblTicketHistoryDto>()
                .ForMember(x => x.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.FullName : null)).ReverseMap();
            AutoMapper.Mapper.CreateMap<TblTicketMasterDto, TblTicketHistoryDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblTicketComment, TblTicketCommentDto>()
               .ForMember(x => x.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.FullName : null))
               .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblTestData, TblTestDataDto>()
                .ForMember(x => x.TestName, opt => opt.MapFrom(src => src.Test != null ? src.Test.TestName : null))
                .ForMember(x => x.DisplayNameValue, opt => opt.MapFrom(src => src.LocatorIdentifier != null ? src.LocatorIdentifier.DisplayName : null))
                .ForMember(x => x.PageId, opt => opt.MapFrom(src => src.LocatorIdentifier != null ? src.LocatorIdentifier.PageId : 0))
                .ForMember(x => x.ActionValue, opt => opt.MapFrom(src => src.Action != null ? src.Action.Value : null))
                .ForMember(x => x.LocatorIdentifierValue, opt => opt.MapFrom(src => src.LocatorIdentifier != null ? src.LocatorIdentifier.Value : null))
                .ForMember(x => x.LocatorValue, opt => opt.MapFrom(src => src.LocatorIdentifier != null && src.LocatorIdentifier.Locator != null ? src.LocatorIdentifier.Locator.Value : null))
                .ForMember(x => x.SharedStepWebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ForMember(x => x.SharedStepWebsiteTestName, opt => opt.MapFrom(src => src.SharedStepWebsiteTest != null ? src.SharedStepWebsiteTest.TestName : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblTestDataDto, ExecutableTestData>().ConvertUsing<TblTestDataExecutableTestDataConverter>();

            AutoMapper.Mapper.CreateMap<TblTest, TblTestDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(x => x.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser != null ? src.CreatedByUser.FullName : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblPages, TblPagesDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblLocatorIdentifier, TblLocatorIdentifierDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Page != null && src.Page.Website != null ? src.Page.Website.Name : null))
                .ForMember(x => x.PageName, opt => opt.MapFrom(src => src.Page != null ? src.Page.Value : null))
                .ForMember(x => x.LocatorValue, opt => opt.MapFrom(src => src.Locator != null ? src.Locator.Value : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblScheduler, TblSchedulerDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ForMember(x => x.UrlList, opt => opt.MapFrom(src => src.Website != null ? src.Website.WebsiteUrlList : null))
                 .ForMember(x => x.LastUpdatedBy, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblLnkSchedulerSuite, TblLnkSchedulerSuiteDto>()
                .ForMember(x => x.SuiteName, opt => opt.MapFrom(src => src.Suite != null ? src.Suite.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblSchedulerHistory, TblSchedulerHistoryDto>()
                .ForMember(x => x.SchedulerName, opt => opt.MapFrom(src => src.Scheduler != null ? src.Scheduler.Name : null))
                .ForMember(x => x.StatusText, opt => opt.MapFrom(src => src.Status.GetAttributeText()))
                .ForMember(x => x.EmailStatusText, opt => opt.MapFrom(src => src.EmailStatus.GetAttributeText()))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblTestQueue, TblTestQueueDto>()
                .ForMember(x => x.TestName, opt => opt.MapFrom(src => src.TestCase != null ? src.TestCase.TestName : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblReportData, TblReportDataDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblSharedTestData, TblSharedTestDataDto>()
                 .ForMember(x => x.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.FirstName))
               .ForMember(x => x.TestName, opt => opt.MapFrom(src => src.SharedTest != null ? src.SharedTest.TestName : null))
               .ForMember(x => x.DisplayNameValue, opt => opt.MapFrom(src => src.LocatorIdentifier != null ? src.LocatorIdentifier.DisplayName : null))
               .ForMember(x => x.PageId, opt => opt.MapFrom(src => src.LocatorIdentifier != null ? src.LocatorIdentifier.PageId : 0))
               .ForMember(x => x.ActionValue, opt => opt.MapFrom(src => src.Action != null ? src.Action.Value : null))
               .ForMember(x => x.LocatorIdentifierValue, opt => opt.MapFrom(src => src.LocatorIdentifier != null ? src.LocatorIdentifier.Value : null))
               .ForMember(x => x.LocatorValue, opt => opt.MapFrom(src => src.LocatorIdentifier != null && src.LocatorIdentifier.Locator != null ? src.LocatorIdentifier.Locator.Value : null))
               .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblSharedTest, TblSharedTestDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ForMember(x => x.SharedTestDataList, opt => opt.MapFrom(src => src.SharedTestDataList != null ? src.SharedTestDataList.Where(m => m.IsDeleted == false) : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblBrowsers, TblBrowsersDto>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblLnkTestDataSharedTestData, TblLnkTestDataSharedTestDataDto>()
                .ForMember(x => x.ModifiedByUserName, opt => opt.MapFrom(src => src.ModifiedByUser.FirstName))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblSharedTestData, TblTestData>();

            AutoMapper.Mapper.CreateMap<TblSharedTestDataDto, TblTestDataDto>()
                .ForMember(x => x.LinkTestType, opt => opt.MapFrom(src => src.StepType < 0 ? 0 : src.StepType))
                .ForMember(x => x.SharedTestDataId, opt => opt.MapFrom(src => src.Id < 0 ? 0 : src.Id))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblEnvironmentDto, TblEnvironment>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblDbLog, TblDbLogDto>()
                .ForMember(x => x.LastUpdatedBy, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblTestCategories, TblTestCategoriesDto>()
                .ForMember(x => x.WebsiteName, opt => opt.MapFrom(src => src.Website != null ? src.Website.Name : null))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblUserProfileDto, TblUserProfile>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblGroup, TblGroupDto>()
                .ForMember(x => x.ModifiedByUserName, opt => opt.MapFrom(x => x.ModifiedByUser != null ? x.ModifiedByUser.FullName : string.Empty));

            AutoMapper.Mapper.CreateMap<TblGroupDto, TblGroup>();

            AutoMapper.Mapper.CreateMap<TblGroupUser, TblGroupUserDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null));

            AutoMapper.Mapper.CreateMap<TblGroupUserDto, TblGroupUser>()
                .ForMember(x => x.User, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<TblModuleDto, TblModule>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblGroupModuleAccess, TblGroupModuleAccessDto>()
                 .ForMember(x => x.ModuleName, opt => opt.MapFrom(x => x.Module != null ? x.Module.ModuleName : string.Empty))
                  .ForMember(x => x.IsModuleExecutable, opt => opt.MapFrom(x => x.Module != null && x.Module.IsExecutable))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblHashTagDescriptionDto, TblHashTagDescription>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblRequestTypesDto, TblRequestTypes>().ReverseMap();

            AutoMapper.Mapper.CreateMap<TblApiTestData, TblApiTestDataDto>()
                .ForMember(x => x.RequestName, opt => opt.MapFrom(x => x.RequestTypes != null ? x.RequestTypes.Name : string.Empty))
                .ReverseMap();

            AutoMapper.Mapper.CreateMap<TblRequestTypes, TblRequestTypesDto>()
               .ReverseMap();
        }
    }
}