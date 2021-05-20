using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_HealthUnit_AddPhoneNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePersonLandline",
                table: "HealthUnits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePersonMobilePhone",
                table: "HealthUnits",
                type: "text",
                nullable: true);

            migrationBuilder.DropColumn(
               name: "ResponsiblePersonPhone",
               table: "HealthUnits");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsiblePersonLandline",
                table: "HealthUnits");

            migrationBuilder.DropColumn(
               name: "ResponsiblePersonMobilePhone",
               table: "HealthUnits");

            migrationBuilder.AddColumn<string>(
                name: "ResponsiblePersonPhone",
                table: "HealthUnits",
                type: "text",
                nullable: true);
        }
    }
}
