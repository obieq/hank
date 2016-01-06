// ---------------------------------------------------------------------------------------------------
// <copyright file="201408061633452_InitialCreate.cs" company="Rekurant">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2014-08-18</date>
// <summary>
//     The 201408061633452_InitialCreate class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Rekurant.DataService.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The InitialCreate class
    /// </summary>
    public partial class InitialCreate : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            this.CreateTable(
            "Logger",
                 c => new
                 {
                     Id = c.Long(nullable: false, identity: true),
                     Machine = c.String(true, 24),
                     EnumLevel = c.Int(true, false, 0),
                     Filename = c.String(true, 255),
                     Method = c.String(true, 255),
                     LineNumber = c.Int(true, false, 0),
                     Message = c.String(true, 1024),
                     StackTrace = c.String(true, 400)
                 }).PrimaryKey(t => t.Id);
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            this.DropTable("Gateways");
        }
    }
}
