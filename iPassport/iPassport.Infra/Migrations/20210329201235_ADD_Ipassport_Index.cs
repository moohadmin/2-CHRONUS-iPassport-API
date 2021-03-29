using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class ADD_Ipassport_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_Name",
                table: "Vaccines",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineManufacturers_Name",
                table: "VaccineManufacturers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityGroups_Name",
                table: "PriorityGroups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_HealthUnits_Cnpj",
                table: "HealthUnits",
                column: "Cnpj");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vaccines_Name",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_VaccineManufacturers_Name",
                table: "VaccineManufacturers");

            migrationBuilder.DropIndex(
                name: "IX_PriorityGroups_Name",
                table: "PriorityGroups");

            migrationBuilder.DropIndex(
                name: "IX_HealthUnits_Cnpj",
                table: "HealthUnits");
        }
    }
}
