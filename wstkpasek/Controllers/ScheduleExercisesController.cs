﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.In;
using wstkpasek.Models.Schedule.Exercise;

namespace wstkpasek.Controllers
{
    [Route("api/schedule/exercises")]
    [ApiController]
    [Authorize]
    public class ScheduleExercisesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IScheduleExerciseRepository scheduleExerciseRepository;

        public ScheduleExercisesController(AppDBContext context, IScheduleExerciseRepository scheduleExerciseRepository)
        {
            _context = context;
            this.scheduleExerciseRepository = scheduleExerciseRepository;
        }
        private string GetEmail()
        {
            return this.User.Identity.Name;
        }

        // GET: api/ScheduleExercises
        [HttpGet("t/{trainingId}")]
        public async Task<ActionResult<List<ScheduleExercise>>> GetScheduleExercisesForTraining(int trainingId)
        {
            var email = GetEmail();
            var exercises = await scheduleExerciseRepository.GetScheduleExerciseForScheduleTrainingAsync(trainingId, email);
            return exercises;
        }

        // GET: api/ScheduleExercises/5
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

        // PUT: api/ScheduleExercises/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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

        // POST: api/ScheduleExercises
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ScheduleExercise>> PostScheduleExercise(ScheduleExercise ScheduleExercise)
        {
            _context.ScheduleExercises.Add(ScheduleExercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScheduleExercise", new { id = ScheduleExercise.ScheduleExerciseId }, ScheduleExercise);
        }
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

        // DELETE: api/ScheduleExercises/5
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

        private bool ScheduleExerciseExists(int id)
        {
            return _context.ScheduleExercises.Any(e => e.ScheduleExerciseId == id);
        }
    }
}
