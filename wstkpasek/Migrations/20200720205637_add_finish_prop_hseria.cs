using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class add_finish_prop_hseria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Zakonczone",
                table: "harm_serie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "harm_cwiczenia",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zakonczone",
                table: "harm_serie");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "harm_cwiczenia",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
