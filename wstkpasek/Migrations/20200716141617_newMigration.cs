using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "cwiczenie_rodzaj",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "cwiczenie_partia",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "cwiczenie_rodzaj");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "cwiczenie_partia");
        }
    }
}
