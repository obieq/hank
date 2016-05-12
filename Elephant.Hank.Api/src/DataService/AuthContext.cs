// ---------------------------------------------------------------------------------------------------
// <copyright file="AuthContext.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The AuthContext class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Reflection;

    using Elephant.Hank.Common.Helper;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.DataService.DBSchema.CustomIdentity;
    using Elephant.Hank.DataService.DBSchema.Linking;
    using Elephant.Hank.Resources.Attributes;
    using Elephant.Hank.Resources.Constants;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;

    /// <summary>
    /// The AuthContext class
    /// </summary>
    public class AuthContext : IdentityDbContext<CustomUser, CustomRole, long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        /// <summary>
        /// The default schema
        /// </summary>
        private const string DefaultSchema = "public";

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthContext"/> class.
        /// </summary>
        public AuthContext()
            : base(ConfigConstants.ConnectionStringKey)
        {
        }

        /// <summary>
        /// Gets or sets the clients.
        /// </summary>
        public DbSet<TblAuthClients> Clients { get; set; }

        /// <summary>
        /// Gets or sets the refresh tokens.
        /// </summary>
        public DbSet<TblRefreshAuthTokens> RefreshTokens { get; set; }

        /// <summary>
        /// Gets or sets the table LNK suite test.
        /// </summary>
        public DbSet<TblLnkSuiteTest> TblLnkSuiteTest { get; set; }

        /// <summary>
        /// Gets or sets the table action.
        /// </summary>
        public DbSet<TblAction> TblAction { get; set; }

        /// <summary>
        /// Gets or sets the table pages.
        /// </summary>
        public DbSet<TblPages> TblPages { get; set; }

        /// <summary>
        /// Gets or sets the table locator.
        /// </summary>
        public DbSet<TblLocator> TblLocator { get; set; }

        /// <summary>
        /// Gets or sets the table locator identifier.
        /// </summary>
        public DbSet<TblLocatorIdentifier> TblLocatorIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the table suite.
        /// </summary>
        public DbSet<TblSuite> TblSuite { get; set; }

        /// <summary>
        /// Gets or sets the table test.
        /// </summary>
        public DbSet<TblTest> TblTest { get; set; }

        /// <summary>
        /// Gets or sets the table test data.
        /// </summary>
        public DbSet<TblTestData> TblTestData { get; set; }

        /// <summary>
        /// Gets or sets the table report data.
        /// </summary>
        public DbSet<TblReportData> TblReportData { get; set; }

        /// <summary>
        /// Gets or sets the table website.
        /// </summary>
        public DbSet<TblWebsite> TblWebsite { get; set; }

        /// <summary>
        /// Gets or sets the table logger.
        /// </summary>
        public DbSet<TblLogger> TblLogger { get; set; }

        /// <summary>
        /// Gets or sets the table test queue.
        /// </summary>
        public DbSet<TblTestQueue> TblTestQueue { get; set; }

        /// <summary>
        /// Gets or sets the table scheduler.
        /// </summary>
        public DbSet<TblScheduler> TblScheduler { get; set; }

        /// <summary>
        /// Gets or sets the table scheduler history.
        /// </summary>
        public DbSet<TblSchedulerHistory> TblSchedulerHistory { get; set; }

        /// <summary>
        /// Gets or sets the table LNK scheduler suite.
        /// </summary>
        public DbSet<TblLnkSchedulerSuite> TblLnkSchedulerSuite { get; set; }

        /// <summary>
        /// Gets or sets the table shared test.
        /// </summary>
        public DbSet<TblSharedTest> TblSharedTest { get; set; }

        /// <summary>
        /// Gets or sets the table shared test data.
        /// </summary>
        public DbSet<TblSharedTestData> TblSharedTestData { get; set; }

        /// <summary>
        /// Gets or sets the table TblLnkTestDataSharedTestData.
        /// </summary>
        public DbSet<TblLnkTestDataSharedTestData> TblLnkTestDataSharedTestData { get; set; }

        /// <summary>
        /// Gets or sets the table browsers.
        /// </summary>
        public DbSet<TblBrowsers> TblBrowsers { get; set; }

        /// <summary>
        /// Gets or sets the table browsers.
        /// </summary>
        public DbSet<TblEnvironment> TblEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the table browsers.
        /// </summary>
        public DbSet<TblDbLog> TblDbLog { get; set; }

        /// <summary>
        /// Gets or sets the table test categories.
        /// </summary>
        public DbSet<TblTestCategories> TblTestCategories { get; set; }

        /// <summary>
        /// Gets or sets the table user profile.
        /// </summary>
        public DbSet<TblUserProfile> TblUserProfile { get; set; }

        /// <summary>
        /// Gets or sets the table user profile.
        /// </summary>
        public DbSet<TblDBCategories> TblDBCategories { get; set; }

        /// <summary>
        /// Gets or sets the table Group.
        /// </summary>
        public DbSet<TblGroup> TblGroup { get; set; }

        /// <summary>
        /// Gets or sets the table Group User.
        /// </summary>
        public DbSet<TblGroupUser> TblGroupUser { get; set; }

        /// <summary>
        /// Gets or sets the table Group Module Access.
        /// </summary>
        public DbSet<TblGroupModuleAccess> TblGroupModuleAccess { get; set; }

        /// <summary>
        /// Gets or sets the table Module.
        /// </summary>
        public DbSet<TblModule> TblModule { get; set; }

        /// <summary>
        /// override save changes
        /// </summary>
        /// <returns>Save status</returns>
        public override int SaveChanges()
        {
            if (!AppSettings.Get(ConfigConstants.DbLogEntryFlag, true))
            {
                return base.SaveChanges();
            }

            List<TblDbLog> dbLogList = new List<TblDbLog>();
            IEnumerable<DbEntityEntry> dbEntityEntry = this.ChangeTracker.Entries();
            var dbEntityEntries = dbEntityEntry as IList<DbEntityEntry> ?? dbEntityEntry.ToList();

            var entityToIgnore = this.GetType()
                    .GetProperties()
                    .Select(n => n.PropertyType)
                    .Where(n => n.IsGenericType)
                    .Select(n => n.GetGenericArguments()[0])
                    .Where(n => n.GetCustomAttributes(typeof(EfIgnoreDbLogAttribute), true).FirstOrDefault() != null)
                    .Select(n => n.Name).ToList();

            foreach (var item in dbEntityEntries)
            {
                string classname = item.Entity.GetType().Name.Split('_')[0];

                if (!entityToIgnore.Contains(classname))
                {
                    TblDbLog dbLog = new TblDbLog
                    {
                        TableType = classname,
                        ValueId = (long)item.CurrentValues["Id"]
                    };
                    dynamic vari = item.Entity;
                    dbLog.LogTracker = (Guid)vari.LogTracker;
                    dbLog.ModifiedOn = DateTime.Now;
                    dbLog.OperationType = (int)item.State;
                    if (item.State == EntityState.Added)
                    {
                        dbLog.NewValue = this.GetEntryValueInString(item, false);
                        dbLog.CreatedBy = dbLog.ModifiedBy = (long)item.CurrentValues["CreatedBy"];
                    }
                    else
                    {
                        dbLog.PreviousValue = this.GetEntryValueInString(item, true);
                        dbLog.CreatedBy = dbLog.ModifiedBy = (long)item.CurrentValues["ModifiedBy"];
                    }

                    dbLogList.Add(dbLog);
                }
            }

            int saveChangeResult = base.SaveChanges();

            foreach (var item in dbEntityEntries)
            {
                string classname = item.Entity.GetType().Name.Split('_')[0];

                if (!entityToIgnore.Contains(classname))
                {
                    dynamic vari = item.Entity;
                    TblDbLog dbLog = dbLogList.FirstOrDefault(m => m.LogTracker == vari.LogTracker);
                    if (dbLog != null && dbLog.OperationType == (int)EntityState.Added)
                    {
                        dbLog.PreviousValue = null;
                        dbLog.NewValue = this.GetEntryValueInString(item, false);
                        dbLog.ValueId = vari.Id;
                        dbLog.CreatedBy = dbLog.ModifiedBy = (long)item.CurrentValues["CreatedBy"];
                    }
                    else if (dbLog != null)
                    {
                        dbLog.NewValue = this.GetEntryValueInString(item, false);
                        dbLog.CreatedBy = dbLog.ModifiedBy = (long)item.CurrentValues["ModifiedBy"];
                    }
                }
            }

            if (dbLogList.Count > 0)
            {
                foreach (var item in dbLogList)
                {
                    if (item.OperationType != (int)EntityState.Added)
                    {
                        int compareResult = string.Compare(item.PreviousValue, item.NewValue, StringComparison.InvariantCultureIgnoreCase);
                        if (compareResult != 0)
                        {
                            this.TblDbLog.Add(item);
                        }
                    }
                    else
                    {
                        this.TblDbLog.Add(item);
                    }
                }

                base.SaveChanges();
            }

            return saveChangeResult;
        }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TblAuthClients>().ToTable("TblAuthClients", DefaultSchema);
            modelBuilder.Entity<TblRefreshAuthTokens>().ToTable("TblRefreshAuthTokens", DefaultSchema);

            modelBuilder.Entity<CustomUser>().ToTable("TblUsers", DefaultSchema);
            modelBuilder.Entity<CustomUserLogin>().ToTable("TblUserLogin", DefaultSchema);
            modelBuilder.Entity<CustomUserRole>().ToTable("TblUserRole", DefaultSchema);
            modelBuilder.Entity<CustomUserClaim>().ToTable("TblUserClaim", DefaultSchema);
            modelBuilder.Entity<CustomRole>().ToTable("TblRole", DefaultSchema);

            modelBuilder.Entity<TblLnkSuiteTest>().ToTable("TblLnkSuiteTest", DefaultSchema);
            modelBuilder.Entity<TblAction>().ToTable("TblAction", DefaultSchema);
            modelBuilder.Entity<TblPages>().ToTable("TblPages", DefaultSchema);
            modelBuilder.Entity<TblLocator>().ToTable("TblLocator", DefaultSchema);
            modelBuilder.Entity<TblLocatorIdentifier>().ToTable("TblLocatorIdentifier", DefaultSchema);
            modelBuilder.Entity<TblSuite>().ToTable("TblSuite", DefaultSchema);
            modelBuilder.Entity<TblTest>().ToTable("TblTest", DefaultSchema);
            modelBuilder.Entity<TblTestData>().ToTable("TblTestData", DefaultSchema);
            modelBuilder.Entity<TblWebsite>().ToTable("TblWebsite", DefaultSchema);
            modelBuilder.Entity<TblLogger>().ToTable("TblLogger", DefaultSchema);

            modelBuilder.Entity<TblReportData>().ToTable("TblReportData", DefaultSchema);
            modelBuilder.Entity<TblTestQueue>().ToTable("TblTestQueue", DefaultSchema);

            modelBuilder.Entity<TblScheduler>().ToTable("TblScheduler", DefaultSchema);
            modelBuilder.Entity<TblSchedulerHistory>().ToTable("TblSchedulerHistory", DefaultSchema);

            modelBuilder.Entity<TblLnkSchedulerSuite>().ToTable("TblLnkSchedulerSuite", DefaultSchema);

            modelBuilder.Entity<TblSharedTest>().ToTable("TblSharedTest", DefaultSchema);
            modelBuilder.Entity<TblSharedTestData>().ToTable("TblSharedTestData", DefaultSchema);
            modelBuilder.Entity<TblLnkTestDataSharedTestData>().ToTable("TblLnkTestDataSharedTestData", DefaultSchema);

            modelBuilder.Entity<TblBrowsers>().ToTable("TblBrowsers", DefaultSchema);

            modelBuilder.Entity<TblEnvironment>().ToTable("TblEnvironment", DefaultSchema);
            modelBuilder.Entity<TblDbLog>().ToTable("TblDbLog", DefaultSchema);
            modelBuilder.Entity<TblTestCategories>().ToTable("TblTestCategories", DefaultSchema);
            modelBuilder.Entity<TblUserProfile>().ToTable("TblUserProfile", DefaultSchema);
            modelBuilder.Entity<TblDataBaseCategories>().ToTable("TblDataBaseCategories", DefaultSchema);
            modelBuilder.Entity<TblDataBaseConnection>().ToTable("TblDataBaseConnection", DefaultSchema);
            modelBuilder.Entity<TblGroup>().ToTable("TblGroup", DefaultSchema);
            modelBuilder.Entity<TblGroupUser>().ToTable("TblGroupUser", DefaultSchema);
            modelBuilder.Entity<TblGroupModuleAccess>().ToTable("TblGroupModuleAccess", DefaultSchema);
            modelBuilder.Entity<TblModule>().ToTable("TblModule", DefaultSchema);

            modelBuilder.Entity<TblApiCategories>().ToTable("TblApiCategories", DefaultSchema);
            modelBuilder.Entity<TblApiConnection>().ToTable("TblApiConnection", DefaultSchema);
        }

        /// <summary>
        /// Get the serialized DB entry value
        /// </summary>
        /// <param name="entry">the DB entry object</param>
        /// <param name="isOrginal">Flag indicate either to return Original or Current entries</param>
        /// <returns>Serialized object </returns>
        private string GetEntryValueInString(DbEntityEntry entry, bool isOrginal)
        {
            object target = this.CloneEntity(entry.Entity);
            foreach (string propName in entry.CurrentValues.PropertyNames)
            {
                object setterValue = isOrginal ? entry.OriginalValues[propName] : entry.CurrentValues[propName];

                PropertyInfo propInfo = target.GetType().GetProperty(propName);
                if (setterValue == DBNull.Value)
                {
                    setterValue = null;
                }

                propInfo.SetValue(target, setterValue, null);
            }

            string srtToReturn = JsonConvert.SerializeObject(target, new JsonSerializerSettings { ContractResolver = new DynamicContractResolver() });
            return srtToReturn;
        }

        /// <summary>
        /// Returns the Cloned copy of object
        /// </summary>
        /// <param name="entity">object to clone</param>
        /// <returns>cloned object</returns>
        private object CloneEntity(object entity)
        {
            DynamicContractResolver contractResolver = new DynamicContractResolver();
            Type type = entity.GetType();
            string str = JsonConvert.SerializeObject(entity, new JsonSerializerSettings { ContractResolver = contractResolver });
            object objToReturn = JsonConvert.DeserializeObject(str, type);
            return objToReturn;
        }
    }
}