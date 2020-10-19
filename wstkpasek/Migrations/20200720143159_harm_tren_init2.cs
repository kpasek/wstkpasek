using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace wstkp.Migrations
{
    public partial class harm_tren_init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "harm_trening",
                columns: table => new
                {
                    HTreningId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    DataTreningu = table.Column<DateTime>(nullable: false),
                    DataZakonczenia = table.Column<DateTime>(nullable: false),
                    TreningId = table.Column<int>(nullable: false),
                    Nazwa = table.Column<string>(nullable: true),
                    Zakonczony = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_harm_trening", x => x.HTreningId);
                    table.ForeignKey(
                        name: "FK_harm_trening_trening_TreningId",
                        column: x => x.TreningId,
                        principalTable: "trening",
                        principalColumn: "TreningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_harm_trening_TreningId",
                table: "harm_trening",
                column: "TreningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "harm_trening");
        }
    }
}
