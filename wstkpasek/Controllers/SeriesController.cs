using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.In;
using wstkpasek.Models.SeriesModel;

namespace wstkpasek.Controllers
{
    [Route("api/series")]
    [ApiController]
    [Authorize]
    public class SeriesController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ISeriesRepository seriesRepository;

        public SeriesController(AppDBContext context, ISeriesRepository seriesRepository)
        {
            _context = context;
            this.seriesRepository = seriesRepository;
        }

        private string GetEmail()
        {
            return this.User.Identity.Name;
        }

        // GET: api/Series
        [HttpGet]
        public ActionResult<IEnumerable<Series>> GetSeries()
        {
            return new List<Series>();
        }
        // GET: api/series/exercise/5
        /// <summary>
        /// endpoint find and return series in exercise
        /// </summary>
        /// <param name="id">exerciseId</param>
        /// <returns>List of series</returns>
        [HttpGet("exercise/{id}")]
        public async Task<ActionResult<List<Series>>> GetSeriesForExercise(int id)
        {
            var email = GetEmail();
            var series = await seriesRepository.GetSeriesForExerciseAsync(id, email);
            return series;

        }
        // GET: api/Series/5
        /// <summary>
        /// Return one series if user is series owner
        /// </summary>
        /// <param name="id">series id</param>
        /// <returns>one series</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeries(int id)
        {
            var series = await _context.Series.FindAsync(id);

            if (series == null)
            {
                return NotFound();
            }

            return series;
        }

        // PUT: api/Series/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update all fields in series. 
        /// series id and id from url must be the same
        /// </summary>
        /// <param name="id">series id form url</param>
        /// <param name="series">series object to update</param>
        /// <returns>update status</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Series>> PutSeries(int id, Series series)
        {
            var email = GetEmail();
            series.UserEmail = email;
            if (id != series.SeriesId || !seriesRepository.CheckSeriesOwner(id, email))
            {
                return BadRequest();
            }
            CreateSeriesName(series);
            _context.Entry(series).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return series;
        }

        // POST: api/Series
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Series>> PostSeries(Series series)
        {
            var email = GetEmail();
            series.UserEmail = email;
            var max = await seriesRepository.GetMaxOrderInExerciseAsync(series.ExerciseId, email);
            series.Order = max > 0 ? max + 1 : 1;
            _context.Series.Add(series);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeries", new { id = series.SeriesId }, series);
        }
        [HttpPost("order")]
        public async Task<ActionResult> ChangeSeriesOrder(ChangeSeriesOrderIn model)
        {
            var email = GetEmail();
            var exerciseId = seriesRepository.GetSeries(model.SeriesId, email).ExerciseId;
            var series = await seriesRepository.GetSeriesForExerciseAsync(exerciseId, email);
            var changedItem =
                            series.Single(s => s.SeriesId == model.SeriesId);
            if (changedItem.Order == model.Order) return Ok();
            if (model.Order > series.Max(m => m.Order)) model.Order = series.Max(m => m.Order);
            var seriesInNewOrder = new List<Series>();

            if (changedItem.Order < model.Order)
            {
                seriesInNewOrder = series.Where(w => w.Order <= model.Order && w.SeriesId != changedItem.SeriesId).ToList();
                foreach (var exercise in seriesInNewOrder)
                {
                    exercise.Order--;
                }

                changedItem.Order = model.Order;
                seriesInNewOrder.Add(changedItem);
            }

            if (changedItem.Order > model.Order)
            {
                {
                    seriesInNewOrder = series.Where(w =>
                        w.Order >= model.Order && w.SeriesId != changedItem.SeriesId).ToList();
                    foreach (var exercise in seriesInNewOrder)
                    {
                        exercise.Order++;
                    }

                    changedItem.Order = model.Order;
                    seriesInNewOrder.Add(changedItem);
                }

            }

            _context.UpdateRange(seriesInNewOrder);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Series/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Series>> DeleteSeries(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }

            _context.Series.Remove(series);
            await _context.SaveChangesAsync();

            return series;
        }

        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.SeriesId == id);
        }
        private void CreateSeriesName(Series series)
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
