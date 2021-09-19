using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_UserDetails_UserDetailsId",
                table: "UserVaccines");

            migrationBuilder.DropIndex(
                name: "IX_UserVaccines_UserDetailsId",
                table: "UserVaccines");

            // migrationBuilder.DropColumn( name: "UserDetailsId", table: "UserVaccines");

            // migrationBuilder.DropColumn(name: "UserId",table: "UserDetails");

            migrationBuilder.RenameColumn(
                name: "ImunizationTime",
                table: "Vaccines",
                newName: "ImmunizationTimeInDays");

            migrationBuilder.RenameColumn(
                name: "ExpirationTime",
                table: "Vaccines",
                newName: "ExpirationTimeInMonths");

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccines_UserId",
                table: "UserVaccines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccines_UserDetails_UserId",
                table: "UserVaccines",
                column: "UserId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_UserDetails_UserId",
                table: "UserVaccines");

            migrationBuilder.DropIndex(
                name: "IX_UserVaccines_UserId",
                table: "UserVaccines");

            migrationBuilder.RenameColumn(
                name: "ImmunizationTimeInDays",
                table: "Vaccines",
                newName: "ImunizationTime");

            migrationBuilder.RenameColumn(
                name: "ExpirationTimeInMonths",
                table: "Vaccines",
                newName: "ExpirationTime");

            migrationBuilder.AddColumn<Guid>(
                name: "UserDetailsId",
                table: "UserVaccines",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserDetails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccines_UserDetailsId",
                table: "UserVaccines",
                column: "UserDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccines_UserDetails_UserDetailsId",
                table: "UserVaccines",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
