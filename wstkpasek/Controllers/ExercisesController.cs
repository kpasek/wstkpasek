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

        private string GetEmail()
        {
            return this.User.Identity.Name;
        }

        // GET: api/Exercises
        [HttpGet]
        public ActionResult<IEnumerable<Exercise>> GetExercises()
        {
            var email = GetEmail();
            var exercise = exerciseRepository.GetExercises(email);
            return exercise.Any() ? exercise : new List<Exercise>();
        }

        [HttpGet("parts")]
        public ActionResult<List<Part>> GetParts()
        {
            var email = GetEmail();
            var parts = exerciseRepository.GetParts(email);
            return parts;
        }

        [HttpGet("types")]
        public ActionResult<List<Type>> GetTypes()
        {
            var email = GetEmail();
            var types = exerciseRepository.GetTypes(email);
            return types;
        }

        // GET: api/exercises/training/{id}
        [HttpGet("training/{id}")]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercisesForTrainingAsync(int id)
        {
            var email = GetEmail();
            var exercises = await exerciseRepository.GetExercisesForTraining(id, email);
            return exercises;
        }

        // GET: api/Exercises/5
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

        [HttpGet("part/{partId}")]
        public ActionResult<List<Exercise>> GetExercisesByPart(string partId)
        {
            var email = GetEmail();
            return exerciseRepository.GetExerciseByPart(partId, email);

        }

        [HttpPost("order")]
        public async Task<int> GetExerciseOrder(CheckOrderIn model)
        {
            var email = GetEmail();
            var order = await exerciseRepository.GetExerciseOrderAsync(model.TrainingId, model.ExerciseId, email);
            return order;
        }

        // PUT: api/Exercises/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

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
        /// <returns>none</returns>
        [HttpPost("change-order")]
        public async Task<ActionResult> ChangeOrder(ChangeOrderIn model)
        {
            var email = GetEmail();
            var trainingExercises = await exerciseRepository.GetExercisesOrderForTraining(model.TrainingId, email);
            var changedItem =
                trainingExercises.Single(s => s.TrainingId == model.TrainingId && s.ExerciseId == model.ExerciseId);
            if (changedItem.Order == model.Order) return Ok();
            if (model.Order > trainingExercises.Max(m => m.Order)) model.Order = trainingExercises.Max(m => m.Order);
            var exercisesInNewOrder = new List<TrainingExercise>();

            if (changedItem.Order < model.Order)
            {
                exercisesInNewOrder = trainingExercises.Where(w => w.Order <= model.Order && w.TrainingExerciseId != changedItem.TrainingExerciseId).ToList();
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
                        w.Order >= model.Order && w.TrainingExerciseId != changedItem.TrainingExerciseId).ToList();
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

        // POST: api/Exercises
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(Exercise exercise)
        {
            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercise", new { id = exercise.ExerciseId }, exercise);
        }

        // DELETE: api/Exercises/5
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

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}