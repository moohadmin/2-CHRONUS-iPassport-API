using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_UserDetails_AddHealthUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HealthUnitId",
                table: "UserDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_HealthUnitId",
                table: "UserDetails",
                column: "HealthUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_HealthUnits_HealthUnitId",
                table: "UserDetails",
                column: "HealthUnitId",
                principalTable: "HealthUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_HealthUnits_HealthUnitId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_HealthUnitId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "HealthUnitId",
                table: "UserDetails");
        }
    }
}
