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
    [Route("api/settings")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SettingsController(AppDBContext context)
        {
            _context = context;
        }
        private string GetEmail()
        {
            return this.User.Identity.Name;
        }
        // GET: api/Settings
        [HttpGet]
        public async Task<ActionResult<Settings>> GetSettings()
        {
            var email = GetEmail();
            return await _context.Settings.SingleAsync(s => s.UserEmail == email);
        }


        // PUT: api/Settings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutSettings(Settings settings)
        {
            var email = GetEmail();
            settings.UserEmail = email;
            _context.Entry(settings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingsExists(settings.SettingsId))
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

        private bool SettingsExists(int id)
        {
            return _context.Settings.Any(e => e.SettingsId == id);
        }
    }
}
