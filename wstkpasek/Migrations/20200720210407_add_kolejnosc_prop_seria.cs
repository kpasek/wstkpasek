using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class add_kolejnosc_prop_seria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "serie",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "harm_serie",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "serie");

            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "harm_serie");
        }
    }
}
