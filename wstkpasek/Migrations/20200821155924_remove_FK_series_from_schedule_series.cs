using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace wstkpasek.Migrations
{
    public partial class remove_FK_series_from_schedule_series : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedule_series_series_SeriesId",
                table: "schedule_series");

            migrationBuilder.DropTable(
                name: "exercise_series");

            migrationBuilder.DropIndex(
                name: "IX_schedule_series_SeriesId",
                table: "schedule_series");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "schedule_series");

            migrationBuilder.CreateIndex(
                name: "IX_series_ExerciseId",
                table: "series",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_series_exercises_ExerciseId",
                table: "series",
                column: "ExerciseId",
                principalTable: "exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_series_exercises_ExerciseId",
                table: "series");

            migrationBuilder.DropIndex(
                name: "IX_series_ExerciseId",
                table: "series");

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "schedule_series",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "exercise_series",
                columns: table => new
                {
                    ExerciseSeriesId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    SeriesId = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_series", x => x.ExerciseSeriesId);
                    table.ForeignKey(
                        name: "FK_exercise_series_exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercise_series_series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "series",
                        principalColumn: "SeriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_schedule_series_SeriesId",
                table: "schedule_series",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_exercise_series_ExerciseId",
                table: "exercise_series",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_exercise_series_SeriesId",
                table: "exercise_series",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_schedule_series_series_SeriesId",
                table: "schedule_series",
                column: "SeriesId",
                principalTable: "series",
                principalColumn: "SeriesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
