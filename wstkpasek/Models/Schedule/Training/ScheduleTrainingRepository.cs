using wstkp.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace wstkp.Models.Schedule.Training
{
  public class ScheduleTrainingRepository : IScheduleTrainingRepository
  {
    private readonly AppDBContext db;

    public ScheduleTrainingRepository(AppDBContext db)
    {
      this.db = db;
    }

    public async Task<List<ScheduleTraining>> GetScheduleTrainingsForMonthAndYearAsync(int month, int year, string email)
    {
      var scheduleTrainings = db.ScheduleTrainings
        .Where(h => h.TrainingDate.Year == year && h.TrainingDate.Month == month && h.UserEmail == email)
        .OrderBy(h => h.TrainingDate);
      return scheduleTrainings.Any() ? await scheduleTrainings.ToListAsync() : new List<ScheduleTraining>();
    }
    public async Task<ScheduleTraining> GetScheduleTrainingAsync(int scheduleTrainingId, string email)
    {
      var scheduleTrainings = db.ScheduleTrainings
        .Where(h => h.ScheduleTrainingId == scheduleTrainingId && h.UserEmail == email);
      return scheduleTrainings.Any() ? await scheduleTrainings.SingleAsync() : null;
    }

    public async Task<ScheduleTraining> GetNextTraining(string email)
    {
      var scheduleTrainings = db.ScheduleTrainings.Where(t => t.TrainingDate >= DateTime.Now && !t.Finish && t.UserEmail == email);
      if (!await scheduleTrainings.AnyAsync()) return null;
      var training = await scheduleTrainings.Where(tr => tr.TrainingDate == scheduleTrainings.Min(m => m.TrainingDate)).SingleAsync();
      return training;
    }

    public async Task<List<ScheduleTraining>> GetScheduleTrainingByYear(int year, string email)
    {
      var scheduleTrainings = db.ScheduleTrainings
        .Where(h => h.TrainingDate.Year == year && h.UserEmail == email)
        .OrderBy(h => h.TrainingDate);
      if (!scheduleTrainings.Any()) return new List<ScheduleTraining>();
      return await scheduleTrainings.ToListAsync();
    }

    public async Task<List<ScheduleTraining>> GetTrainingHistoryAsync(int count, string email)
    {
      var trainings = db.ScheduleTrainings.Where(t => t.Finish == true).OrderByDescending(o => o.TrainingDate).Take(count);
      try
      {
        return await trainings.ToListAsync();
      }
      catch (System.Exception)
      {
        throw new NullReferenceException("No history found");
      }

    }

    public async Task<List<ScheduleTraining>> GetScheduleTrainingsAdminAsync(string user, string name, bool finish, DateTime startDate, DateTime endDate)
    {
      if(startDate.Year == 1) startDate = DateTime.Now.AddMonths(-1);
      if(endDate.Year == 1) endDate = DateTime.Now.AddDays(7);
      var trainings = db.ScheduleTrainings.Where(s => EF.Functions.ILike(s.UserEmail, $"%{user}%")
                                                      && EF.Functions.ILike(s.Name, $"%{name}%")
                                                      && s.Finish == finish
                                                      && s.TrainingDate >= startDate
                                                      && s.TrainingDate <= endDate)
                                          .OrderBy(o => o.UserEmail)
                                          .ThenByDescending(tb => tb.TrainingDate)
                                          .Take(50);
      return await trainings.AnyAsync() ? await trainings.ToListAsync() : null;
    }
  }
}
