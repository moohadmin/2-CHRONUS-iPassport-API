using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_CompanyResponsible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyResponsibles_ResponsibleId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ResponsibleId",
                table: "Companies");

            // migrationBuilder.DropColumn( name: "ResponsibleId", table: "Companies");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyResponsibles_Companies_Id",
                table: "CompanyResponsibles",
                column: "Id",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyResponsibles_Companies_Id",
                table: "CompanyResponsibles");

            migrationBuilder.AddColumn<Guid>(
                name: "ResponsibleId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ResponsibleId",
                table: "Companies",
                column: "ResponsibleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyResponsibles_ResponsibleId",
                table: "Companies",
                column: "ResponsibleId",
                principalTable: "CompanyResponsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
