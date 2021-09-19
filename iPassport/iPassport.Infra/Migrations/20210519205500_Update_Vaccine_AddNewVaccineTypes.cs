using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iPassport.Infra.Migrations
{
    public partial class Update_Vaccine_AddNewVaccineTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DosageTypeId",
                table: "Vaccines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VaccineDosageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineDosageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaccinePeriodTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Identifyer = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinePeriodTypes", x => x.Id);
                });

            migrationBuilder
                    .Sql("Insert into \"VaccinePeriodTypes\" (\"Id\", \"Description\", \"Identifyer\", \"CreateDate\", \"UpdateDate\") VALUES(uuid_generate_v4(), 'Fixo', 1, timezone('utc', now()), timezone('utc', now())),"
                    + "(uuid_generate_v4(), 'Variável', 2, timezone('utc', now()), timezone('utc', now()));");

            migrationBuilder
                    .Sql("Insert into \"VaccineDosageTypes\" (\"Id\", \"Description\", \"CreateDate\", \"UpdateDate\") VALUES(uuid_generate_v4(), 'Geral', timezone('utc', now()), timezone('utc', now())),"
                    + "(uuid_generate_v4(), 'Por Faixa Etária', timezone('utc', now()), timezone('utc', now()));");

            migrationBuilder
                   .Sql("UPDATE \"Vaccines\" SET \"DosageTypeId\" = (select vt.\"Id\" from \"VaccineDosageTypes\" vt where vt.\"Description\" = 'Geral')");

            migrationBuilder.CreateTable(
                name: "AgeGroupVaccines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VaccineId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredDoses = table.Column<int>(type: "integer", nullable: false),
                    MaxTimeNextDose = table.Column<int>(type: "integer", nullable: false),
                    MinTimeNextDose = table.Column<int>(type: "integer", nullable: false),
                    InitalAgeGroup = table.Column<int>(type: "integer", nullable: false),
                    FinalAgeGroup = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeGroupVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgeGroupVaccines_VaccinePeriodTypes_PeriodTypeId",
                        column: x => x.PeriodTypeId,
                        principalTable: "VaccinePeriodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgeGroupVaccines_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneralGroupVaccines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VaccineId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredDoses = table.Column<int>(type: "integer", nullable: false),
                    MaxTimeNextDose = table.Column<int>(type: "integer", nullable: false),
                    MinTimeNextDose = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralGroupVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralGroupVaccines_VaccinePeriodTypes_PeriodTypeId",
                        column: x => x.PeriodTypeId,
                        principalTable: "VaccinePeriodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralGroupVaccines_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_DosageTypeId",
                table: "Vaccines",
                column: "DosageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeGroupVaccines_PeriodTypeId",
                table: "AgeGroupVaccines",
                column: "PeriodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgeGroupVaccines_VaccineId",
                table: "AgeGroupVaccines",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralGroupVaccines_PeriodTypeId",
                table: "GeneralGroupVaccines",
                column: "PeriodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralGroupVaccines_VaccineId",
                table: "GeneralGroupVaccines",
                column: "VaccineId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_VaccineDosageTypes_DosageTypeId",
                table: "Vaccines",
                column: "DosageTypeId",
                principalTable: "VaccineDosageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder
                .Sql("INSERT INTO \"GeneralGroupVaccines\" (\"Id\", \"PeriodTypeId\", \"VaccineId\", \"RequiredDoses\", \"MaxTimeNextDose\", \"MinTimeNextDose\", \"CreateDate\", \"UpdateDate\")\n"
                        + "select uuid_generate_v4(),"
                        + "(select v.\"Id\" from \"VaccinePeriodTypes\" v where v.\"Description\" = 'Fixo'),"
                        + "\"Id\","
                        + "\"RequiredDoses\","
                        + "\"MaxTimeNextDose\","
                        + "\"MinTimeNextDose\","
                        + "timezone('utc', now()),"
                        + "timezone('utc', now()) FROM \"Vaccines\"");

            // migrationBuilder.DropColumn( name: "MaxTimeNextDose", table: "Vaccines");

            // migrationBuilder.DropColumn( name: "MinTimeNextDose", table: "Vaccines");

            // migrationBuilder.DropColumn( name: "RequiredDoses", table: "Vaccines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_VaccineDosageTypes_DosageTypeId",
                table: "Vaccines");

            migrationBuilder.DropTable(
                name: "AgeGroupVaccines");

            migrationBuilder.DropTable(
                name: "GeneralGroupVaccines");

            migrationBuilder.DropTable(
                name: "VaccineDosageTypes");

            migrationBuilder.DropTable(
                name: "VaccinePeriodTypes");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_DosageTypeId",
                table: "Vaccines");

            migrationBuilder.DropColumn( name: "DosageTypeId", table: "Vaccines");

            migrationBuilder.AddColumn<int>(
                name: "MaxTimeNextDose",
                table: "Vaccines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinTimeNextDose",
                table: "Vaccines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredDoses",
                table: "Vaccines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
