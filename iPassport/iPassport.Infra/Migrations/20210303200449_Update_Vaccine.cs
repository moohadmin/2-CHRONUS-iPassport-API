using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_Vaccine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(name: "UniqueDose",table: "Vaccines");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vaccines",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxTimeNextDose",
                table: "Vaccines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinTimeNextDose",
                table: "Vaccines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTimeNextDose",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "MinTimeNextDose",
                table: "Vaccines");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vaccines",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "UniqueDose",
                table: "Vaccines",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
