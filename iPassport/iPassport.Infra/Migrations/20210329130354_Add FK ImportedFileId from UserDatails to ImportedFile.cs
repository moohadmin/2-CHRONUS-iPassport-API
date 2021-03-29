using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class AddFKImportedFileIdfromUserDatailstoImportedFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImportedFileId",
                table: "UserDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_ImportedFileId",
                table: "UserDetails",
                column: "ImportedFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_ImportedFiles_ImportedFileId",
                table: "UserDetails",
                column: "ImportedFileId",
                principalTable: "ImportedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_ImportedFiles_ImportedFileId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_ImportedFileId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "ImportedFileId",
                table: "UserDetails");
        }
    }
}
