using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_UserVaccine_AddEpmployeeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "UserVaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "UserVaccines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCoren",
                table: "UserVaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCpf",
                table: "UserVaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "UserVaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "UserVaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnityType",
                table: "UserVaccines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Batch",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "EmployeeCoren",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "EmployeeCpf",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "UnityType",
                table: "UserVaccines");
        }
    }
}
