using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations.PassportIdentity
{
    public partial class AddUsertypeAndUserUserType_UpdateUserAndAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorporateCellphoneNumber",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Adresses",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Identifyer = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserUserTypes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeactivationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeactivationUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserTypes", x => new { x.UserId, x.UserTypeId });
                    table.ForeignKey(
                        name: "FK_UserUserTypes_Users_DeactivationUserId",
                        column: x => x.DeactivationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserUserTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserUserTypes_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTypes_Identifyer",
                table: "UserTypes",
                column: "Identifyer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserUserTypes_DeactivationUserId",
                table: "UserUserTypes",
                column: "DeactivationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUserTypes_UserTypeId",
                table: "UserUserTypes",
                column: "UserTypeId");

            migrationBuilder
                .Sql("Insert into \"UserTypes\" (\"Id\", \"Name\", \"Identifyer\", \"CreateDate\", \"UpdateDate\") VALUES(uuid_generate_v4(), 'Administrativo', 0, timezone('utc', now()), timezone('utc', now()));"
                + "Insert into \"UserTypes\" (\"Id\", \"Name\", \"Identifyer\",  \"CreateDate\", \"UpdateDate\") VALUES(uuid_generate_v4(), 'Cidadão', 1, timezone('utc', now()), timezone('utc', now()));"
                + "Insert into \"UserTypes\" (\"Id\", \"Name\", \"Identifyer\", \"CreateDate\", \"UpdateDate\") VALUES(uuid_generate_v4(), 'Agente', 2, timezone('utc', now()), timezone('utc', now()));"
                );

            migrationBuilder
                    .Sql("Insert into \"UserUserTypes\" (\"UserId\", \"UserTypeId\", \"DeactivationDate\", \"DeactivationUserId\") "
                    + "select u.\"Id\" as UserId, (select ut.\"Id\" from \"UserTypes\" ut where ut.\"Identifyer\" = u.\"UserType\") as UserTypeId, u.\"DeactivationDate\", u.\"DeactivationUserId\" from public.\"Users\" u "
                    + "where u.\"UserType\" is not null;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUserTypes");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropColumn(
                name: "CorporateCellphoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Adresses");
        }
    }
}
