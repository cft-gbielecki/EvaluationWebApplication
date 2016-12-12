using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EvaluationWebApplication.Migrations
{
    public partial class AddServicesToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceEntryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryID = table.Column<int>(nullable: false),
                    IsBillable = table.Column<bool>(nullable: false),
                    IsCountForOT = table.Column<bool>(nullable: false),
                    IsCountsForRequired = table.Column<bool>(nullable: false),
                    ServiceSuffix = table.Column<string>(nullable: true),
                    ServiceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceEntryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
