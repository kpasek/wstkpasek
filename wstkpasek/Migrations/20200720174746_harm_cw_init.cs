using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace wstkp.Migrations
{
    public partial class harm_cw_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HCwiczenia",
                columns: table => new
                {
                    HCwiczenieId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    CwiczenieId = table.Column<int>(nullable: false),
                    HTreningId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCwiczenia", x => x.HCwiczenieId);
                    table.ForeignKey(
                        name: "FK_HCwiczenia_cwiczenia_CwiczenieId",
                        column: x => x.CwiczenieId,
                        principalTable: "cwiczenia",
                        principalColumn: "CwiczenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HCwiczenia_harm_trening_HTreningId",
                        column: x => x.HTreningId,
                        principalTable: "harm_trening",
                        principalColumn: "HTreningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "harm_serie",
                columns: table => new
                {
                    HSeriaId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    HCwiczenieId = table.Column<int>(nullable: false),
                    SeriaId = table.Column<int>(nullable: false),
                    IloscPowtorzen = table.Column<int>(nullable: false),
                    Obciazenie = table.Column<double>(nullable: false),
                    Czas = table.Column<int>(nullable: false),
                    Dystans = table.Column<double>(nullable: false),
                    Odpoczynek = table.Column<int>(nullable: false),
                    Intensywnosc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_harm_serie", x => x.HSeriaId);
                    table.ForeignKey(
                        name: "FK_harm_serie_HCwiczenia_HCwiczenieId",
                        column: x => x.HCwiczenieId,
                        principalTable: "HCwiczenia",
                        principalColumn: "HCwiczenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_harm_serie_serie_SeriaId",
                        column: x => x.SeriaId,
                        principalTable: "serie",
                        principalColumn: "SeriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_harm_serie_HCwiczenieId",
                table: "harm_serie",
                column: "HCwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_harm_serie_SeriaId",
                table: "harm_serie",
                column: "SeriaId");

            migrationBuilder.CreateIndex(
                name: "IX_HCwiczenia_CwiczenieId",
                table: "HCwiczenia",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_HCwiczenia_HTreningId",
                table: "HCwiczenia",
                column: "HTreningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "harm_serie");

            migrationBuilder.DropTable(
                name: "HCwiczenia");
        }
    }
}
