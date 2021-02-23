using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class UpdateUserDetails_Add_PassportDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Passport",
                table: "UserDetails",
                newName: "PassportDoc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassportDoc",
                table: "UserDetails",
                newName: "Passport");
        }
    }
}
