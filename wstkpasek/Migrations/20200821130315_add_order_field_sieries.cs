using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkp.Migrations
{
    public partial class add_order_field_sieries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "series",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "series");
        }
    }
}
