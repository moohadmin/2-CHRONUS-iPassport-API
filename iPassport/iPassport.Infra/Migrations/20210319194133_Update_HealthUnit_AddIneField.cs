using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_HealthUnit_AddIneField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "HealthUnits",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ine",
                table: "HealthUnits",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthUnits_Ine",
                table: "HealthUnits",
                column: "Ine",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HealthUnits_Ine",
                table: "HealthUnits");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "HealthUnits");

            migrationBuilder.DropColumn(
                name: "Ine",
                table: "HealthUnits");
        }
    }
}
