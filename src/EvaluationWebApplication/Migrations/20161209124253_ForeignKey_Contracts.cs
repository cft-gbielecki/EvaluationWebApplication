using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluationWebApplication.Migrations
{
    public partial class ForeignKey_Contracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientID",
                table: "Contracts",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Clients_ClientID",
                table: "Contracts",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Clients_ClientID",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ClientID",
                table: "Contracts");
        }
    }
}
