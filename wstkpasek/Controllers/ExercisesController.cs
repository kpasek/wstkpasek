using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.In;
using wstkpasek.Models.InModels;
using wstkpasek.Models.Out;
using wstkpasek.Models.TrainingModel;

namespace wstkpasek.Controllers
{
  [Route("api/exercises")]
  [ApiController]
  [Authorize]
  public class ExercisesController : ControllerBase
  {
    private readonly AppDBContext _context;
    private readonly IExerciseRepository exerciseRepository;

    public ExercisesController(AppDBContext context, IExerciseRepository exerciseRepository)
    {
      _context = context;
      this.exerciseRepository = exerciseRepository;
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
    /// GET: api/exercises
    /// download all exercise for user
    /// </summary>
    /// <returns>list of exercises</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Exercise>> GetExercises()
    {
      var email = GetEmail();
      var exercise = exerciseRepository.GetExercises(email);

      return exercise.Any() ? exercise : new List<Exercise>();
    }
    /// <summary>
    /// GET: api/parts
    /// download all public and user parts
    /// </summary>
    /// <returns>list of parts</returns>

    [HttpGet("parts")]
    public ActionResult<List<Part>> GetParts()
    {
      var email = GetEmail();
      var parts = exerciseRepository.GetParts(email);
      return parts;
    }
    /// <summary>
    /// GET: api/types
    /// download all public and user types of exercises
    /// </summary>
    /// <returns>list of exercise types</returns>

    [HttpGet("types")]
    public ActionResult<List<Type>> GetTypes()
    {
      var email = GetEmail();
      var types = exerciseRepository.GetTypes(email);
      return types;
    }

    /// <summary>
    /// GET: api/exercises/training/{id}
    /// get all exercises for training
    /// user need to be training owner
    /// </summary>
    /// <param name="id">Training ID</param>
    /// <returns>list of exercises in training</returns>
    [HttpGet("training/{id}")]
    public async Task<ActionResult<IEnumerable<ExerciseWithOrder>>> GetExercisesForTrainingAsync(int id)
    {
      var email = GetEmail();
      var exercises = await exerciseRepository.GetExercisesForTraining(id, email);
      return exercises;
    }

    // GET: api/Exercises/5
    /// <summary>
    /// GET: api/exercises/{id}
    /// get one exercise
    /// user need to be exercise owner
    /// </summary>
    /// <param name="id">exercise id</param>
    /// <returns>one exercise</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Exercise>> GetExercise(int id)
    {
      var exercise = await _context.Exercises.FindAsync(id);

      if (exercise == null)
      {
        return NotFound();
      }

      return exercise;
    }

    /// <summary>
    /// GET: part/{partId}
    /// get exercises by part
    /// user need to be exercises owner
    /// </summary>
    /// <param name="partId">part name</param>
    /// <returns>list of exercises</returns>
    [HttpGet("part/{partId}")]
    public ActionResult<List<Exercise>> GetExercisesByPart(string partId)
    {
      var email = GetEmail();
      return exerciseRepository.GetExerciseByPart(partId, email);

    }
    /// <summary>
    /// POST: /api/order
    /// get order fot exercise in training
    /// </summary>
    /// <param name="model">TrainingID, ExerciseId</param>
    /// <returns>integer</returns>
    [HttpPost("order")]
    public async Task<int> GetExerciseOrder(CheckOrderIn model)
    {
      var email = GetEmail();
      var order = await exerciseRepository.GetExerciseOrderAsync(model.TrainingId, model.ExerciseId, email);
      return order;
    }

    // PUT: api/Exercises/5
    /// <summary>
    /// PUT: api/exercises/{id}
    /// update exercise if user is exercise owner
    /// exercise id in url must be the same in exercise model
    /// </summary>
    /// <param name="id">exercise id</param>
    /// <param name="exercise">exercise model to update</param>
    /// <returns>bad request if id != model.exerciseId, not found if exercise not exists, no content if update success</returns>

    [HttpPut("{id}")]
    public async Task<IActionResult> PutExercise(int id, Exercise exercise)
    {
      var email = GetEmail();
      exercise.UserEmail = email;

      if (id != exercise.ExerciseId)
      {
        return BadRequest();
      }


      _context.Entry(exercise).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ExerciseExists(id))
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
    /// POST: api/chane/{id}
    /// swap exercise in training
    /// </summary>
    /// <param name="id">exercise id to swap</param>
    /// <param name="model">model.trainingID, new exerciseId, order in training</param>
    /// <returns>OK if swap success</returns>

    [HttpPost("change/{id}")]
    public async Task<ActionResult> SwapExercise(int id, SwapExerciseIn model)
    {
      var trEx = _context.TrainingExercises.Where(w => w.ExerciseId == id && w.TrainingId == model.TrainingId && w.Order == model.Order)
                                           .OrderByDescending(o => o.TrainingExerciseId)
                                           .Take(1)
                                           .Single();
      trEx.ExerciseId = model.ExerciseId;
      _context.Update(trEx);
      await _context.SaveChangesAsync();
      return Ok();

    }
    /// <summary>
    /// change exercise order in training
    /// </summary>
    /// <param name="model">trainingId, exerciseId, new order</param>
    /// <returns>OK if success</returns>
    [HttpPost("change-order")]
    public async Task<ActionResult> ChangeOrder(ChangeOrderIn model)
    {
      var email = GetEmail();
      var trainingExercises = await exerciseRepository.GetExercisesOrderForTraining(model.TrainingId, email);
      var changedItem =
          trainingExercises.Single(s => s.TrainingId == model.TrainingId && s.ExerciseId == model.ExerciseId);
      if (changedItem.Order == model.Order) return Ok();
      if (model.Order > trainingExercises.Count) model.Order = trainingExercises.Count;
      var exercisesInNewOrder = new List<TrainingExercise>();

      if (changedItem.Order < model.Order)
      {
        exercisesInNewOrder = trainingExercises.Where(w => w.Order <= model.Order && w.Order > changedItem.Order && w.TrainingExerciseId != changedItem.TrainingExerciseId).ToList();
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
          exercisesInNewOrder = trainingExercises.Where(w =>
              w.Order >= model.Order && w.Order < changedItem.Order && w.TrainingExerciseId != changedItem.TrainingExerciseId).ToList();
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
    /// POST: api/exercises
    /// create new exercise
    /// </summary>
    /// <param name="exercise">new exercise data</param>
    /// <returns>created exercise</returns>
    [HttpPost]
    public async Task<ActionResult<Exercise>> PostExercise(Exercise exercise)
    {
      var email = GetEmail();
      exercise.UserEmail = email;
      var part = exerciseRepository.GetPart(exercise.PartId, email);
      var types = exerciseRepository.GetType(exercise.TypeId, email);
      if (part == null)
      {
        var newPart = new Part
        {
          Name = exercise.PartId,
          Public = false,
          UserEmail = email
        };
        await _context.AddAsync(newPart);
      }
      if (types == null)
      {
        var newType = new Type
        {
          Public = false,
          TypeName = exercise.TypeId,
          UserEmail = email
        };
        await _context.AddAsync(newType);
      }

      await _context.Exercises.AddAsync(exercise);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetExercise", new { id = exercise.ExerciseId }, exercise);
    }

    /// <summary>
    /// DELETE: api/exercises/{id}
    /// delete exercise
    /// user need to be exercise owner
    /// </summary>
    /// <param name="id">exercise id to delete</param>
    /// <returns>not found id exercise not exests, deleted exercise if success</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Exercise>> DeleteExercise(int id)
    {
      var exercise = await _context.Exercises.FindAsync(id);
      if (exercise == null)
      {
        return NotFound();
      }

      _context.Exercises.Remove(exercise);
      await _context.SaveChangesAsync();

      return exercise;
    }

    /// <summary>
    /// check exercise is exists in database
    /// </summary>
    /// <param name="id">esercise id to check</param>
    /// <returns>true or false</returns>
    private bool ExerciseExists(int id)
    {
      return _context.Exercises.Any(e => e.ExerciseId == id);
    }
  }
}