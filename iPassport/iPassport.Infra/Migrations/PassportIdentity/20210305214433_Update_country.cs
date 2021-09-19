using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn( name: "IbgeCode", table: "Countries");

            migrationBuilder.AlterColumn<long>(
                name: "Population",
                table: "Countries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalCode",
                table: "Countries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalCode",
                table: "Countries");

            migrationBuilder.AlterColumn<int>(
                name: "Population",
                table: "Countries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IbgeCode",
                table: "Countries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
