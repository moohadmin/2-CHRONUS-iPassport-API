using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace iPassport.Infra.Migrations
{
    public partial class VaccineManufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseVaccine_Diseases_DiseasesId",
                table: "DiseaseVaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseVaccine_Vaccines_VaccinesId",
                table: "DiseaseVaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_UserDetails_UserId",
                table: "UserVaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_Vaccines_VaccineId",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "Laboratory",
                table: "Vaccines");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Vaccines",
                newName: "Name");

            migrationBuilder.AddColumn<Guid>(
                name: "ManufacturerId",
                table: "Vaccines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VaccineManufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineManufacturers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_ManufacturerId",
                table: "Vaccines",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseVaccine_Diseases_DiseasesId",
                table: "DiseaseVaccine",
                column: "DiseasesId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseVaccine_Vaccines_VaccinesId",
                table: "DiseaseVaccine",
                column: "VaccinesId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccines_UserDetails_UserId",
                table: "UserVaccines",
                column: "UserId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccines_Vaccines_VaccineId",
                table: "UserVaccines",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_VaccineManufacturers_ManufacturerId",
                table: "Vaccines",
                column: "ManufacturerId",
                principalTable: "VaccineManufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseVaccine_Diseases_DiseasesId",
                table: "DiseaseVaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseVaccine_Vaccines_VaccinesId",
                table: "DiseaseVaccine");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_UserDetails_UserId",
                table: "UserVaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_Vaccines_VaccineId",
                table: "UserVaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VaccineManufacturers_ManufacturerId",
                table: "Vaccines");

            migrationBuilder.DropTable(
                name: "VaccineManufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_ManufacturerId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Vaccines");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vaccines",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Laboratory",
                table: "Vaccines",
                type: "nvarchar(80)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseVaccine_Diseases_DiseasesId",
                table: "DiseaseVaccine",
                column: "DiseasesId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseVaccine_Vaccines_VaccinesId",
                table: "DiseaseVaccine",
                column: "VaccinesId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
