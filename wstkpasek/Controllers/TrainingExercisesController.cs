using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.TrainingModel;

namespace wstkpasek.Controllers
{
  [Route("api/trainings/exercises/")]
  [ApiController]
  [Authorize]
  public class TrainingExercisesController : ControllerBase
  {
    private readonly AppDBContext _context;

    public TrainingExercisesController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/TrainingExercises
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainingExercise>>> GetTrainingExercises()
    {
      return await _context.TrainingExercises.ToListAsync();
    }

    // GET: api/TrainingExercises/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingExercise>> GetTrainingExercise(int id)
    {
      var trainingExercise = await _context.TrainingExercises.FindAsync(id);

      if (trainingExercise == null)
      {
        return NotFound();
      }

      return trainingExercise;
    }

    // PUT: api/TrainingExercises/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrainingExercise(int id, TrainingExercise trainingExercise)
    {
      if (id != trainingExercise.TrainingExerciseId)
      {
        return BadRequest();
      }

      _context.Entry(trainingExercise).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TrainingExerciseExists(id))
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

    // POST: api/TrainingExercises
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<TrainingExercise>> PostTrainingExercise(TrainingExercise trainingExercise)
    {
      _context.TrainingExercises.Add(trainingExercise);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTrainingExercise", new { id = trainingExercise.TrainingExerciseId }, trainingExercise);
    }

    // DELETE: api/TrainingExercises/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<TrainingExercise>> DeleteTrainingExercise(int id)
    {
      var trainingExercise = await _context.TrainingExercises.FindAsync(id);
      if (trainingExercise == null)
      {
        return NotFound();
      }

      _context.TrainingExercises.Remove(trainingExercise);
      await _context.SaveChangesAsync();

      return trainingExercise;
    }

    private bool TrainingExerciseExists(int id)
    {
      return _context.TrainingExercises.Any(e => e.TrainingExerciseId == id);
    }
  }
}
