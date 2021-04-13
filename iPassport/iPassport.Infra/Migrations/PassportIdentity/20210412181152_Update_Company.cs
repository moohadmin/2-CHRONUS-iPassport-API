using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "Companies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeactivationUserId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHeadquarters",
                table: "Companies",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsibleId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeName",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyResponsibles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Occupation = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    MobilePhone = table.Column<string>(type: "text", nullable: true),
                    Landline = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyResponsibles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_DeactivationUserId",
                table: "Companies",
                column: "DeactivationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ParentId",
                table: "Companies",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ResponsibleId",
                table: "Companies",
                column: "ResponsibleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Companies_ParentId",
                table: "Companies",
                column: "ParentId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyResponsibles_ResponsibleId",
                table: "Companies",
                column: "ResponsibleId",
                principalTable: "CompanyResponsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_DeactivationUserId",
                table: "Companies",
                column: "DeactivationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Companies_ParentId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyResponsibles_ResponsibleId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_DeactivationUserId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyResponsibles");

            migrationBuilder.DropIndex(
                name: "IX_Companies_DeactivationUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Name",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ParentId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ResponsibleId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DeactivationUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsHeadquarters",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "TradeName",
                table: "Companies");
        }
    }
}
