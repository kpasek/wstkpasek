using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.Schedule.Series;

namespace wstkpasek.Controllers
{
    [Route("api/schedule/series")]
    [ApiController]
    [Authorize]
    public class ScheduleSeriesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IScheduleSeriesRepository scheduleSeriesRepository;

        public ScheduleSeriesController(AppDBContext context, IScheduleSeriesRepository scheduleSeriesRepository)
        {
            _context = context;
            this.scheduleSeriesRepository = scheduleSeriesRepository;
        }

        private string GetEmail()
        {
            return this.User.Identity.Name;
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
        // GET: api/ScheduleSeries/5
        [HttpGet("e/{exerciseId}")]
        public async Task<ActionResult<List<ScheduleSeries>>> GetScheduleSeriesForExercise(int exerciseId)
        {
            var email = GetEmail();
            var scheduleSeries = await scheduleSeriesRepository.GetScheduleSeriesForScheduleExerciseAsync(exerciseId, email);

            if (scheduleSeries == null) return NoContent();

            return scheduleSeries;
        }

        // PUT: api/ScheduleSeries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<ScheduleSeries>> PutScheduleSeries(int id, ScheduleSeries scheduleSeries)
        {
            var email = GetEmail();
            scheduleSeries.UserEmail = email;
            if (id != scheduleSeries.ScheduleSeriesId)
            {
                return BadRequest();
            }
            CreateSeriesName(scheduleSeries);
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

            return scheduleSeries;
        }

        // POST: api/ScheduleSeries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ScheduleSeries>> PostScheduleSeries(ScheduleSeries scheduleSeries)
        {
            var email = GetEmail();
            scheduleSeries.UserEmail = email;
            CreateSeriesName(scheduleSeries);
            scheduleSeries.Order = await scheduleSeriesRepository.GetMaxOrderForScheduleExerciseAsync(scheduleSeries.ScheduleExerciseId, email) + 1;
            _context.ScheduleSeries.Add(scheduleSeries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScheduleSeries", new { id = scheduleSeries.ScheduleSeriesId }, scheduleSeries);
        }

        // DELETE: api/ScheduleSeries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ScheduleSeries>> DeleteScheduleSeries(int id)
        {
            var email = GetEmail();

            var scheduleSeries = await _context.ScheduleSeries.FindAsync(id);
            if (scheduleSeries == null)
            {
                return NotFound();
            }
            var seriesNewOrder = await scheduleSeriesRepository.GetScheduleSeriesForScheduleExerciseAsync(scheduleSeries.ScheduleExerciseId, email);
            IEnumerable<ScheduleSeries> seriesToUpdate = seriesNewOrder.Where(w => w.Order > scheduleSeries.Order);
            if (seriesToUpdate.Any())
            {
                foreach (var s in seriesToUpdate)
                {
                    s.Order--;
                }
                _context.UpdateRange(seriesToUpdate);
            }
            _context.ScheduleSeries.Remove(scheduleSeries);
            await _context.SaveChangesAsync();

            return scheduleSeries;
        }

        private bool ScheduleSeriesExists(int id)
        {
            return _context.ScheduleSeries.Any(e => e.ScheduleSeriesId == id);
        }
        private void CreateSeriesName(ScheduleSeries series)
        {
            var seriesName = "";
            if (series.Repeats > 0) seriesName += series.Repeats + "x ";
            if (series.Load > 0) seriesName += series.Load + "kg ";
            if (series.Time > 0) seriesName += series.Time + "s ";
            if (series.Distance > 0) seriesName += series.Distance + "m ";
            if (series.RestTime > 0) seriesName += series.RestTime + "s odp.";
            if (seriesName.Trim() != "") series.Name = seriesName.Trim();
            else series.Name = "pusta";
        }
    }
}
