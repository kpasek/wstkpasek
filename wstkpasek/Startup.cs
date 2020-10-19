using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using wstkpasek.Models.Database;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.Schedule.Exercise;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.SeriesModel;
using wstkpasek.Models.TrainingModel;
using wstkpasek.Models.User;

namespace wstkpasek
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AppDBContext>();
      services.AddCors();
      services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
        option =>
        {
          option.Password.RequireDigit = true;
          option.Password.RequiredLength = 6;
          option.Password.RequireNonAlphanumeric = false;
          option.Password.RequireLowercase = true;
          option.Password.RequireUppercase = false;
          option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
          option.Lockout.MaxFailedAccessAttempts = 5;
          option.Lockout.AllowedForNewUsers = true;
          option.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();

      services.ConfigureApplicationCookie(config =>
      {
        config.Cookie.Name = "token.cookie";
        config.LoginPath = "/user/login";
      });

      //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      //services.AddControllersWithViews();

      services.AddTransient<IExerciseRepository, ExerciseRepository>();
      services.AddTransient<ISeriesRepository, SeriesRepository>();
      services.AddTransient<IScheduleSeriesRepository, ScheduleSeriesRepository>();
      services.AddTransient<ITrainingRepository, TrainingRepository>();
      services.AddTransient<IScheduleTrainingRepository, ScheduleTrainingRepository>();
      services.AddTransient<IScheduleExerciseRepository, ScheduleExerciseRepository>();
      services.AddTransient<IUserRepository, UserRepository>();

      services.AddControllersWithViews();

      // In production, the React files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/build";
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      app.UseRouting();
      app.UseCors(b =>
      {
        b.WithOrigins("localhost");
        b.AllowAnyMethod();
        b.AllowAnyHeader();
      });
      app.UseAuthentication();

      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action=Index}/{id?}");
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = "ClientApp";

        if (env.IsDevelopment())
        {
          spa.UseReactDevelopmentServer(npmScript: "start");
        }
      });
    }
  }
}
