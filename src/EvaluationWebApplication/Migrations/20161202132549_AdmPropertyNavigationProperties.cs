using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluationWebApplication.Migrations
{
    public partial class AdmPropertyNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee",
                table: "TimeEntries");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "TimeEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "TimeEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeSurname",
                table: "TimeEntries",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdministrator",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_EmployeeID",
                table: "TimeEntries",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntries_Employees_EmployeeID",
                table: "TimeEntries",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntries_Employees_EmployeeID",
                table: "TimeEntries");

            migrationBuilder.DropIndex(
                name: "IX_TimeEntries_EmployeeID",
                table: "TimeEntries");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "TimeEntries");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "TimeEntries");

            migrationBuilder.DropColumn(
                name: "EmployeeSurname",
                table: "TimeEntries");

            migrationBuilder.DropColumn(
                name: "IsAdministrator",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Employee",
                table: "TimeEntries",
                nullable: true);
        }
    }
}
