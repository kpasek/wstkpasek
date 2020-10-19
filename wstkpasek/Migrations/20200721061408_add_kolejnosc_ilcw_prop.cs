using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class add_kolejnosc_ilcw_prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IleCwiczen",
                table: "trening",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IleCwiczen",
                table: "harm_trening",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "harm_cwiczenia",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kolejnosc",
                table: "cwiczenia",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IleCwiczen",
                table: "trening");

            migrationBuilder.DropColumn(
                name: "IleCwiczen",
                table: "harm_trening");

            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "harm_cwiczenia");

            migrationBuilder.DropColumn(
                name: "Kolejnosc",
                table: "cwiczenia");
        }
    }
}
