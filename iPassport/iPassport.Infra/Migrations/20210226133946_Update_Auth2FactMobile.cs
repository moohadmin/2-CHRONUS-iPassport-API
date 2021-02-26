using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_Auth2FactMobile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportUses_PassportDetails_PassportDetailsId",
                table: "PassportUses");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportUses_UserDetails_CitizenId",
                table: "PassportUses");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "Auth2FactMobile",
                newName: "IsValid");

            migrationBuilder.AddForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportUses_PassportDetails_PassportDetailsId",
                table: "PassportUses",
                column: "PassportDetailsId",
                principalTable: "PassportDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportUses_UserDetails_CitizenId",
                table: "PassportUses",
                column: "CitizenId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportUses_PassportDetails_PassportDetailsId",
                table: "PassportUses");

            migrationBuilder.DropForeignKey(
                name: "FK_PassportUses_UserDetails_CitizenId",
                table: "PassportUses");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "Auth2FactMobile",
                newName: "IsUsed");

            migrationBuilder.AddForeignKey(
                name: "FK_PassportDetails_Passports_PassportId",
                table: "PassportDetails",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_UserDetails_UserDetailsId",
                table: "Passports",
                column: "UserDetailsId",
                principalTable: "UserDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportUses_PassportDetails_PassportDetailsId",
                table: "PassportUses",
                column: "PassportDetailsId",
                principalTable: "PassportDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PassportUses_UserDetails_CitizenId",
                table: "PassportUses",
                column: "CitizenId",
                principalTable: "UserDetails",
                principalColumn: "Id");
        }
    }
}
