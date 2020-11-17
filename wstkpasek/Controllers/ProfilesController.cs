using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.User;

namespace wstkpasek.Controllers
{
  [Route("api/profiles")]
  [ApiController]
  [Authorize]
  public class ProfilesController : ControllerBase
  {
    private readonly AppDBContext _context;

    public ProfilesController(AppDBContext context)
    {
      _context = context;
    }
    /// <summary>
    /// Method return user email from provided token
    /// </summary>
    /// <returns>user email</returns>
    private string GetEmail()
    {
      return this.User.Identity.Name;
    }
    // GET: api/Profiles
    /// <summary>
    /// GET: api/profiles
    /// get user profile
    /// </summary>
    /// <returns>user profile</returns>
    [HttpGet]
    public async Task<ActionResult<Profile>> GetProfile()
    {
      var email = GetEmail();

      return await _context.Profile.SingleAsync(s => s.Email == email);
    }

    /// <summary>
    /// PUT: api/profile
    /// </summary>
    /// <param name="profile">updated user profile</param>
    /// <returns>not found if profile not exists, no content if success</returns>
    [HttpPut]
    public async Task<IActionResult> PutProfile(Profile profile)
    {
      var email = GetEmail();
      profile.Email = email;

      _context.Entry(profile).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProfileExists(profile.ProfileId))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }
    /// <summary>
    /// check user profile is exists
    /// </summary>
    /// <param name="id">user profile id</param>
    /// <returns>true or false</returns>
    private bool ProfileExists(int id)
    {
      return _context.Profile.Any(e => e.ProfileId == id);
    }
  }
}
