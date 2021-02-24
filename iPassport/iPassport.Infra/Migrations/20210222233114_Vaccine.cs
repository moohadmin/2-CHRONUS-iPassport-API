using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Vaccine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Laboratory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredDoses = table.Column<int>(type: "int", nullable: false),
                    ExpirationTime = table.Column<int>(type: "int", nullable: false),
                    ImunizationTime = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseVaccine",
                columns: table => new
                {
                    DiseasesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VaccinesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseVaccine", x => new { x.DiseasesId, x.VaccinesId });
                    table.ForeignKey(
                        name: "FK_DiseaseVaccine_Diseases_DiseasesId",
                        column: x => x.DiseasesId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseaseVaccine_Vaccine_VaccinesId",
                        column: x => x.VaccinesId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseVaccine_VaccinesId",
                table: "DiseaseVaccine",
                column: "VaccinesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiseaseVaccine");

            migrationBuilder.DropTable(
                name: "Vaccines");
        }
    }
}
