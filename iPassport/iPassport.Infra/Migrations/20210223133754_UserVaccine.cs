using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace iPassport.Infra.Migrations
{
    public partial class UserVaccine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseVaccine_Vaccine_VaccinesId",
                table: "DiseaseVaccine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccine",
                table: "Vaccines");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Vaccines",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Laboratory",
                table: "Vaccines",
                type: "nvarchar(80)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vaccines",
                type: "nvarchar(300)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Vaccines",
                type: "DateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserVaccine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VaccinationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVaccine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVaccine_UserDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "UserDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserVaccine_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccine_UserDetailsId",
                table: "UserVaccine",
                column: "UserDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccine_VaccineId",
                table: "UserVaccine",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseVaccine_Vaccines_VaccinesId",
                table: "DiseaseVaccine",
                column: "VaccinesId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseaseVaccine_Vaccines_VaccinesId",
                table: "DiseaseVaccine");

            migrationBuilder.DropTable(
                name: "UserVaccine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines");

            migrationBuilder.RenameTable(
                name: "Vaccines",
                newName: "Vaccines");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Vaccines",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AlterColumn<string>(
                name: "Laboratory",
                table: "Vaccines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vaccines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Vaccine",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DateTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccine",
                table: "Vaccines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseaseVaccine_Vaccine_VaccinesId",
                table: "DiseaseVaccine",
                column: "VaccinesId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
