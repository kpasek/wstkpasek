using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.In;
using wstkpasek.Models.TrainingModel;

namespace wstkpasek.Controllers
{
    [Route("api/trainings")]
    [ApiController]
    [Authorize]
    public class TrainingsController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ITrainingRepository trainingRepository;

        public TrainingsController(AppDBContext context, ITrainingRepository trainingRepository)
        {
            _context = context;
            this.trainingRepository = trainingRepository;
        }
        /// <summary>
        /// get email address from token
        /// </summary>
        /// <returns>user email from token</returns>
        private string GetEmail()
        {
            return this.User.Identity.Name;
        }

        // GET: api/Trainings
        /// <summary>
        /// use trainings repository to get all user trainings
        /// </summary>
        /// <returns>trainigs list in json format</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Training>>> GetTrainings()
        {
            var email = GetEmail();
            var userTrainings = await trainingRepository.GetTrainings(email);
            return userTrainings;
        }

        // GET: api/Trainings/5
        /// <summary>
        /// Use repository to get one user training.
        /// </summary>
        /// <param name="id">IdTraining</param>
        /// <returns>One uesr training</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Training>> GetTraining(int id)
        {
            var email = GetEmail();
            var training = await trainingRepository.GetTraining(id, email);

            if (training == null) return NotFound();

            return training;
        }

        // PUT: api/Trainings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// update all fields in training. User need to be owner and training id must be the same as in route
        /// </summary>
        /// <param name="id"></param>
        /// <param name="training"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraining(int id, Training training)
        {
            var email = GetEmail();
            if (id != training.TrainingId || !trainingRepository.CheckTrainingOwner(id, email)) return BadRequest();
            training.UserEmail = email;
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

        [HttpPost("exercise/add")]
        public async Task<ActionResult> AddExerciseInTraining(AddExerciseInTraining model)
        {
            var email = GetEmail();
            TrainingExercise te = new TrainingExercise
            {
                ExerciseId = model.ExerciseId,
                TrainingId = model.TrainingId,
                Order = await trainingRepository.GetMaxOrderInTrainingAsync(model.TrainingId, email) + 1,
                UserEmail = email
            };
            await _context.TrainingExercises.AddAsync(te);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("exercise/delete")]
        public async Task<ActionResult> DeleteExerciseInTraining(DeleteExerciseInTraining model)
        {
            var email = GetEmail();
            var te = await _context.TrainingExercises.Where(w => w.ExerciseId == model.ExerciseId && w.TrainingId == model.TrainingId && w.UserEmail == email && w.Order == model.Order)
                .Take(1)
                .SingleAsync();
            _context.Remove(te);
            await _context.SaveChangesAsync();
            return NoContent();
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
