using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class HealthUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthUnitTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthUnitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    ResponsiblePersonName = table.Column<string>(type: "text", nullable: true),
                    ResponsiblePersonPhone = table.Column<string>(type: "text", nullable: true),
                    ResponsiblePersonOccupation = table.Column<string>(type: "text", nullable: true),
                    DeactivationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthUnits_HealthUnitTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "HealthUnitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthUnits_TypeId",
                table: "HealthUnits",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthUnits");

            migrationBuilder.DropTable(
                name: "HealthUnitTypes");
        }
    }
}
