using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.In;
using wstkpasek.Models.Schedule.Exercise;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.SeriesModel;

namespace wstkpasek.Controllers
{
  [Route("api/schedule/exercises")]
  [ApiController]
  [Authorize]
  public class ScheduleExercisesController : ControllerBase
  {
    private readonly AppDBContext _context;
    private readonly IScheduleExerciseRepository scheduleExerciseRepository;
    private readonly ISeriesRepository seriesRepository;

    public ScheduleExercisesController(AppDBContext context, IScheduleExerciseRepository scheduleExerciseRepository, ISeriesRepository seriesRepository)
    {
      _context = context;
      this.scheduleExerciseRepository = scheduleExerciseRepository;
      this.seriesRepository = seriesRepository;
    }
    /// <summary>
    /// Method return user email from provided token
    /// </summary>
    /// <returns>user email</returns>
    private string GetEmail()
    {
      return this.User.Identity.Name;
    }

    /// <summary>
    /// GET: api/schedule/exercise/t/{trainingId}
    /// get schedule-exercise for schedule-training 
    /// </summary>
    /// <param name="trainingId">training id required</param>
    /// <returns>list of schedule-exercises</returns>
    [HttpGet("t/{trainingId}")]
    public async Task<ActionResult<List<ScheduleExercise>>> GetScheduleExercisesForTraining(int trainingId)
    {
      var email = GetEmail();
      var exercises = await scheduleExerciseRepository.GetScheduleExerciseForScheduleTrainingAsync(trainingId, email);
      return exercises;
    }

    /// <summary>
    /// GET: api/schedule/exercise/{id}
    /// get schedule-exercise by id
    /// </summary>
    /// <param name="id">schedule-exercise id required</param>
    /// <returns>schedule-exercise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleExercise>> GetScheduleExercise(int id)
    {
      var ScheduleExercise = await _context.ScheduleExercises.FindAsync(id);

      if (ScheduleExercise == null)
      {
        return NotFound();
      }

      return ScheduleExercise;
    }

    /// <summary>
    /// PUT: api/schedule/exercise/{id}
    /// update schedule-exercise
    /// id from url must be the same as in model
    /// </summary>
    /// <param name="id">schedule-exercise id required</param>
    /// <param name="ScheduleExercise">ScheduleExercise model</param>
    /// <returns>OK - success, NotFound, BadRequest if id != model.id</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutScheduleExercise(int id, ScheduleExercise ScheduleExercise)
    {
      if (id != ScheduleExercise.ScheduleExerciseId)
      {
        return BadRequest();
      }

      _context.Entry(ScheduleExercise).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ScheduleExerciseExists(id))
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
    /// POST: api/schedule/exercise
    /// create new schedule-exercise
    /// </summary>
    /// <param name="model">ScheduleExercise model required</param>
    /// <returns>created schedule-exercise</returns>
    [HttpPost]
    public async Task<ActionResult<ScheduleExercise>> PostScheduleExercise(AddSExerciseIn model)
    {
      var email = GetEmail();
      var scheduleExercise = new ScheduleExercise();
      scheduleExercise.UserEmail = email;
      scheduleExercise.ExerciseId = model.exerciseId;
      scheduleExercise.ScheduleTrainingId = model.scheduleTrainingId;
      scheduleExercise.Order = await scheduleExerciseRepository.GetMaxOrderForScheduleExerciseAsync(model.scheduleTrainingId, email) + 1;

      _context.ScheduleExercises.Add(scheduleExercise);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetScheduleExercise", new { id = scheduleExercise.ScheduleExerciseId }, scheduleExercise);
    }

    /// <summary>
    /// POST: api/schedule/exercise
    /// swap exercises in schedule-training
    /// </summary>
    /// <param name="model">exerciseId - new exercise, scheduleExerciseId to swap</param>
    /// <returns>OK - success</returns>
    [HttpPost("swap")]
    public async Task<ActionResult> SwapExercise(SwapSExerciseIn model)
    {
      var email = GetEmail();
      var exerciseDelete = await scheduleExerciseRepository.GetScheduleExerciseAsync(model.ScheduleExerciseId, email);
      var newExercise = new ScheduleExercise
      {
        ScheduleTrainingId = exerciseDelete.ScheduleTrainingId,
        Started = false,
        UserEmail = email,
        Order = exerciseDelete.Order,
        ExerciseId = model.ExerciseId
      };

      await _context.AddAsync(newExercise);
      await _context.SaveChangesAsync();
      var series = new List<ScheduleSeries>();
      var seriesInExercise = await seriesRepository.GetSeriesForExerciseAsync(newExercise.ExerciseId, email);
      foreach (var s in seriesInExercise)
      {
        series.Add(new ScheduleSeries
        {
          ScheduleExerciseId = newExercise.ScheduleExerciseId,
          Distance = s.Distance,
          Finish = false,
          Load = s.Load,
          Name = s.Name,
          Order = s.Order,
          Repeats = s.Repeats,
          RestTime = s.RestTime,
          Time = s.Time,
          UserEmail = email,
        });
      }
      await _context.AddRangeAsync(series);
      _context.Remove(exerciseDelete);
      await _context.SaveChangesAsync();
      return Ok();
    }

    /// <summary>
    /// POST: api/schedule/exercise/change-order
    /// change exercise order in schedule-training
    /// </summary>
    /// <param name="model">SExerciseId to change, new order</param>
    /// <returns>OK - success</returns>

    [HttpPost("change-order")]
    public async Task<ActionResult> ChangeOrder(ChangeOrderSExerciseIn model)
    {
      var email = GetEmail();
      var changedItem = await scheduleExerciseRepository.GetScheduleExerciseAsync(model.scheduleExerciseId, email);
      var sExercises = await scheduleExerciseRepository.GetScheduleExerciseForScheduleTrainingAsync(changedItem.ScheduleTrainingId, email);
      if (changedItem.Order == model.Order) return Ok();
      if (model.Order > sExercises.Count) model.Order = sExercises.Count;
      var exercisesInNewOrder = new List<ScheduleExercise>();

      if (changedItem.Order < model.Order)
      {
        exercisesInNewOrder = sExercises.Where(w => w.Order <= model.Order && w.Order > changedItem.Order && w.ScheduleExerciseId != changedItem.ScheduleExerciseId).ToList();
        foreach (var exercise in exercisesInNewOrder)
        {
          exercise.Order--;
        }

        changedItem.Order = model.Order;
        exercisesInNewOrder.Add(changedItem);
      }

      if (changedItem.Order > model.Order)
      {
        {
          exercisesInNewOrder = sExercises.Where(w =>
              w.Order >= model.Order && w.Order < changedItem.Order && w.ScheduleExerciseId != changedItem.ScheduleExerciseId).ToList();
          foreach (var exercise in exercisesInNewOrder)
          {
            exercise.Order++;
          }

          changedItem.Order = model.Order;
          exercisesInNewOrder.Add(changedItem);
        }

      }

      _context.UpdateRange(exercisesInNewOrder);
      await _context.SaveChangesAsync();
      return Ok();
    }

    /// <summary>
    /// DELETE: api/schedule/exercise/{id}
    /// delete schedule-exercise
    /// user must be exercise owner
    /// </summary>
    /// <param name="id">SExercise id</param>
    /// <returns>deleted exercise</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleExercise>> DeleteScheduleExercise(int id)
    {
      var ScheduleExercise = await _context.ScheduleExercises.FindAsync(id);
      if (ScheduleExercise == null)
      {
        return NotFound();
      }

      _context.ScheduleExercises.Remove(ScheduleExercise);
      await _context.SaveChangesAsync();

      return ScheduleExercise;
    }
    /// <summary>
    /// Check schedule-exercise exists in database
    /// </summary>
    /// <param name="id">schedule-exercise ID</param>
    /// <returns>true or false</returns>
    private bool ScheduleExerciseExists(int id)
    {
      return _context.ScheduleExercises.Any(e => e.ScheduleExerciseId == id);
    }
  }
}
