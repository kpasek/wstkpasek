using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.In;
using wstkpasek.Models.Out;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.SeriesModel;
using wstkpasek.Models.TrainingModel;

namespace wstkpasek.Controllers
{
  [Route("api/schedule/trainings")]
  [ApiController]
  [Authorize]
  public class ScheduleTrainingsController : ControllerBase
  {
    private readonly AppDBContext _context;
    private readonly IScheduleTrainingRepository scheduleTrainingRepository;
    private readonly IScheduleSeriesRepository scheduleSeriesRepository;
    private readonly ITrainingRepository trainingRepository;
    private readonly ISeriesRepository seriesRespository;

    public ScheduleTrainingsController(AppDBContext context, IScheduleTrainingRepository scheduleTrainingRepository, IScheduleSeriesRepository scheduleSeriesRepository, ITrainingRepository trainingRepository, ISeriesRepository seriesRespository)
    {
      _context = context;
      this.scheduleTrainingRepository = scheduleTrainingRepository;
      this.scheduleSeriesRepository = scheduleSeriesRepository;
      this.trainingRepository = trainingRepository;
      this.seriesRespository = seriesRespository;
    }

    private string GetEmail()
    {
      return this.User.Identity.Name;
    }

    // GET: api/ScheduleTrainings
    [HttpGet("{year}/{month}")]
    public async Task<ActionResult<IEnumerable<ScheduleTraining>>> GetScheduleTrainings(int year, int month)
    {
      var email = GetEmail();
      var trainings = await scheduleTrainingRepository.GetScheduleTrainingsForMonthAndYearAsync(year, month, email);
      return trainings;
    }

    // GET: api/ScheduleTrainings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleTraining>> GetScheduleTraining(int id)
    {
      var scheduleTraining = await _context.ScheduleTrainings.FindAsync(id);

      if (scheduleTraining == null)
      {
        return NotFound();
      }

      return scheduleTraining;
    }

    /// <summary>
    /// GET: api/schedule/trainings/start/{id}
    /// prepare list of series to run training
    /// depending on user settings series sorted by:
    /// number of current exercises in training
    /// number of finished series
    /// all not finished series include last series history
    /// from user settings model return next training date and time
    /// </summary>
    /// <param name="id">schedule training id required</param>
    /// <returns>model iclude 3 lists: series current, to complete, finished</returns>
    [HttpGet("start/{id}")]
    public async Task<ActionResult<StartOutModel>> GetStartTrainingSeries(int id)
    {
      var email = GetEmail();
      if (id <= 0) return BadRequest();

      var model = new StartOutModel();
      var scheduleTraining = await scheduleTrainingRepository.GetScheduleTrainingAsync(id, email);
      var training = await trainingRepository.GetTraining(scheduleTraining.TrainingId, email);
      model.Training = scheduleTraining;
      var series = await scheduleSeriesRepository.GetScheduleSeriesForScheduleTrainingAsync(id, email);
      var maxExerciseOrder = series.Select(s => s.ScheduleExercise).Max(m => m.Order);
      var maxSeriesOrder = series.Max(m => m.Order);
      var scheduleSeriesArray = new ScheduleSeries[maxExerciseOrder, maxSeriesOrder];
      foreach (var s in series)
        scheduleSeriesArray[s.ScheduleExercise.Order - 1, s.Order - 1] = s;
      model.SeriesList = new List<ScheduleSeriesHistory>();
      model.SeriesToComplete = new List<ScheduleSeriesHistory>();
      model.SeriesCompleted = new List<ScheduleSeriesHistory>();
      var task = Task.Run(async () =>
      {
        for (var i = 0; i < maxExerciseOrder; i += model.Training.ExerciseNumber)
        {
          for (var s = 0; s < maxSeriesOrder; s++)
          {
            for (var c = i; c < model.Training.ExerciseNumber + i; c++)
            {
              try
              {
                if (model.SeriesList.Count < scheduleTraining.ExerciseNumber && scheduleSeriesArray[c, s] != null && !scheduleSeriesArray[c, s].Finish)
                {
                  model.SeriesList.Add(new ScheduleSeriesHistory
                  {
                    ScheduleSeriesId = scheduleSeriesArray[c, s].ScheduleSeriesId,
                    ScheduleExerciseId = scheduleSeriesArray[c, s].ScheduleExerciseId,
                    ScheduleExercise = scheduleSeriesArray[c, s].ScheduleExercise,
                    Distance = scheduleSeriesArray[c, s].Distance,
                    Finish = scheduleSeriesArray[c, s].Finish,
                    Intensity = scheduleSeriesArray[c, s].Intensity,
                    Load = scheduleSeriesArray[c, s].Load,
                    Name = scheduleSeriesArray[c, s].Name,
                    Order = scheduleSeriesArray[c, s].Order,
                    Repeats = scheduleSeriesArray[c, s].Repeats,
                    RestTime = scheduleSeriesArray[c, s].RestTime,
                    Time = scheduleSeriesArray[c, s].Time,
                    UserEmail = scheduleSeriesArray[c, s].UserEmail,
                  });
                  var hist = await scheduleSeriesRepository.GetScheduleSeriesInLastTrainingAsync(scheduleSeriesArray[c, s], email);
                  model.SeriesList[model.SeriesList.Count - 1].History = hist.OrderBy(o => o.Order).ToList();

                }
                else if (scheduleSeriesArray[c, s] != null && !scheduleSeriesArray[c, s].Finish)
                {
                  model.SeriesToComplete.Add(new ScheduleSeriesHistory
                  {
                    ScheduleSeriesId = scheduleSeriesArray[c, s].ScheduleSeriesId,
                    ScheduleExerciseId = scheduleSeriesArray[c, s].ScheduleExerciseId,
                    ScheduleExercise = scheduleSeriesArray[c, s].ScheduleExercise,
                    Distance = scheduleSeriesArray[c, s].Distance,
                    Finish = scheduleSeriesArray[c, s].Finish,
                    Intensity = scheduleSeriesArray[c, s].Intensity,
                    Load = scheduleSeriesArray[c, s].Load,
                    Name = scheduleSeriesArray[c, s].Name,
                    Order = scheduleSeriesArray[c, s].Order,
                    Repeats = scheduleSeriesArray[c, s].Repeats,
                    RestTime = scheduleSeriesArray[c, s].RestTime,
                    Time = scheduleSeriesArray[c, s].Time,
                    UserEmail = scheduleSeriesArray[c, s].UserEmail,
                  });
                  var hist = await scheduleSeriesRepository.GetScheduleSeriesInLastTrainingAsync(scheduleSeriesArray[c, s], email);
                  model.SeriesToComplete[model.SeriesToComplete.Count - 1].History = hist.OrderBy(o => o.Order).ToList();
                }
                else if (scheduleSeriesArray[c, s] != null)
                {
                  model.SeriesCompleted.Add(new ScheduleSeriesHistory
                  {
                    ScheduleSeriesId = scheduleSeriesArray[c, s].ScheduleSeriesId,
                    ScheduleExerciseId = scheduleSeriesArray[c, s].ScheduleExerciseId,
                    ScheduleExercise = scheduleSeriesArray[c, s].ScheduleExercise,
                    Distance = scheduleSeriesArray[c, s].Distance,
                    Finish = scheduleSeriesArray[c, s].Finish,
                    Intensity = scheduleSeriesArray[c, s].Intensity,
                    Load = scheduleSeriesArray[c, s].Load,
                    Name = scheduleSeriesArray[c, s].Name,
                    Order = scheduleSeriesArray[c, s].Order,
                    Repeats = scheduleSeriesArray[c, s].Repeats,
                    RestTime = scheduleSeriesArray[c, s].RestTime,
                    Time = scheduleSeriesArray[c, s].Time,
                    UserEmail = scheduleSeriesArray[c, s].UserEmail,
                  });
                  var hist = await scheduleSeriesRepository.GetScheduleSeriesInLastTrainingAsync(scheduleSeriesArray[c, s], email);
                  model.SeriesCompleted[model.SeriesCompleted.Count - 1].History = hist.OrderBy(o => o.Order).ToList();
                }

              }
              catch (Exception)
              {
                // ignored
              }
            }
          }
        }
      });
      await task;
      var settings = _context.Settings.Single(u => u.UserEmail == email);
      var hour = settings.TrainingHour;
      var minute = settings.TrainingMinute;
      var days = settings.TrainingDayInterval;
      model.NextTrainingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0).AddDays(days);
      return model;
    }

    // PUT: api/ScheduleTrainings/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutScheduleTraining(int id, ScheduleTraining scheduleTraining)
    {
      var email = GetEmail();
      scheduleTraining.UserEmail = email;
      if (id != scheduleTraining.ScheduleTrainingId)
      {
        return BadRequest();
      }

      _context.Entry(scheduleTraining).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ScheduleTrainingExists(id))
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
    /// POST: api/schedule/trainings/finish
    /// finish schedule training
    /// mark all series as finish
    /// depend on user choose:
    /// create new training in new date
    /// update all series in training plan
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("finish")]
    public async Task<ActionResult> FinishTraining(FinishTrainingIn form)
    {
      var email = GetEmail();
      var mScheduleTrainingTask = scheduleTrainingRepository.GetScheduleTrainingAsync(form.ScheduleTrainingId, email);
      var mScheduleTraining = await mScheduleTrainingTask;
      var seriesTask = scheduleSeriesRepository.GetScheduleSeriesForScheduleTrainingAsync(form.ScheduleTrainingId, email);

      mScheduleTraining.TrainingFinishDate = form.FinishDate;
      mScheduleTraining.Finish = true;

      var seriesInFinishedTraining = await seriesTask;
      var seriesToComplete = new List<ScheduleSeries>();
      var scheduleExercises = new List<Models.Schedule.Exercise.ScheduleExercise>();
      var scheduleTraining = new ScheduleTraining();
      var t = await trainingRepository.GetTraining(mScheduleTraining.TrainingId, email);
      if (form.CreateNextTraining)
      {
        scheduleTraining.TrainingDate = form.NextTrainingDate;
        scheduleTraining.ExerciseNumber = mScheduleTraining.ExerciseNumber;
        scheduleTraining.Name = t.Name;
        scheduleTraining.Finish = false;
        scheduleTraining.UserEmail = email;
        scheduleTraining.TrainingId = mScheduleTraining.TrainingId;
        var addTrainingTask = _context.ScheduleTrainings.AddAsync(scheduleTraining);
        await addTrainingTask;
      }
      var exercisesList = new List<Models.Schedule.Exercise.ScheduleExercise>();
      var seriesList = new List<ScheduleSeries>();


      foreach (var series in seriesInFinishedTraining)
      {
        //Zakonczenie wszystkich serii
        if (!series.Finish)
        {
          series.Finish = true;
          seriesToComplete.Add(series);
        }
        if (form.CreateNextTraining)
        {
          if (exercisesList.Find(e =>
                  e.ExerciseId == series.ScheduleExercise.ExerciseId &&
                  e.Order == series.ScheduleExercise.Order) ==
              null)
          {
            //przekopiowanie cwiczen do nowego treningu
            var c = new Models.Schedule.Exercise.ScheduleExercise
            {
              ExerciseId = series.ScheduleExercise.ExerciseId,
              ScheduleTraining = scheduleTraining,
              Order = series.ScheduleExercise.Order,
              UserEmail = email
            };
            exercisesList.Add(c);
          }
          //przekopiowanie serii do nowego treningu
          var s = new ScheduleSeries
          {
            Time = series.Time,
            Distance = series.Distance,
            ScheduleExercise = exercisesList.Find(c =>
              c.ExerciseId == series.ScheduleExercise.ExerciseId && c.Order == series.ScheduleExercise.Order),
            Repeats = series.Repeats,
            Order = series.Order,
            Load = series.Load,
            RestTime = series.RestTime,
            Name = CreateSeriesName(series),
            UserEmail = email
          };
          seriesList.Add(s);
        }
        //aktualizacja serii
        if (!form.UpdateSeries) continue;
        var se = await seriesRespository.GetSeriesForExerciseAsync(series.ScheduleExercise.ExerciseId, email);
        var newSeries = new Series
        {
          Time = series.Time,
          Distance = series.Distance,
          Repeats = series.Repeats,
          Name = series.Name,
          Load = series.Load,
          RestTime = series.RestTime,
          Order = series.Order,
          ExerciseId = series.ScheduleExercise.ExerciseId,
          UserEmail = email
        };
        if (se != null)
        {
          _context.RemoveRange(se);
        }
        await _context.Series.AddAsync(newSeries);
      }
      _context.UpdateRange(seriesToComplete);
      _context.Update(mScheduleTraining);
      await _context.SaveChangesAsync();

      if (!form.CreateNextTraining) return Ok();
      var addScheduleExerciseTask = _context.AddRangeAsync(scheduleExercises.Distinct());
      await addScheduleExerciseTask;

      var addScheduleSeriesTask = _context.ScheduleSeries.AddRangeAsync(seriesList);
      await addScheduleSeriesTask;

      await _context.SaveChangesAsync();
      return Ok();
    }

    // POST: api/ScheduleTrainings
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<ScheduleTraining>> PostScheduleTraining(ScheduleTraining model)
    {
      var email = GetEmail();
      var scheduleTraining = await NewScheduleTraining(model.TrainingId, model.TrainingDate, email);
      var trainingExercises = await trainingRepository.GetExercisesTrainingsAsync(model.TrainingId, email);
      if (trainingExercises == null) return Ok();
      foreach (var trainingExercise in trainingExercises)
      {
        var scheduleExercise = await NewScheduleExercise(email, scheduleTraining, trainingExercise);
        var series = await seriesRespository.GetSeriesForExerciseAsync(trainingExercise.ExerciseId, email);
        if (series == null) continue;
        foreach (var s in series) await NewSeries(email, scheduleExercise, s);
      }

      return CreatedAtAction("GetScheduleTraining", new { id = scheduleTraining.ScheduleTrainingId }, scheduleTraining);
    }

    // DELETE: api/ScheduleTrainings/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleTraining>> DeleteScheduleTraining(int id)
    {
      var scheduleTraining = await _context.ScheduleTrainings.FindAsync(id);
      if (scheduleTraining == null)
      {
        return NotFound();
      }

      _context.ScheduleTrainings.Remove(scheduleTraining);
      await _context.SaveChangesAsync();

      return scheduleTraining;
    }
    private async Task<ScheduleTraining> NewScheduleTraining(int trainingId, DateTime date, string email)
    {
      var training = await trainingRepository.GetTraining(trainingId, email);
      var newScheduleTraining = new ScheduleTraining
      {
        UserEmail = email,
        TrainingId = trainingId,
        TrainingDate = date,
        Name = training.Name
      };
      await _context.ScheduleTrainings.AddAsync(newScheduleTraining);
      await _context.SaveChangesAsync();
      return newScheduleTraining;
    }
    private async Task<Models.Schedule.Exercise.ScheduleExercise> NewScheduleExercise(string email, ScheduleTraining scheduleTraining,
        TrainingExercise trainingExercise)
    {
      var nScheduleExercise = new Models.Schedule.Exercise.ScheduleExercise
      {
        ExerciseId = trainingExercise.ExerciseId,
        ScheduleTraining = scheduleTraining,
        UserEmail = email,
        Order = trainingExercise.Order
      };
      await _context.ScheduleExercises.AddAsync(nScheduleExercise);
      await _context.SaveChangesAsync();
      return nScheduleExercise;
    }
    private async Task NewSeries(string email, Models.Schedule.Exercise.ScheduleExercise scheduleExercise, Series series)
    {
      var nScheduleSeries = new ScheduleSeries
      {
        ScheduleExercise = scheduleExercise,
        Name = series.Name,
        Repeats = series.Repeats,
        Load = series.Load,
        Time = series.Time,
        Distance = series.Distance,
        RestTime = series.RestTime,
        Order = series.Order,
        UserEmail = email
      };
      await _context.ScheduleSeries.AddAsync(nScheduleSeries);
      await _context.SaveChangesAsync();
    }
    private string CreateSeriesName(ScheduleSeries series)
    {
      var name = "";
      if (series.Repeats > 0) name += series.Repeats + "x ";
      if (series.Load > 0) name += series.Load + "kg ";
      if (series.Time > 0) name += series.Time + "s ";
      if (series.Distance > 0) name += series.Distance + "m ";
      if (series.RestTime > 0) name += series.RestTime + "s odp.";
      return name.Trim();
    }
    private bool ScheduleTrainingExists(int id)
    {
      return _context.ScheduleTrainings.Any(e => e.ScheduleTrainingId == id);
    }
  }
}
