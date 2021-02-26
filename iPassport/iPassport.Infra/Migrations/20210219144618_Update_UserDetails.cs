using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace iPassport.Infra.Migrations
{
    public partial class Update_UserDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "UserDetails",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserDetails",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserDetails",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "UserDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_PlanId",
                table: "UserDetails",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Plans_PlanId",
                table: "UserDetails",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Plans_PlanId",
                table: "UserDetails");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_PlanId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "UserDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "UserDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");
        }
    }
}
