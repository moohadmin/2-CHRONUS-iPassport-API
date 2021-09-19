using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_Users_Add_DeactivationDate_DeactivationUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeactivationUserId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeactivationUserId",
                table: "Users",
                column: "DeactivationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_DeactivationUserId",
                table: "Users",
                column: "DeactivationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_DeactivationUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DeactivationUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeactivationUserId",
                table: "Users");
        }
    }
}
