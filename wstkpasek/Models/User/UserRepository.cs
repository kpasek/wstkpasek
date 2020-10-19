using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wstkpasek.Models.Database;
using wstkpasek.Models.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace wstkpasek.Models.User
{
  public class UserRepository : IUserRepository
  {
    private readonly AppDBContext db;

    public UserRepository(AppDBContext db)
    {
      this.db = db;
    }

    public async Task<Weight> GetLastWeightAsync(string email)
    {
      var weight = db.Weights
        .Where(w => w.UserEmail == email)
        .OrderByDescending(o => o.Date)
        .Take(1);
      return await weight.AnyAsync() ? await weight.SingleAsync() : new Weight();
    }

    public async Task<MyMessage> GetMessage(string code, string language)
    {
      var m = await db.Messages.SingleAsync(s => s.MessegesCode == code && s.Language == language);
      return m ?? new MyMessage { Message = "ERROR: Message not found" };
    }

    public async Task<List<MyMessage>> GetMessagesAsync()
    {
      return await db.Messages.ToListAsync();
    }

    public async Task<Profile> GetProfile(string email)
    {
      var profile = await db.Profile.SingleAsync(s => s.Email == email);
      return profile;
    }

    public Task<Settings> GetSettings(string email)
    {
      var settings = db.Settings.SingleAsync(s => s.UserEmail == email);
      return settings;
    }

    public async Task<IdentityUser<int>> GetUserAsync(string email)
    {
      var user = await db.Users.SingleAsync(s => s.Email == email);
      return user;
    }

    public async Task<string> GetUserLanguage(string email)
    {
      var lan = await db.Settings.SingleAsync(s => s.UserEmail == email);
      return lan != null ? lan.Language : "pl_PL";
    }

    public async Task<List<IdentityUser<int>>> GetUsersAsync(string email, string name, string lastName)
    {
      if (string.IsNullOrWhiteSpace(email + name + lastName)) return await db.Users.OrderBy(o => o.Id).Take(50).ToListAsync();
      List<string> emailsFromProfile = new List<string>();

      var u = db.Profile.Where(w => EF.Functions.ILike(w.Name, $"%{(string.IsNullOrWhiteSpace(name) ? "ZZZZZZZZZZZZZZZZZZZZ" : name)}%") || EF.Functions.ILike(w.LastName, $"%{(string.IsNullOrWhiteSpace(lastName) ? "ZZZZZZZZZZZZZZZZZZZZZZZ" : lastName)}%"));
      emailsFromProfile = await u.AnyAsync() ? await u.Select(s => s.Email).ToListAsync() : new List<string>() { "NieZnaleziono" };

      var usersByEmail = db.Users.Where(w => EF.Functions.ILike(w.UserName, $"%{(string.IsNullOrWhiteSpace(email) ? "ZZZZZZZZZZZZZ" : email)}%") || emailsFromProfile.Contains(w.UserName)).OrderBy(o => o.Id);
      return await usersByEmail.AnyAsync() ? await usersByEmail.ToListAsync() : null;
    }

    public async Task<List<Weight>> GetWeights(string email)
    {
      var w = db.Weights.Where(w => w.UserEmail == email);
      return await w.AnyAsync() ? await w.ToListAsync() : new List<Weight>();
    }
  }
}