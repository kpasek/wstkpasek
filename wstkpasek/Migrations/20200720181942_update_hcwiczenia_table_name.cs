using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class update_hcwiczenia_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_harm_serie_HCwiczenia_HCwiczenieId",
                table: "harm_serie");

            migrationBuilder.DropForeignKey(
                name: "FK_HCwiczenia_cwiczenia_CwiczenieId",
                table: "HCwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_HCwiczenia_harm_trening_HTreningId",
                table: "HCwiczenia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HCwiczenia",
                table: "HCwiczenia");

            migrationBuilder.RenameTable(
                name: "HCwiczenia",
                newName: "harm_cwiczenia");

            migrationBuilder.RenameIndex(
                name: "IX_HCwiczenia_HTreningId",
                table: "harm_cwiczenia",
                newName: "IX_harm_cwiczenia_HTreningId");

            migrationBuilder.RenameIndex(
                name: "IX_HCwiczenia_CwiczenieId",
                table: "harm_cwiczenia",
                newName: "IX_harm_cwiczenia_CwiczenieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_harm_cwiczenia",
                table: "harm_cwiczenia",
                column: "HCwiczenieId");

            migrationBuilder.AddForeignKey(
                name: "FK_harm_cwiczenia_cwiczenia_CwiczenieId",
                table: "harm_cwiczenia",
                column: "CwiczenieId",
                principalTable: "cwiczenia",
                principalColumn: "CwiczenieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_harm_cwiczenia_harm_trening_HTreningId",
                table: "harm_cwiczenia",
                column: "HTreningId",
                principalTable: "harm_trening",
                principalColumn: "HTreningId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_harm_serie_harm_cwiczenia_HCwiczenieId",
                table: "harm_serie",
                column: "HCwiczenieId",
                principalTable: "harm_cwiczenia",
                principalColumn: "HCwiczenieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_harm_cwiczenia_cwiczenia_CwiczenieId",
                table: "harm_cwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_harm_cwiczenia_harm_trening_HTreningId",
                table: "harm_cwiczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_harm_serie_harm_cwiczenia_HCwiczenieId",
                table: "harm_serie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_harm_cwiczenia",
                table: "harm_cwiczenia");

            migrationBuilder.RenameTable(
                name: "harm_cwiczenia",
                newName: "HCwiczenia");

            migrationBuilder.RenameIndex(
                name: "IX_harm_cwiczenia_HTreningId",
                table: "HCwiczenia",
                newName: "IX_HCwiczenia_HTreningId");

            migrationBuilder.RenameIndex(
                name: "IX_harm_cwiczenia_CwiczenieId",
                table: "HCwiczenia",
                newName: "IX_HCwiczenia_CwiczenieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HCwiczenia",
                table: "HCwiczenia",
                column: "HCwiczenieId");

            migrationBuilder.AddForeignKey(
                name: "FK_harm_serie_HCwiczenia_HCwiczenieId",
                table: "harm_serie",
                column: "HCwiczenieId",
                principalTable: "HCwiczenia",
                principalColumn: "HCwiczenieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HCwiczenia_cwiczenia_CwiczenieId",
                table: "HCwiczenia",
                column: "CwiczenieId",
                principalTable: "cwiczenia",
                principalColumn: "CwiczenieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HCwiczenia_harm_trening_HTreningId",
                table: "HCwiczenia",
                column: "HTreningId",
                principalTable: "harm_trening",
                principalColumn: "HTreningId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
