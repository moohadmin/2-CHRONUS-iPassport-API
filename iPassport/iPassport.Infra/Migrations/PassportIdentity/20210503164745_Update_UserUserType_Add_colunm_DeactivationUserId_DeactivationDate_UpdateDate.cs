using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class Update_UserUserType_Add_colunm_DeactivationUserId_DeactivationDate_UpdateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UserUserTypes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "UserUserTypes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "UserUserTypes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder
                .Sql("Update public.\"UserUserTypes\" as uut \n set \"LastLogin\" = u.\"LastLogin\", \n " +
                "\"CreateDate\" = timezone('utc', now()), \n \"UpdateDate\" = timezone('utc', now()) \n" +
                "from public.\"Users\" u where uut.\"UserId\" = u.\"Id\" ;"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserUserTypes");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "UserUserTypes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "UserUserTypes");
        }
    }
}
