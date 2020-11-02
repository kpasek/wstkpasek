using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private string GetEmail()
        {
            return this.User.Identity.Name;
        }
        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<Profile>> GetProfile()
        {
            var email = GetEmail();

            return await _context.Profile.SingleAsync(s => s.Email == email);
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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

        private bool ProfileExists(int id)
        {
            return _context.Profile.Any(e => e.ProfileId == id);
        }
    }
}
