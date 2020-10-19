using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using wstkpasek.Models;
using wstkpasek.Models.Database;

using wstkpasek.Models.User;
using wstkpasek.Models.Resources;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.SeriesModel;
using wstkpasek.Models.TrainingModel;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.Schedule.Exercise;
using wstkpasek.Models.Schedule.Series;

namespace wstkpasek.Models.Database
{
  public class AppDBContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
  {
    private readonly string connectionString = $"Host={DbConfig.Host};Database={DbConfig.Database};Username={DbConfig.User};Password={DbConfig.Password}";
    public AppDBContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<MyMessage> Messages { get; set; }
    public DbSet<Schedule.Exercise.ScheduleExercise> ScheduleExercises { get; set; }
    public DbSet<ScheduleSeries> ScheduleSeries { get; set; }
    public DbSet<ScheduleTraining> ScheduleTrainings { get; set; }
    public DbSet<Weight> Weights { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<TrainingExercise> TrainingExercises { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Series> Series { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<PasswordChangedHistory> PasswordChangedHistories { get; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      #region "Seed Data"
      builder.Entity<IdentityRole<int>>().HasData(
        new { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
        new { Id = 2, Name = "User", NormalizedName = "USER" }
        );
      #endregion

      base.OnModelCreating(builder);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql(connectionString);
    public DbSet<wstkpasek.Models.Exercises.ScheduleExercie> ScheduleExercie { get; set; }
  }
}

