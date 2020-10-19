using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class update_kolejnosc_seria_prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "serie");

            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "cwiczenie_seria",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "cwiczenie_seria");

            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "serie",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
