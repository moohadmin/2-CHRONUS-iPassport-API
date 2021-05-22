using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class UpdateDosageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Identifyer",
                table: "VaccineDosageTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder
                .Sql("UPDATE \"VaccineDosageTypes\" SET \"Identifyer\" = 1 WHERE \"Description\" = 'Geral'");

            migrationBuilder
                .Sql("UPDATE \"VaccineDosageTypes\" SET \"Identifyer\" = 2 WHERE \"Description\" = 'Por Faixa Etária'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifyer",
                table: "VaccineDosageTypes");
        }
    }
}
