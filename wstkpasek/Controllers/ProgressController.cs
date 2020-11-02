using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wstkpasek.Models.Database;
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
        public async Task<ActionResult<List<ScheduleSeries>>> GetSeries()
        {
            var email = GetEmail();
            var series = await seriesRepository.GetSeriesByDatesAsync(email, null, null);

            return series;
        }

        // GET: api/Types/5
        [HttpGet("{dateFrom}/{dateTo}")]
        public async Task<ActionResult<List<ScheduleSeries>>> GetSeries(string dateFrom, string dateTo)
        {

            var email = GetEmail();
            var series = await seriesRepository.GetSeriesByDatesAsync(email, null, null);

            return series;
        }

    }
}
