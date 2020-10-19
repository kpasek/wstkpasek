using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkp.Models.Database;
using wstkp.Models.TrainingModel;

namespace wstkp.Controllers
{
  [Route("api/trainings")]
  [ApiController]
  [Authorize]
  public class TrainingsController : ControllerBase
  {
    private readonly AppDBContext _context;

    public TrainingsController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/Trainings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Training>>> GetTrainings()
    {
      return await _context.Trainings.ToListAsync();
    }

    // GET: api/Trainings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Training>> GetTraining(int id)
    {
      var training = await _context.Trainings.FindAsync(id);

      if (training == null)
      {
        return NotFound();
      }

      return training;
    }

    // PUT: api/Trainings/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTraining(int id, Training training)
    {
      if (id != training.TrainingId)
      {
        return BadRequest();
      }

      _context.Entry(training).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TrainingExists(id))
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

    // POST: api/Trainings
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Training>> PostTraining(Training training)
    {
      _context.Trainings.Add(training);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTraining", new { id = training.TrainingId }, training);
    }

    // DELETE: api/Trainings/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Training>> DeleteTraining(int id)
    {
      var training = await _context.Trainings.FindAsync(id);
      if (training == null)
      {
        return NotFound();
      }

      _context.Trainings.Remove(training);
      await _context.SaveChangesAsync();

      return training;
    }

    private bool TrainingExists(int id)
    {
      return _context.Trainings.Any(e => e.TrainingId == id);
    }
  }
}
