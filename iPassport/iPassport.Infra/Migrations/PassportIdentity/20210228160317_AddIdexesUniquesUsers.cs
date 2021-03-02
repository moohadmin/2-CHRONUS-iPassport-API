using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class AddIdexesUniquesUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_CNS",
                table: "Users",
                column: "CNS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CPF",
                table: "Users",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_InternationalDocument",
                table: "Users",
                column: "InternationalDocument",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassportDoc",
                table: "Users",
                column: "PassportDoc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RG",
                table: "Users",
                column: "RG",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CNS",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CPF",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_InternationalDocument",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PassportDoc",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RG",
                table: "Users");
        }
    }
}
