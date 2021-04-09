using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_Company_AddSegmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SegmentId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SegmentId",
                table: "Companies",
                column: "SegmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanySegments_SegmentId",
                table: "Companies",
                column: "SegmentId",
                principalTable: "CompanySegments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanySegments_SegmentId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_SegmentId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "Companies");
        }
    }
}
