// ---------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The DefaultRegistry class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.DependencyResolution
{
    using System.Data.Entity;

    using Elephant.Hank.Api.Areas.HelpPage.Controllers;
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.Common.Mapper;
    using Elephant.Hank.Common.Report;
    using Elephant.Hank.DataService;
    using Elephant.Hank.Framework.Data;
    using Elephant.Hank.Framework.Mapper;
    using Elephant.Hank.Framework.Report;
    using Elephant.Hank.Resources.Constants;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    /// <summary>
    /// The DefaultRegistry class
    /// </summary>
    public class DefaultRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRegistry"/> class.
        /// </summary>
        public DefaultRegistry()
        {
            this.Scan(
                            scan =>
                            {
                                scan.TheCallingAssembly();
                                scan.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("Elephant"));
                                scan.WithDefaultConventions();
                            });

            this.For(typeof(DbContext)).Use(typeof(AuthContext));
            this.For(typeof(IRepository<>)).Use(typeof(Repository<>));

            this.For(typeof(IUserStore<>)).Use(typeof(UserStore<>));
            this.For(typeof(UserManager<>)).Singleton().Use(typeof(UserManager<>));

            this.For<IAuthRepository>().Use<AuthRepository>();

            this.For<ILoggerService>().Singleton().Use<LoggerService>();

            this.For<IMapperFactory>().Singleton().Use<MapperFactory>();

            this.For<IFileFolderHelper>().Use<FileFolderHelper>()
                .Ctor<string>("baseFolder")
                .Is(AppSettings.Get(ConfigConstants.ReportLocation, string.Empty))
                .Singleton();

            this.For<HelpController>().Use(ctx => new HelpController());
        }
    }
}