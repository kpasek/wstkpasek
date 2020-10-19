using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace wstkpasek.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cwiczenie_partia",
                columns: table => new
                {
                    Nazwa = table.Column<string>(nullable: false),
                    Zaakceptowane = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cwiczenie_partia", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "cwiczenie_rodzaj",
                columns: table => new
                {
                    Nazwa = table.Column<string>(nullable: false),
                    Zaakceptowane = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cwiczenie_rodzaj", x => x.Nazwa);
                });

            migrationBuilder.CreateTable(
                name: "profil",
                columns: table => new
                {
                    ProfilId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(nullable: true),
                    Imie = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    Plec = table.Column<string>(nullable: true),
                    DataUrodzenia = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profil", x => x.ProfilId);
                });

            migrationBuilder.CreateTable(
                name: "profil_waga",
                columns: table => new
                {
                    WagaId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    WagaKG = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profil_waga", x => x.WagaId);
                });

            migrationBuilder.CreateTable(
                name: "serie",
                columns: table => new
                {
                    SeriaId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    Nazwa = table.Column<string>(nullable: true),
                    IloscPowtorzen = table.Column<int>(nullable: false),
                    Obciazenie = table.Column<double>(nullable: false),
                    Czas = table.Column<int>(nullable: false),
                    Dystans = table.Column<double>(nullable: false),
                    DystansPlan = table.Column<double>(nullable: false),
                    Odpoczynek = table.Column<int>(nullable: false),
                    IloscPowtorzenPlan = table.Column<int>(nullable: false),
                    CzasPlan = table.Column<int>(nullable: false),
                    Intensywnosc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_serie", x => x.SeriaId);
                });

            migrationBuilder.CreateTable(
                name: "trening",
                columns: table => new
                {
                    TreningId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trening", x => x.TreningId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cwiczenia",
                columns: table => new
                {
                    CwiczenieId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    CzyZaakceptowane = table.Column<bool>(nullable: false),
                    Nazwa = table.Column<string>(nullable: true),
                    PartiaId = table.Column<string>(nullable: true),
                    RodzajId = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
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
                    CwiczenieSeriaId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: true),
                    CwiczenieId = table.Column<int>(nullable: false),
                    SeriaId = table.Column<int>(nullable: false)
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
                name: "trening_cwiczenie",
                columns: table => new
                {
                    TreningCwiczenieId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(nullable: false),
                    TreningId = table.Column<int>(nullable: false),
                    CwiczenieId = table.Column<int>(nullable: false)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
                name: "IX_trening_cwiczenie_CwiczenieId",
                table: "trening_cwiczenie",
                column: "CwiczenieId");

            migrationBuilder.CreateIndex(
                name: "IX_trening_cwiczenie_TreningId",
                table: "trening_cwiczenie",
                column: "TreningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "cwiczenie_seria");

            migrationBuilder.DropTable(
                name: "profil");

            migrationBuilder.DropTable(
                name: "profil_waga");

            migrationBuilder.DropTable(
                name: "trening_cwiczenie");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "serie");

            migrationBuilder.DropTable(
                name: "cwiczenia");

            migrationBuilder.DropTable(
                name: "trening");

            migrationBuilder.DropTable(
                name: "cwiczenie_partia");

            migrationBuilder.DropTable(
                name: "cwiczenie_rodzaj");
        }
    }
}
