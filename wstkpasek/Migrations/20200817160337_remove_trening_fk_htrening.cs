using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class remove_trening_fk_htrening : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_harm_trening_trening_TreningId",
                table: "harm_trening");

            migrationBuilder.DropIndex(
                name: "IX_harm_trening_TreningId",
                table: "harm_trening");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_harm_trening_TreningId",
                table: "harm_trening",
                column: "TreningId");

            migrationBuilder.AddForeignKey(
                name: "FK_harm_trening_trening_TreningId",
                table: "harm_trening",
                column: "TreningId",
                principalTable: "trening",
                principalColumn: "TreningId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
