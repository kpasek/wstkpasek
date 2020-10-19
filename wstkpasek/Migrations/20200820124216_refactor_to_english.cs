using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace wstkpasek.Migrations
{
    public partial class refactor_to_english : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cwiczenie_seria");

            migrationBuilder.DropTable(
                name: "harm_serie");

            migrationBuilder.DropTable(
                name: "profil");

            migrationBuilder.DropTable(
                name: "profil_waga");

            migrationBuilder.DropTable(
                name: "trening_cwiczenie");

            migrationBuilder.DropTable(
                name: "ustawienia");

            migrationBuilder.DropTable(
                name: "harm_cwiczenia");

            migrationBuilder.DropTable(
                name: "serie");

            migrationBuilder.DropTable(
                name: "trening");

            migrationBuilder.DropTable(
                name: "cwiczenia");

            migrationBuilder.DropTable(
                name: "harm_trening");

            migrationBuilder.DropTable(
                name: "cwiczenie_partia");

            migrationBuilder.DropTable(
                name: "cwiczenie_rodzaj");

            migrationBuilder.CreateTable(
                name: "exercise_parts",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    UserEmail = table.Column<string>(nullable: true),
                    Public = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_parts", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "exercise_types",
                columns: table => new
                {
                    TypeName = table.Column<string>(nullable: false),
                    UserEmail = table.Column<string>(nullable: true),
                    Public = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_types", x => x.TypeName);
                });

            migrationBuilder.CreateTable(
                name: "profile",
                columns: table => new
                {
                    ProfileId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "schedule_trainings",
                columns: table => new
                {
                    ScheduleTrainingId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    TrainingDate = table.Column<DateTime>(nullable: false),
                    TrainingFinishDate = table.Column<DateTime>(nullable: false),
                    TrainingId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ExerciseNumber = table.Column<int>(nullable: false),
                    Finish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_trainings", x => x.ScheduleTrainingId);
                });

            migrationBuilder.CreateTable(
                name: "series",
                columns: table => new
                {
                    SeriesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Repeats = table.Column<int>(nullable: false),
                    Load = table.Column<double>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    RestTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_series", x => x.SeriesId);
                });

            migrationBuilder.CreateTable(
                name: "trainings",
                columns: table => new
                {
                    TrainingId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Repeats = table.Column<int>(nullable: false),
                    Public = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainings", x => x.TrainingId);
                });

            migrationBuilder.CreateTable(
                name: "user_settings",
                columns: table => new
                {
                    SettingsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    ColorTheme = table.Column<int>(nullable: false),
                    TrainingHour = table.Column<int>(nullable: false),
                    TrainingMinute = table.Column<int>(nullable: false),
                    TrainingDayInterval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_settings", x => x.SettingsId);
                });

            migrationBuilder.CreateTable(
                name: "user_weight",
                columns: table => new
                {
                    WeightId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    WeightIdKg = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_weight", x => x.WeightId);
                });

            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    Public = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PartId = table.Column<string>(nullable: true),
                    TypeId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.ExerciseId);
                    table.ForeignKey(
                        name: "FK_exercises_exercise_parts_PartId",
                        column: x => x.PartId,
                        principalTable: "exercise_parts",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_exercises_exercise_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "exercise_types",
                        principalColumn: "TypeName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "exercise_series",
                columns: table => new
                {
                    ExerciseSeriesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    ExerciseId = table.Column<int>(nullable: false),
                    SeriesId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "schedule_exercise",
                columns: table => new
                {
                    ScheduleExerciseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    ScheduleTrainingId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Started = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_exercise", x => x.ScheduleExerciseId);
                    table.ForeignKey(
                        name: "FK_schedule_exercise_exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedule_exercise_schedule_trainings_ScheduleTrainingId",
                        column: x => x.ScheduleTrainingId,
                        principalTable: "schedule_trainings",
                        principalColumn: "ScheduleTrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainings_exercises",
                columns: table => new
                {
                    TrainingExerciseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    TrainingId = table.Column<int>(nullable: false),
                    ExerciseId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainings_exercises", x => x.TrainingExerciseId);
                    table.ForeignKey(
                        name: "FK_trainings_exercises_exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trainings_exercises_trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "trainings",
                        principalColumn: "TrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedule_series",
                columns: table => new
                {
                    ScheduleSeriesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ScheduleExerciseId = table.Column<int>(nullable: false),
                    SeriesId = table.Column<int>(nullable: false),
                    Repeats = table.Column<int>(nullable: false),
                    Load = table.Column<double>(nullable: false),
                    Time = table.Column<int>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    RestTime = table.Column<int>(nullable: false),
                    Intensity = table.Column<int>(nullable: false),
                    Finish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_series", x => x.ScheduleSeriesId);
                    table.ForeignKey(
                        name: "FK_schedule_series_schedule_exercise_ScheduleExerciseId",
                        column: x => x.ScheduleExerciseId,
                        principalTable: "schedule_exercise",
                        principalColumn: "ScheduleExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedule_series_series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "series",
                        principalColumn: "SeriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exercise_series_ExerciseId",
                table: "exercise_series",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_exercise_series_SeriesId",
                table: "exercise_series",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_exercises_PartId",
                table: "exercises",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_exercises_TypeId",
                table: "exercises",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exercise_ExerciseId",
                table: "schedule_exercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exercise_ScheduleTrainingId",
                table: "schedule_exercise",
                column: "ScheduleTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_series_ScheduleExerciseId",
                table: "schedule_series",
                column: "ScheduleExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_series_SeriesId",
                table: "schedule_series",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_trainings_exercises_ExerciseId",
                table: "trainings_exercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_trainings_exercises_TrainingId",
                table: "trainings_exercises",
                column: "TrainingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exercise_series");

            migrationBuilder.DropTable(
                name: "profile");

            migrationBuilder.DropTable(
                name: "schedule_series");

            migrationBuilder.DropTable(
                name: "trainings_exercises");

            migrationBuilder.DropTable(
                name: "user_settings");

            migrationBuilder.DropTable(
                name: "user_weight");

            migrationBuilder.DropTable(
                name: "schedule_exercise");

            migrationBuilder.DropTable(
                name: "series");

            migrationBuilder.DropTable(
                name: "trainings");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "schedule_trainings");

            migrationBuilder.DropTable(
                name: "exercise_parts");

            migrationBuilder.DropTable(
                name: "exercise_types");

            migrationBuilder.CreateTable(
                name: "cwiczenie_partia",
                columns: table => new
                {
                    Nazwa = table.Column<string>(type: "text", nullable: false),
                    Publiczne = table.Column<bool>(type: "boolean", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cwiczenie_partia", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "cwiczenie_rodzaj",
                columns: table => new
                {
                    Nazwa = table.Column<string>(type: "text", nullable: false),
                    Publiczne = table.Column<bool>(type: "boolean", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cwiczenie_rodzaj", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "harm_trening",
                columns: table => new
                {
                    HTreningId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataTreningu = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataZakonczenia = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IleCwiczen = table.Column<int>(type: "integer", nullable: false),
                    Nazwa = table.Column<string>(type: "text", nullable: true),
                    TreningId = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    Zakonczony = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_harm_trening", x => x.HTreningId);
                });

            migrationBuilder.CreateTable(
                name: "profil",
                columns: table => new
                {
                    ProfilId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataUrodzenia = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Imie = table.Column<string>(type: "text", nullable: true),
                    Nazwisko = table.Column<string>(type: "text", nullable: true),
                    Plec = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profil", x => x.ProfilId);
                });

            migrationBuilder.CreateTable(
                name: "profil_waga",
                columns: table => new
                {
                    WagaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    WagaKG = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profil_waga", x => x.WagaId);
                });

            migrationBuilder.CreateTable(
                name: "serie",
                columns: table => new
                {
                    SeriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Czas = table.Column<int>(type: "integer", nullable: false),
                    Dystans = table.Column<double>(type: "double precision", nullable: false),
                    IloscPowtorzen = table.Column<int>(type: "integer", nullable: false),
                    Nazwa = table.Column<string>(type: "text", nullable: true),
                    Obciazenie = table.Column<double>(type: "double precision", nullable: false),
                    Odpoczynek = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serie", x => x.SeriaId);
                });

            migrationBuilder.CreateTable(
                name: "trening",
                columns: table => new
                {
                    TreningId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IleCwiczen = table.Column<int>(type: "integer", nullable: false),
                    Nazwa = table.Column<string>(type: "text", nullable: true),
                    Publiczne = table.Column<bool>(type: "boolean", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trening", x => x.TreningId);
                });

            migrationBuilder.CreateTable(
                name: "ustawienia",
                columns: table => new
                {
                    UstawienieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoIleDniTrening = table.Column<int>(type: "integer", nullable: false),
                    ColorTheme = table.Column<int>(type: "integer", nullable: false),
                    GodzinaTreningu = table.Column<int>(type: "integer", nullable: false),
                    MinutaTreningu = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ustawienia", x => x.UstawienieId);
                });

            migrationBuilder.CreateTable(
                name: "cwiczenia",
                columns: table => new
                {
                    CwiczenieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(type: "text", nullable: true),
                    Opis = table.Column<string>(type: "text", nullable: true),
                    PartiaId = table.Column<string>(type: "text", nullable: true),
                    Publiczne = table.Column<bool>(type: "boolean", nullable: false),
                    RodzajId = table.Column<string>(type: "text", nullable: true),
                    UserEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cwiczenia", x => x.CwiczenieId);
                    table.ForeignKey(
                        name: "FK_cwiczenia_cwiczenie_partia_PartiaId",
                        column: x => x.PartiaId,
                        principalTable: "cwiczenie_partia",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cwiczenia_cwiczenie_rodzaj_RodzajId",
                        column: x => x.RodzajId,
                        principalTable: "cwiczenie_rodzaj",
                        principalColumn: "Nazwa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cwiczenie_seria",
                columns: table => new
                {
                    CwiczenieSeriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CwiczenieId = table.Column<int>(type: "integer", nullable: false),
                    Kolejnosc = table.Column<int>(type: "integer", nullable: false),
                    SeriaId = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cwiczenie_seria", x => x.CwiczenieSeriaId);
                    table.ForeignKey(
                        name: "FK_cwiczenie_seria_cwiczenia_CwiczenieId",
                        column: x => x.CwiczenieId,
                        principalTable: "cwiczenia",
                        principalColumn: "CwiczenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cwiczenie_seria_serie_SeriaId",
                        column: x => x.SeriaId,
                        principalTable: "serie",
                        principalColumn: "SeriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "harm_cwiczenia",
                columns: table => new
                {
                    HCwiczenieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CwiczenieId = table.Column<int>(type: "integer", nullable: false),
                    HTreningId = table.Column<int>(type: "integer", nullable: false),
                    Kolejnosc = table.Column<int>(type: "integer", nullable: false),
                    Rozpoczęte = table.Column<bool>(type: "boolean", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_harm_cwiczenia", x => x.HCwiczenieId);
                    table.ForeignKey(
                        name: "FK_harm_cwiczenia_cwiczenia_CwiczenieId",
                        column: x => x.CwiczenieId,
                        principalTable: "cwiczenia",
                        principalColumn: "CwiczenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_harm_cwiczenia_harm_trening_HTreningId",
                        column: x => x.HTreningId,
                        principalTable: "harm_trening",
                        principalColumn: "HTreningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trening_cwiczenie",
                columns: table => new
                {
                    TreningCwiczenieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CwiczenieId = table.Column<int>(type: "integer", nullable: false),
                    Kolejnosc = table.Column<int>(type: "integer", nullable: false),
                    TreningId = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trening_cwiczenie", x => x.TreningCwiczenieId);
                    table.ForeignKey(
                        name: "FK_trening_cwiczenie_cwiczenia_CwiczenieId",
                        column: x => x.CwiczenieId,
                        principalTable: "cwiczenia",
                        principalColumn: "CwiczenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trening_cwiczenie_trening_TreningId",
                        column: x => x.TreningId,
                        principalTable: "trening",
                        principalColumn: "TreningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "harm_serie",
                columns: table => new
                {
                    HSeriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Czas = table.Column<int>(type: "integer", nullable: false),
                    Dystans = table.Column<double>(type: "double precision", nullable: false),
                    HCwiczenieId = table.Column<int>(type: "integer", nullable: false),
                    IloscPowtorzen = table.Column<int>(type: "integer", nullable: false),
                    Intensywnosc = table.Column<int>(type: "integer", nullable: false),
                    Kolejnosc = table.Column<int>(type: "integer", nullable: false),
                    Nazwa = table.Column<string>(type: "text", nullable: true),
                    Obciazenie = table.Column<double>(type: "double precision", nullable: false),
                    Odpoczynek = table.Column<int>(type: "integer", nullable: false),
                    SeriaId = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    Zakonczone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_harm_serie", x => x.HSeriaId);
                    table.ForeignKey(
                        name: "FK_harm_serie_harm_cwiczenia_HCwiczenieId",
                        column: x => x.HCwiczenieId,
                        principalTable: "harm_cwiczenia",
                        principalColumn: "HCwiczenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_harm_serie_serie_SeriaId",
                        column: x => x.SeriaId,
                        principalTable: "serie",
                        principalColumn: "SeriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cwiczenia_PartiaId",
                table: "cwiczenia",
                column: "PartiaId");

            migrationBuilder.CreateIndex(
                name: "IX_cwiczenia_RodzajId",
                table: "cwiczenia",
                column: "RodzajId");

            migrationBuilder.CreateIndex(
                name: "IX_cwiczenie_seria_CwiczenieId",
                table: "cwiczenie_seria",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_cwiczenie_seria_SeriaId",
                table: "cwiczenie_seria",
                column: "SeriaId");

            migrationBuilder.CreateIndex(
                name: "IX_harm_cwiczenia_CwiczenieId",
                table: "harm_cwiczenia",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_harm_cwiczenia_HTreningId",
                table: "harm_cwiczenia",
                column: "HTreningId");

            migrationBuilder.CreateIndex(
                name: "IX_harm_serie_HCwiczenieId",
                table: "harm_serie",
                column: "HCwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_harm_serie_SeriaId",
                table: "harm_serie",
                column: "SeriaId");

            migrationBuilder.CreateIndex(
                name: "IX_trening_cwiczenie_CwiczenieId",
                table: "trening_cwiczenie",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_trening_cwiczenie_TreningId",
                table: "trening_cwiczenie",
                column: "TreningId");
        }
    }
}
