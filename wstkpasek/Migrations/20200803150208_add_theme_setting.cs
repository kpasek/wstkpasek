using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class add_theme_setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorTheme",
                table: "ustawienia",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorTheme",
                table: "ustawienia");
        }
    }
}
