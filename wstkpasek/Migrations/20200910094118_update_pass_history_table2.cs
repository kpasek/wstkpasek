using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class update_pass_history_table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_password_change_history",
                table: "log_password_change");

            migrationBuilder.RenameTable(
                name: "log_password_change",
                newName: "log_password_change");

            migrationBuilder.AddPrimaryKey(
                name: "PK_log_password_change",
                table: "log_password_change",
                column: "PasswordChangedHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_password_change_history",
                table: "log_password_change");

            migrationBuilder.RenameTable(
                name: "log_password_change",
                newName: "log_password_change");

            migrationBuilder.AddPrimaryKey(
                name: "PK_log_password_change",
                table: "log_password_change",
                column: "PasswordChangedHistoryId");
        }
    }
}
