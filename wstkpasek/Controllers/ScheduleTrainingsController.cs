﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkp.Models.Database;
using wstkp.Models.Schedule.Training;

namespace wstkp.Controllers
{
  [Route("api/schedule/trainings")]
  [ApiController]
  [Authorize]
  public class ScheduleTrainingsController : ControllerBase
  {
    private readonly AppDBContext _context;

    public ScheduleTrainingsController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/ScheduleTrainings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduleTraining>>> GetScheduleTrainings()
    {
      return await _context.ScheduleTrainings.ToListAsync();
    }

    // GET: api/ScheduleTrainings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleTraining>> GetScheduleTraining(int id)
    {
      var scheduleTraining = await _context.ScheduleTrainings.FindAsync(id);

      if (scheduleTraining == null)
      {
        return NotFound();
      }

      return scheduleTraining;
    }

    // PUT: api/ScheduleTrainings/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutScheduleTraining(int id, ScheduleTraining scheduleTraining)
    {
      if (id != scheduleTraining.ScheduleTrainingId)
      {
        return BadRequest();
      }

      _context.Entry(scheduleTraining).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ScheduleTrainingExists(id))
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

    // POST: api/ScheduleTrainings
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<ScheduleTraining>> PostScheduleTraining(ScheduleTraining scheduleTraining)
    {
      _context.ScheduleTrainings.Add(scheduleTraining);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetScheduleTraining", new { id = scheduleTraining.ScheduleTrainingId }, scheduleTraining);
    }

    // DELETE: api/ScheduleTrainings/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleTraining>> DeleteScheduleTraining(int id)
    {
      var scheduleTraining = await _context.ScheduleTrainings.FindAsync(id);
      if (scheduleTraining == null)
      {
        return NotFound();
      }

      _context.ScheduleTrainings.Remove(scheduleTraining);
      await _context.SaveChangesAsync();

      return scheduleTraining;
    }

    private bool ScheduleTrainingExists(int id)
    {
      return _context.ScheduleTrainings.Any(e => e.ScheduleTrainingId == id);
    }
  }
}
