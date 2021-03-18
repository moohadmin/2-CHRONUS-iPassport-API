using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_User_Drop_PriorityGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorityGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "BloodTypeId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HumanRaceId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Adresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Adresses",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BloodTypeId",
                table: "Users",
                column: "BloodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HumanRaceId",
                table: "Users",
                column: "HumanRaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BloodTypes_BloodTypeId",
                table: "Users",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Genders_GenderId",
                table: "Users",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HumanRaces_HumanRaceId",
                table: "Users",
                column: "HumanRaceId",
                principalTable: "HumanRaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BloodTypes_BloodTypeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Genders_GenderId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_HumanRaces_HumanRaceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BloodTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GenderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HumanRaceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BloodTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HumanRaceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Adresses");

            migrationBuilder.CreateTable(
                name: "PriorityGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityGroups", x => x.Id);
                });
        }
    }
}
