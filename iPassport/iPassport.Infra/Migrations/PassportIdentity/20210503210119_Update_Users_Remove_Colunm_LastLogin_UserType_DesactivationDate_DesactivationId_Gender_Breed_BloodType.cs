using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_Users_Remove_Colunm_LastLogin_UserType_DesactivationDate_DesactivationId_Gender_Breed_BloodType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey( name: "FK_Users_Users_DeactivationUserId", table: "Users");

            migrationBuilder.DropIndex( name: "IX_Users_DeactivationUserId", table: "Users");

            // migrationBuilder.DropColumn( name: "BloodType", table: "Users");
            // migrationBuilder.DropColumn( name: "Breed", table: "Users");
            // migrationBuilder.DropColumn( name: "DeactivationDate", table: "Users");
            // migrationBuilder.DropColumn( name: "DeactivationUserId", table: "Users");
            // migrationBuilder.DropColumn( name: "Gender", table: "Users");
            // migrationBuilder.DropColumn( name: "LastLogin", table: "Users");
            // migrationBuilder.DropColumn( name: "UserType", table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "Users",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
