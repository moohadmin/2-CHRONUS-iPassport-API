using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_HealthUnityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Identifyer",
                table: "HealthUnitTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UniqueCode",
                table: "HealthUnits",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthUnits_UniqueCode",
                table: "HealthUnits",
                column: "UniqueCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HealthUnits_UniqueCode",
                table: "HealthUnits");

            migrationBuilder.DropColumn(
                name: "Identifyer",
                table: "HealthUnitTypes");

            migrationBuilder.DropColumn(
                name: "UniqueCode",
                table: "HealthUnits");
        }
    }
}
