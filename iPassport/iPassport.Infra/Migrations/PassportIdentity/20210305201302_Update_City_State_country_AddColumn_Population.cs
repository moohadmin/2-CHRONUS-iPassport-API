using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_City_State_country_AddColumn_Population : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Population",
                table: "States",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Population",
                table: "Countries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Population",
                table: "Cities",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Population",
                table: "States");

            migrationBuilder.DropColumn(
                name: "Population",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Population",
                table: "Cities");
        }
    }
}
