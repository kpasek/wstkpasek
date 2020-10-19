using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkp.Models.Database;
using wstkp.Models.Exercises;

namespace wstkp.Controllers
{
  [Route("api/schedule/exercises")]
  [ApiController]
  [Authorize]
  public class ScheduleExercisesController : ControllerBase
  {
    private readonly AppDBContext _context;

    public ScheduleExercisesController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/ScheduleExercies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduleExercie>>> GetScheduleExercie()
    {
      return await _context.ScheduleExercie.ToListAsync();
    }

    // GET: api/ScheduleExercies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleExercie>> GetScheduleExercie(int id)
    {
      var scheduleExercie = await _context.ScheduleExercie.FindAsync(id);

      if (scheduleExercie == null)
      {
        return NotFound();
      }

      return scheduleExercie;
    }

    // PUT: api/ScheduleExercies/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutScheduleExercie(int id, ScheduleExercie scheduleExercie)
    {
      if (id != scheduleExercie.ScheduleExerciseId)
      {
        return BadRequest();
      }

      _context.Entry(scheduleExercie).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ScheduleExercieExists(id))
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

    // POST: api/ScheduleExercies
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<ScheduleExercie>> PostScheduleExercie(ScheduleExercie scheduleExercie)
    {
      _context.ScheduleExercie.Add(scheduleExercie);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetScheduleExercie", new { id = scheduleExercie.ScheduleExerciseId }, scheduleExercie);
    }

    // DELETE: api/ScheduleExercies/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleExercie>> DeleteScheduleExercie(int id)
    {
      var scheduleExercie = await _context.ScheduleExercie.FindAsync(id);
      if (scheduleExercie == null)
      {
        return NotFound();
      }

      _context.ScheduleExercie.Remove(scheduleExercie);
      await _context.SaveChangesAsync();

      return scheduleExercie;
    }

    private bool ScheduleExercieExists(int id)
    {
      return _context.ScheduleExercie.Any(e => e.ScheduleExerciseId == id);
    }
  }
}
