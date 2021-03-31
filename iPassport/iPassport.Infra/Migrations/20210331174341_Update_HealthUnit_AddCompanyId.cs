using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_HealthUnit_AddCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "HealthUnits",
                type: "uuid",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "HealthUnits");
        }
    }
}
