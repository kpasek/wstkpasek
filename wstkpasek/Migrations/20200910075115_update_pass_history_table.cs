using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class update_pass_history_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "password_change_history",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldHashedPassord",
                table: "password_change_history",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "password_change_history");

            migrationBuilder.DropColumn(
                name: "OldHashedPassord",
                table: "password_change_history");
        }
    }
}
