using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class update_kolejnosc_prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "cwiczenia");

            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "trening_cwiczenie",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "trening_cwiczenie");

            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "cwiczenia",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
