using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace wstkp.Migrations
{
    public partial class add_user_settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ustawienia",
                columns: table => new
                {
                    UstawienieId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    GodzinaTreningu = table.Column<int>(nullable: false),
                    MinutaTreningu = table.Column<int>(nullable: false),
                    CoIleDniTrening = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ustawienia", x => x.UstawienieId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ustawienia");
        }
    }
}
