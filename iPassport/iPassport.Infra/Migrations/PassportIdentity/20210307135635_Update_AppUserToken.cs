using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_AppUserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserTokens",
                table: "AppUserTokens");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AppUserTokens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "AppUserTokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "AppUserTokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserTokens",
                table: "AppUserTokens",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTokens_UserId_Provider_Value",
                table: "AppUserTokens",
                columns: new[] { "UserId", "Provider", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTokens_Value",
                table: "AppUserTokens",
                column: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserTokens",
                table: "AppUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AppUserTokens_UserId_Provider_Value",
                table: "AppUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AppUserTokens_Value",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AppUserTokens");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "AppUserTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserTokens",
                table: "AppUserTokens",
                columns: new[] { "UserId", "Provider", "Value" });
        }
    }
}
