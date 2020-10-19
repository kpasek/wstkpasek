using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using wstkpasek.Models.Exercises;
using wstkpasek.Models.Database;
using wstkpasek.Models.Out;
using wstkpasek.Models.TrainingModel;
using wstkpasek.Models.User;
using wstkpasek.Models.In;
using wstkpasek.Models.SeriesModel;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.Schedule.Exercise;
using wstkpasek.Models.Schedule.Series;

namespace wstkpasek.Controllers
{
  [Route("api/user")]
  public class UserController : Controller
  {
    private readonly UserManager<IdentityUser<int>> userManager;
    private readonly SignInManager<IdentityUser<int>> signInManager;
    private readonly AppDBContext db;
    private readonly ITrainingRepository tr;
    private readonly IExerciseRepository er;
    private readonly IUserRepository ur;

    public UserController(UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager, AppDBContext db, ITrainingRepository tr, IExerciseRepository er, IUserRepository ur)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.db = db;
      this.tr = tr;
      this.er = er;
      this.ur = ur;
    }

    //private string GetEmail()
    //{
    //  return this.User.Identity.Name;
    //}

    [Route("register")]
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] UserRegister userRegister)
    {

      var user = new IdentityUser<int>()
      {
        UserName = userRegister.Email,
        Email = userRegister.Email
      };
      var result = await userManager.CreateAsync(user, userRegister.Password);

      if (!result.Succeeded) return BadRequest();
      var loginResult = await signInManager.PasswordSignInAsync(user, userRegister.Password, false, true);
      if (!loginResult.Succeeded) return BadRequest();
      var userClaims = new List<Claim>()
      {
        new Claim(ClaimTypes.Email, userRegister.Email),
      };

      var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

      var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

      await HttpContext.SignInAsync(userPrincipal);
      await AddStartPackage(userRegister.Email);
      await CreatUserProfile(userRegister);
      return Ok();
    }
    [Route("login")]
    [HttpPost]
    public async Task<ActionResult> LoginPost([FromBody] UserLogin userLogin)
    {

      var user = await userManager.FindByEmailAsync(userLogin.Email);

      if (user == null) return BadRequest();
      var loginResult = await signInManager.PasswordSignInAsync(user, userLogin.Password, false, true);
      if (!loginResult.Succeeded) return BadRequest();
      var profile = db.Profile.Single(p => p.Email == userLogin.Email);
      var userClaims = new List<Claim>()
      {
        new Claim(ClaimTypes.Email, userLogin.Email)
      };

      var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

      var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

      await HttpContext.SignInAsync(userPrincipal);

      return Ok();
    }

    [Route("logout")]
    [HttpGet]
    public async Task<ActionResult> Logout()
    {
      await signInManager.SignOutAsync();
      return Ok();
    }

    [Route("reset-password")]
    [HttpPost]
    public async Task<ActionResult> ResetPassword(ResetPasswordIn reset)
    {
      if (!reset.CheckPassword()) return BadRequest();
      if (this.User.IsInRole("Admin"))
      {
        var user = await userManager.FindByEmailAsync(reset.Email);
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await this.userManager.ResetPasswordAsync(user, token, reset.Password);
        if (!resetResult.Succeeded) return BadRequest();
        return Ok();
      }
      if (User.Identity.IsAuthenticated)
      {
        if (this.User.Identity.Name != reset.Email) return Redirect(reset.ReturnUrl);
        var user = await userManager.FindByEmailAsync(reset.Email);
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await this.userManager.ResetPasswordAsync(user, token, reset.Password);
        if (!resetResult.Succeeded) return BadRequest();
        return Ok();
      }
      return BadRequest();
    }

    [Route("delete-user")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUser(string email, string ReturnUrl = "/")
    {
      var user = await userManager.FindByEmailAsync(email);
      if (user == null) Redirect(ReturnUrl);
      var profile = await ur.GetProfile(user.Email);
      var settings = await ur.GetSettings(user.Email);
      var weights = await ur.GetWeights(user.Email);
      var trainings = await tr.GetTrainings(user.Email);
      var exercises = er.GetExercises(user.Email);
      if (trainings != null) db.RemoveRange(trainings);
      if (exercises != null) db.RemoveRange(exercises);
      db.Remove(profile);
      db.Remove(settings);
      if (weights != null) db.RemoveRange(weights);
      db.Remove(user);
      await db.SaveChangesAsync();
      return Ok();
    }
    [HttpGet]
    [Route("check")]
    public ActionResult Check()
    {
      if(User.Identity.IsAuthenticated)return Ok();
      return Unauthorized();
    }

    public async Task SignInUserAsync(IdentityUser<int> user, bool isPersistent, IEnumerable<Claim> customClaims)
    {
      var claimsPrincipal = await signInManager.CreateUserPrincipalAsync(user);
      if (customClaims != null && claimsPrincipal?.Identity is ClaimsIdentity claimsIdentity)
      {
        claimsIdentity.AddClaims(customClaims);
      }
      await signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme,
          claimsPrincipal,
          new AuthenticationProperties { IsPersistent = isPersistent });
    }

 
    private async Task CreatUserProfile(UserRegister user)
    {
      var profile = new Profile
      {
        Birthday = user.Birthday,
        Email = user.Email,
        Name = user.Name,
        LastName = user.LastName,
        Gender = user.Gender,
      };
      var profileTask = db.Profile.AddAsync(profile);

      var settings = new Settings
      {
        UserEmail = user.Email
      };
      var weight = new Weight
      {
        UserEmail = user.Email,
        Date = DateTime.Now,
        WeightIdKg = 0
      };
      await profileTask;
      await db.Settings.AddAsync(settings);
      await db.Weights.AddAsync(weight);
      await db.SaveChangesAsync();
    }


    private async Task AddStartPackage(string email)
    {
      var publicTrainingsTask = tr.GetPublicTrainingsAsync();

      var trainings = await publicTrainingsTask;
      var publicExerciseTask = er.GetPublicExercisesAsync();

      var newTrainings = trainings.Select(t => new Training { ExerciseNumber = t.ExerciseNumber, Name = t.Name, Public = false, UserEmail = email }).ToList();
      await db.Trainings.AddRangeAsync(newTrainings);

      var exercises = await publicExerciseTask;
      var newExercises = exercises.Select(e => new Exercise
      {
        Name = e.Name,
        Description = e.Description,
        PartId = e.PartId,
        Public = false,
        TypeId = e.TypeId,
        UserEmail = email
      })
        .ToList();
      await db.Exercises.AddRangeAsync(newExercises);
      await db.SaveChangesAsync();
    }
  }
}