using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "user_settings",
                maxLength: 6,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessegesCode = table.Column<string>(maxLength: 30, nullable: false),
                    Language = table.Column<string>(maxLength: 6, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Type = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessegesCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "user_settings");
        }
    }
}
