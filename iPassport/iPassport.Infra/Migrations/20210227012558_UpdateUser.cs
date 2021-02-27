using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Breed",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "CNS",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "InternationalDocument",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "PassportDoc",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "RG",
                table: "UserDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "UserDetails",
                type: "DateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNS",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternationalDocument",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "UserDetails",
                type: "DateTime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportDoc",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RG",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
