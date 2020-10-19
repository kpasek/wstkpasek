using Microsoft.EntityFrameworkCore.Migrations;

namespace wstkpasek.Migrations
{
    public partial class rename_exercise_number_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repeats",
                table: "trainings");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseNumber",
                table: "trainings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseNumber",
                table: "trainings");

            migrationBuilder.AddColumn<int>(
                name: "Repeats",
                table: "trainings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
