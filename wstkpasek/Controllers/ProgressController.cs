using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wstkpasek.Models.Database;
using wstkpasek.Models.Out;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.SeriesModel;

namespace wstkpasek.Controllers
{
    [Route("api/progress")]
    [ApiController]
    [Authorize]
    public class ProgressController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IScheduleSeriesRepository seriesRepository;

        public ProgressController(AppDBContext context, IScheduleSeriesRepository seriesRepository)
        {
            _context = context;
            this.seriesRepository = seriesRepository;
        }
        private string GetEmail()
        {
            return this.User.Identity.Name;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<List<ProgressOut>>> GetSeries()
        {
            var email = GetEmail();
            var series = await seriesRepository.GetSeriesByDatesAsync(email, DateTime.Now.AddMonths(-1), null);
            List<ProgressOut> p = new List<ProgressOut>();
            foreach (var s in series.OrderBy(o => o.Name).Select(s => s.ScheduleExercise.Exercise.Name).Distinct().ToList())
            {
                foreach (var d in series.Where(w => w.ScheduleExercise.Exercise.Name == s).Select(ser => new ProgressOut
                {
                    Part = s,
                    Dates = series.Where(w => w.ScheduleExercise.Exercise.Name == s).Select(d => d.ScheduleExercise.ScheduleTraining.TrainingDate.ToString("yyyy.MM.dd")).ToList(),
                    Repeats = series.Where(w => w.ScheduleExercise.Exercise.Name == s).Select(r => (double)r.Repeats).ToList(),
                    Loads = series.Where(w => w.ScheduleExercise.Exercise.Name == s).Select(l => l.Load).ToList(),
                    Distanses = series.Where(w => w.ScheduleExercise.Exercise.Name == s).Select(dd => dd.Distance).ToList(),
                    Times = series.Where(w => w.ScheduleExercise.Exercise.Name == s).Select(t => (double)t.Time).ToList(),
                    RestTimes = series.Where(w => w.ScheduleExercise.Exercise.Name == s).Select(rt => (double)rt.RestTime).ToList()
                }))
                {
                    p.Add(d);
                }
            }
            return p;
        }

        // GET: api/progress/2020-09-01/2020-10-01
        /// <summary>
        /// Find and return prepared series between dates
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns>List of series</returns>
        [HttpGet("{dateFrom}/{dateTo}")]
        public async Task<ActionResult<List<ScheduleSeries>>> GetSeries(string dateFrom, string dateTo)
        {

            var email = GetEmail();
            var series = await seriesRepository.GetSeriesByDatesAsync(email, null, null);

            return series;
        }

    }
}
