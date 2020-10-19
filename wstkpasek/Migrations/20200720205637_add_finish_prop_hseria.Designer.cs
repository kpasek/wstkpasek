﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using wstkp.Models.Database;

namespace wstkp.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20200720205637_add_finish_prop_hseria")]
    partial class add_finish_prop_hseria
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("kptr.Models.Cwiczenia.Cwiczenie", b =>
                {
                    b.Property<int>("CwiczenieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("CzyZaakceptowane")
                        .HasColumnType("boolean");

                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.Property<string>("Opis")
                        .HasColumnType("text");

                    b.Property<string>("PartiaId")
                        .HasColumnType("text");

                    b.Property<string>("RodzajId")
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CwiczenieId");

                    b.HasIndex("PartiaId");

                    b.HasIndex("RodzajId");

                    b.ToTable("cwiczenia");
                });

            modelBuilder.Entity("kptr.Models.Cwiczenia.CwiczenieSeria", b =>
                {
                    b.Property<int>("CwiczenieSeriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CwiczenieId")
                        .HasColumnType("integer");

                    b.Property<int>("SeriaId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.HasKey("CwiczenieSeriaId");

                    b.HasIndex("CwiczenieId");

                    b.HasIndex("SeriaId");

                    b.ToTable("cwiczenie_seria");
                });

            modelBuilder.Entity("kptr.Models.Cwiczenia.Partia", b =>
                {
                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.Property<bool>("Zaakceptowane")
                        .HasColumnType("boolean");

                    b.HasKey("Nazwa");

                    b.ToTable("cwiczenie_partia");
                });

            modelBuilder.Entity("kptr.Models.Cwiczenia.Rodzaj", b =>
                {
                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.Property<bool>("Zaakceptowane")
                        .HasColumnType("boolean");

                    b.HasKey("Nazwa");

                    b.ToTable("cwiczenie_rodzaj");
                });

            modelBuilder.Entity("kptr.Models.Harmonogram.HCwiczenia.HCwiczenie", b =>
                {
                    b.Property<int>("HCwiczenieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CwiczenieId")
                        .HasColumnType("integer");

                    b.Property<int>("HTreningId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("HCwiczenieId");

                    b.HasIndex("CwiczenieId");

                    b.HasIndex("HTreningId");

                    b.ToTable("harm_cwiczenia");
                });

            modelBuilder.Entity("kptr.Models.Harmonogram.HSerie.HSeria", b =>
                {
                    b.Property<int>("HSeriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Czas")
                        .HasColumnType("integer");

                    b.Property<double>("Dystans")
                        .HasColumnType("double precision");

                    b.Property<int>("HCwiczenieId")
                        .HasColumnType("integer");

                    b.Property<int>("IloscPowtorzen")
                        .HasColumnType("integer");

                    b.Property<int>("Intensywnosc")
                        .HasColumnType("integer");

                    b.Property<double>("Obciazenie")
                        .HasColumnType("double precision");

                    b.Property<int>("Odpoczynek")
                        .HasColumnType("integer");

                    b.Property<int>("SeriaId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Zakonczone")
                        .HasColumnType("boolean");

                    b.HasKey("HSeriaId");

                    b.HasIndex("HCwiczenieId");

                    b.HasIndex("SeriaId");

                    b.ToTable("harm_serie");
                });

            modelBuilder.Entity("kptr.Models.Harmonogram.HTreningi.HTrening", b =>
                {
                    b.Property<int>("HTreningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataTreningu")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataZakonczenia")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.Property<int>("TreningId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Zakonczony")
                        .HasColumnType("boolean");

                    b.HasKey("HTreningId");

                    b.HasIndex("TreningId");

                    b.ToTable("harm_trening");
                });

            modelBuilder.Entity("kptr.Models.Serie.Seria", b =>
                {
                    b.Property<int>("SeriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Czas")
                        .HasColumnType("integer");

                    b.Property<double>("Dystans")
                        .HasColumnType("double precision");

                    b.Property<int>("IloscPowtorzen")
                        .HasColumnType("integer");

                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.Property<double>("Obciazenie")
                        .HasColumnType("double precision");

                    b.Property<int>("Odpoczynek")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SeriaId");

                    b.ToTable("serie");
                });

            modelBuilder.Entity("kptr.Models.Treningi.Trening", b =>
                {
                    b.Property<int>("TreningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Nazwa")
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.HasKey("TreningId");

                    b.ToTable("trening");
                });

            modelBuilder.Entity("kptr.Models.Treningi.TreningCwiczenie", b =>
                {
                    b.Property<int>("TreningCwiczenieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CwiczenieId")
                        .HasColumnType("integer");

                    b.Property<int>("TreningId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TreningCwiczenieId");

                    b.HasIndex("CwiczenieId");

                    b.HasIndex("TreningId");

                    b.ToTable("trening_cwiczenie");
                });

            modelBuilder.Entity("kptr.Models.User.Profil", b =>
                {
                    b.Property<int>("ProfilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataUrodzenia")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Imie")
                        .HasColumnType("text");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("text");

                    b.Property<string>("Plec")
                        .HasColumnType("text");

                    b.HasKey("ProfilId");

                    b.ToTable("profil");
                });

            modelBuilder.Entity("kptr.Models.User.Waga", b =>
                {
                    b.Property<int>("WagaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.Property<double>("WagaKG")
                        .HasColumnType("double precision");

                    b.HasKey("WagaId");

                    b.ToTable("profil_waga");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("kptr.Models.Cwiczenia.Cwiczenie", b =>
                {
                    b.HasOne("kptr.Models.Cwiczenia.Partia", "Partia")
                        .WithMany()
                        .HasForeignKey("PartiaId");

                    b.HasOne("kptr.Models.Cwiczenia.Rodzaj", "Rodzaj")
                        .WithMany()
                        .HasForeignKey("RodzajId");
                });

            modelBuilder.Entity("kptr.Models.Cwiczenia.CwiczenieSeria", b =>
                {
                    b.HasOne("kptr.Models.Cwiczenia.Cwiczenie", "Cwiczenie")
                        .WithMany()
                        .HasForeignKey("CwiczenieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kptr.Models.Serie.Seria", "Seria")
                        .WithMany()
                        .HasForeignKey("SeriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("kptr.Models.Harmonogram.HCwiczenia.HCwiczenie", b =>
                {
                    b.HasOne("kptr.Models.Cwiczenia.Cwiczenie", "Cwiczenie")
                        .WithMany()
                        .HasForeignKey("CwiczenieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kptr.Models.Harmonogram.HTreningi.HTrening", "HTrening")
                        .WithMany()
                        .HasForeignKey("HTreningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("kptr.Models.Harmonogram.HSerie.HSeria", b =>
                {
                    b.HasOne("kptr.Models.Harmonogram.HCwiczenia.HCwiczenie", "HCwiczenie")
                        .WithMany()
                        .HasForeignKey("HCwiczenieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kptr.Models.Serie.Seria", "Seria")
                        .WithMany()
                        .HasForeignKey("SeriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("kptr.Models.Harmonogram.HTreningi.HTrening", b =>
                {
                    b.HasOne("kptr.Models.Treningi.Trening", "Trening")
                        .WithMany()
                        .HasForeignKey("TreningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("kptr.Models.Treningi.TreningCwiczenie", b =>
                {
                    b.HasOne("kptr.Models.Cwiczenia.Cwiczenie", "Cwiczenie")
                        .WithMany()
                        .HasForeignKey("CwiczenieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kptr.Models.Treningi.Trening", "Trening")
                        .WithMany()
                        .HasForeignKey("TreningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
