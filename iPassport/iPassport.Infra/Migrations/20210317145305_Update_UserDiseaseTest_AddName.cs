using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_UserDiseaseTest_AddName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDiseaseTests_Diseases_DiseaseId",
                table: "UserDiseaseTests");

            migrationBuilder.DropIndex(
                name: "IX_UserDiseaseTests_DiseaseId",
                table: "UserDiseaseTests");

            // migrationBuilder.DropColumn( name: "DiseaseId", table: "UserDiseaseTests");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserDiseaseTests",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserDiseaseTests");

            migrationBuilder.AddColumn<Guid>(
                name: "DiseaseId",
                table: "UserDiseaseTests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserDiseaseTests_DiseaseId",
                table: "UserDiseaseTests",
                column: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDiseaseTests_Diseases_DiseaseId",
                table: "UserDiseaseTests",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
