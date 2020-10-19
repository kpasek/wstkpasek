using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkp.Models.Database;
using wstkp.Models.Schedule.Series;

namespace wstkp.Controllers
{
  [Route("api/schedule/series")]
  [ApiController]
  [Authorize]
  public class ScheduleSeriesController : ControllerBase
  {
    private readonly AppDBContext _context;

    public ScheduleSeriesController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/ScheduleSeries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduleSeries>>> GetScheduleSeries()
    {
      return await _context.ScheduleSeries.ToListAsync();
    }

    // GET: api/ScheduleSeries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleSeries>> GetScheduleSeries(int id)
    {
      var scheduleSeries = await _context.ScheduleSeries.FindAsync(id);

      if (scheduleSeries == null)
      {
        return NotFound();
      }

      return scheduleSeries;
    }

    // PUT: api/ScheduleSeries/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutScheduleSeries(int id, ScheduleSeries scheduleSeries)
    {
      if (id != scheduleSeries.ScheduleSeriesId)
      {
        return BadRequest();
      }

      _context.Entry(scheduleSeries).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ScheduleSeriesExists(id))
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

    // POST: api/ScheduleSeries
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<ScheduleSeries>> PostScheduleSeries(ScheduleSeries scheduleSeries)
    {
      _context.ScheduleSeries.Add(scheduleSeries);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetScheduleSeries", new { id = scheduleSeries.ScheduleSeriesId }, scheduleSeries);
    }

    // DELETE: api/ScheduleSeries/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleSeries>> DeleteScheduleSeries(int id)
    {
      var scheduleSeries = await _context.ScheduleSeries.FindAsync(id);
      if (scheduleSeries == null)
      {
        return NotFound();
      }

      _context.ScheduleSeries.Remove(scheduleSeries);
      await _context.SaveChangesAsync();

      return scheduleSeries;
    }

    private bool ScheduleSeriesExists(int id)
    {
      return _context.ScheduleSeries.Any(e => e.ScheduleSeriesId == id);
    }
  }
}
