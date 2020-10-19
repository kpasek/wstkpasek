using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class add_rozpoczete_hcwiczenie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Rozpoczęte",
                table: "harm_cwiczenia",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rozpoczęte",
                table: "harm_cwiczenia");
        }
    }
}
