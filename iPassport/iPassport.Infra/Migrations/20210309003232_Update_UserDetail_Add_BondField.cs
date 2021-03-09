using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_UserDetail_Add_BondField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bond",
                table: "UserDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriorityGroup",
                table: "UserDetails",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bond",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "PriorityGroup",
                table: "UserDetails");
        }
    }
}
