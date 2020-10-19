using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class update_seria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CzasPlan",
                table: "serie");

            migrationBuilder.DropColumn(
                name: "DystansPlan",
                table: "serie");

            migrationBuilder.DropColumn(
                name: "IloscPowtorzenPlan",
                table: "serie");

            migrationBuilder.DropColumn(
                name: "Intensywnosc",
                table: "serie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CzasPlan",
                table: "serie",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DystansPlan",
                table: "serie",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "IloscPowtorzenPlan",
                table: "serie",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intensywnosc",
                table: "serie",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
