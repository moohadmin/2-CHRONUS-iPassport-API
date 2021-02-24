using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_UserVaccine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccine_UserDetails_UserDetailsId",
                table: "UserVaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccine_Vaccines_VaccineId",
                table: "UserVaccine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVaccine",
                table: "UserVaccine");

            migrationBuilder.DropIndex(
                name: "IX_UserVaccine_UserDetailsId",
                table: "UserVaccine");

            migrationBuilder.DropColumn(
                name: "UserDetailsId",
                table: "UserVaccine");

            migrationBuilder.RenameTable(
                name: "UserVaccine",
                newName: "UserVaccines");

            migrationBuilder.RenameIndex(
                name: "IX_UserVaccine_VaccineId",
                table: "UserVaccines",
                newName: "IX_UserVaccines_VaccineId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VaccinationDate",
                table: "UserVaccines",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "UserVaccines",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserVaccines",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVaccines",
                table: "UserVaccines",
                column: "Id");

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccines_Vaccines_VaccineId",
                table: "UserVaccines",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_UserDetails_UserId",
                table: "UserVaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_Vaccines_VaccineId",
                table: "UserVaccines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVaccines",
                table: "UserVaccines");

            migrationBuilder.DropIndex(
                name: "IX_UserVaccines_UserId",
                table: "UserVaccines");

            migrationBuilder.RenameTable(
                name: "UserVaccines",
                newName: "UserVaccine");

            migrationBuilder.RenameIndex(
                name: "IX_UserVaccines_VaccineId",
                table: "UserVaccine",
                newName: "IX_UserVaccine_VaccineId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VaccinationDate",
                table: "UserVaccine",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "UserVaccine",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "UserVaccine",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AddColumn<Guid>(
                name: "UserDetailsId",
                table: "UserVaccine",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVaccine",
                table: "UserVaccine",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccine_UserDetailsId",
                table: "UserVaccine",
                column: "UserDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccine_UserDetails_UserDetailsId",
                table: "UserVaccine",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccine_Vaccines_VaccineId",
                table: "UserVaccine",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
