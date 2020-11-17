using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wstkpasek.Models.Database;
using wstkpasek.Models.Out;
using wstkpasek.Models.Schedule.Series;

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
    /// <summary>
    /// Method return user email from provided token
    /// </summary>
    /// <returns>user email</returns>
    private string GetEmail()
    {
      return this.User.Identity.Name;
    }

    /// <summary>
    /// GET: api/progress
    /// get all finished series from last month
    /// </summary>
    /// <returns>list of series formated as ProgressOut</returns>
    [HttpGet]
    public async Task<ActionResult<List<ProgressOut>>> GetSeries()
    {
      var email = GetEmail();
      var series = await seriesRepository.GetSeriesByDatesAsync(email, DateTime.Now.AddMonths(-1), null);
      List<ProgressOut> p = new List<ProgressOut>();

      foreach (var date in series.Select(d => d.ScheduleExercise.ScheduleTraining.TrainingDate).Distinct())
      {
        foreach (var s in series.Where(w => w.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(s => s.ScheduleExercise).Distinct())
        {
          var pr = new ProgressOut
          {
            Date = date.ToString("MM-dd"),
            Type = series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date && ser.ScheduleExerciseId == s.ScheduleExerciseId).Take(1).Select(select => select.ScheduleExercise.Exercise.Name).Single(),
            LoadAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date && ser.ScheduleExerciseId == s.ScheduleExerciseId).Select(select => select.Load).Average(), 2),
            RepeatAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date && ser.ScheduleExerciseId == s.ScheduleExerciseId).Select(select => select.Repeats).Average(), 2),
            TimeAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date && ser.ScheduleExerciseId == s.ScheduleExerciseId).Select(select => select.Time).Average(), 2),
            RestTimeAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date && ser.ScheduleExerciseId == s.ScheduleExerciseId).Select(select => select.RestTime).Average(), 2),
            DistanseAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date && ser.ScheduleExerciseId == s.ScheduleExerciseId).Select(select => select.Distance).Average(), 2),

          };
          p.Add(pr);
        }
      }
      return p;

    }
    /// <summary>
    /// GET: api/progress/part/{partId}
    /// get list of series by part between dates
    /// series formated as ProgressOut
    /// </summary>
    /// <param name="part">part id required</param>
    /// <param name="dateFrom">start date default -3 month from now</param>
    /// <param name="dateTo">end date default now</param>
    /// <returns>list of series formated as ProgressOut</returns>
    [HttpGet("part/{part}")]
    public async Task<ActionResult<List<ProgressOut>>> GetProgressByPart(string part, DateTime dateFrom, DateTime dateTo)
    {
      var email = GetEmail();
      if (dateFrom.Year == 1) dateFrom = DateTime.Now.AddMonths(-3);
      if (dateTo.Year == 1) dateTo = DateTime.Now;
      var series = await seriesRepository.GetSeriesByDatesAsync(email, part, dateFrom, dateTo);
      List<ProgressOut> p = new List<ProgressOut>();

      foreach (var date in series.Select(d => d.ScheduleExercise.ScheduleTraining.TrainingDate).Distinct())
      {
        var pr = new ProgressOut
        {
          Date = date.ToString("MM-dd"),
          Type = series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.ScheduleExercise.Exercise.PartId).Take(1).Single(),
          LoadAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Load).Average(), 2),
          RepeatAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Repeats).Average(), 2),
          TimeAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Time).Average(), 2),
          RestTimeAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.RestTime).Average(), 2),
          DistanseAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Distance).Average(), 2),
        };
        p.Add(pr);
      }
      return p;

    }

    /// <summary>
    /// GET: api/progress/exercise/{exerciseId}
    /// get list of finished series for exercise between dates
    /// formated by ProgressOut
    /// </summary>
    /// <param name="exerciseId">exercise Id required</param>
    /// <param name="dateFrom">start date default -3 month from now</param>
    /// <param name="dateTo">end date default now</param>
    /// <returns></returns>
    [HttpGet("exercise/{exerciseId}")]
    public async Task<ActionResult<List<ProgressOut>>> GetProgressByExercise(int exerciseId, DateTime dateFrom, DateTime dateTo)
    {
      var email = GetEmail();
      if (dateFrom.Year == 1) dateFrom = DateTime.Now.AddMonths(-3);
      if (dateTo.Year == 1) dateTo = DateTime.Now;
      var series = await seriesRepository.GetSeriesByDatesAsync(email, exerciseId, dateFrom, dateTo);
      List<ProgressOut> p = new List<ProgressOut>();

      foreach (var date in series.Select(d => d.ScheduleExercise.ScheduleTraining.TrainingDate).Distinct())
      {

        var pr = new ProgressOut
        {
          Date = date.ToString("MM-dd"),
          Type = series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.ScheduleExercise.Exercise.Name).Take(1).Single(),
          LoadAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Load).Average(), 2),
          RepeatAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Repeats).Average(), 2),
          TimeAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Time).Average(), 2),
          RestTimeAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.RestTime).Average(), 2),
          DistanseAv = Math.Round(series.Where(ser => ser.ScheduleExercise.ScheduleTraining.TrainingDate == date).Select(select => select.Distance).Average(), 2),
        };
        p.Add(pr);

      }
      return p;

    }

    /// <summary>
    /// GET: api/progress/2020-09-01/2020-10-01
    /// Find and return prepared series between dates
    /// for now NOT USED
    /// </summary>
    /// <param name="dateFrom">start date default -3 month from now</param>
    /// <param name="dateTo">end date default now</param>
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
