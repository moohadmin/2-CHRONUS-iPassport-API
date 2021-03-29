using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_UserDetails_UserVaccine_Create_PriorityGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UnityType",
                table: "UserVaccines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "UserVaccines",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "HealthUnitId",
                table: "UserVaccines",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PriorityGroupId",
                table: "UserDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PriorityGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVaccines_HealthUnitId",
                table: "UserVaccines",
                column: "HealthUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_PriorityGroupId",
                table: "UserDetails",
                column: "PriorityGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_PriorityGroups_PriorityGroupId",
                table: "UserDetails",
                column: "PriorityGroupId",
                principalTable: "PriorityGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVaccines_HealthUnits_HealthUnitId",
                table: "UserVaccines",
                column: "HealthUnitId",
                principalTable: "HealthUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_PriorityGroups_PriorityGroupId",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVaccines_HealthUnits_HealthUnitId",
                table: "UserVaccines");

            migrationBuilder.DropTable(
                name: "PriorityGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserVaccines_HealthUnitId",
                table: "UserVaccines");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_PriorityGroupId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "HealthUnitId",
                table: "UserVaccines");

            migrationBuilder.DropColumn(
                name: "PriorityGroupId",
                table: "UserDetails");

            migrationBuilder.AlterColumn<int>(
                name: "UnityType",
                table: "UserVaccines",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "UserVaccines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
