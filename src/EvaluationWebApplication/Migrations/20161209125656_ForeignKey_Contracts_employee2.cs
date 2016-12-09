using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluationWebApplication.Migrations
{
    public partial class ForeignKey_Contracts_employee2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Contracts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployeeID",
                table: "Contracts",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Employees_EmployeeID",
                table: "Contracts",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Employees_EmployeeID",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_EmployeeID",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Contracts");
        }
    }
}
