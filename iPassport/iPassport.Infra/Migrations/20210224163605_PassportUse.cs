using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class PassportUse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassportUses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CitizenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassportDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowAccess = table.Column<bool>(type: "bit", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassportUses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassportUses_PassportDetails_PassportDetailsId",
                        column: x => x.PassportDetailsId,
                        principalTable: "PassportDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassportUses_UserDetails_AgentId",
                        column: x => x.AgentId,
                        principalTable: "UserDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassportUses_UserDetails_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "UserDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassportUses_AgentId",
                table: "PassportUses",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_PassportUses_CitizenId",
                table: "PassportUses",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_PassportUses_PassportDetailsId",
                table: "PassportUses",
                column: "PassportDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassportUses");
        }
    }
}
