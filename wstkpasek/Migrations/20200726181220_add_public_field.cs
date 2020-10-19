using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class add_public_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zaakceptowane",
                table: "cwiczenie_rodzaj");

            migrationBuilder.DropColumn(
                name: "Zaakceptowane",
                table: "cwiczenie_partia");

            migrationBuilder.DropColumn(
                name: "CzyZaakceptowane",
                table: "cwiczenia");

            migrationBuilder.AddColumn<bool>(
                name: "Publiczne",
                table: "trening",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Publiczne",
                table: "cwiczenie_rodzaj",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Publiczne",
                table: "cwiczenie_partia",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Publiczne",
                table: "cwiczenia",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publiczne",
                table: "trening");

            migrationBuilder.DropColumn(
                name: "Publiczne",
                table: "cwiczenie_rodzaj");

            migrationBuilder.DropColumn(
                name: "Publiczne",
                table: "cwiczenie_partia");

            migrationBuilder.DropColumn(
                name: "Publiczne",
                table: "cwiczenia");

            migrationBuilder.AddColumn<bool>(
                name: "Zaakceptowane",
                table: "cwiczenie_rodzaj",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Zaakceptowane",
                table: "cwiczenie_partia",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CzyZaakceptowane",
                table: "cwiczenia",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
