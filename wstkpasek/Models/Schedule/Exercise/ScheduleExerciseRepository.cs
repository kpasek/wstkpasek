using wstkp.Models.Database;
using wstkp.Models.Schedule.Exercise;
using wstkp.Models.Schedule.Training;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkp.Models.Schedule.Exercise
{
  public class ScheduleExerciseRepository : IScheduleExerciseRepository
  {
    private readonly AppDBContext db;

    public ScheduleExerciseRepository(AppDBContext db)
    {
      this.db = db;
    }

    public async Task<ScheduleExercise> GetScheduleExerciseAsync(int scheduleExerciseId, string email)
    {
      var c = db.ScheduleExercises
        .Include(h => h.Exercise)
        .Include(t => t.ScheduleTraining)
        .Where(h => h.ScheduleExerciseId == scheduleExerciseId && h.UserEmail == email);
      return await c.AnyAsync() ? await c.SingleAsync() : null;
    }

    public async Task<List<ScheduleExercise>> GetScheduleExerciseForScheduleTrainingAsync(int scheduleTrainingId, string email)
    {
      var c = db.ScheduleExercises
        .Include(h => h.Exercise)
        .Include(h => h.ScheduleTraining)
        .Where(h => h.ScheduleTrainingId == scheduleTrainingId && h.UserEmail == email)
        .OrderBy(o => o.Order);
      return await c.AnyAsync() ? await c.ToListAsync() : null;
    }

    public async Task<int> GetMaxOrderForScheduleExerciseAsync(int scheduleTrainingId, string email)
    {
      var kol = db.ScheduleExercises.Where(h => h.ScheduleTrainingId == scheduleTrainingId && h.UserEmail == email);
      return !await kol.AnyAsync() ? -1 : await kol.MaxAsync(m => m.Order);
    }

    public async Task<ScheduleExercise> GetScheduleExerciseByOrderAsync(int order, int scheduleTrainingId, string email)
    {
      var hc = db.ScheduleExercises.Where(c => c.ScheduleTrainingId == scheduleTrainingId && c.Order == order && c.UserEmail == email);
      if (!await hc.AnyAsync()) return null;
      return await hc.SingleAsync();
    }

    public async Task<List<ScheduleExercise>> GetScheduleExercisesAdminAsync(string user, string name, bool started, string part, DateTime start, DateTime end)
    {
      if (start.Year == 1) start = DateTime.Now.AddMonths(-1);
      if (end.Year == 1) end = DateTime.Now.AddDays(7);

      var exercises = db.ScheduleExercises.Include(i => i.Exercise)
        .Include(ii => ii.ScheduleTraining)
        .Where(e => EF.Functions.ILike(e.UserEmail, $"%{user}%")
                    && EF.Functions.ILike(e.Exercise.Name, $"%{name}%")
                    && e.Started == started
                    && EF.Functions.ILike(e.Exercise.PartId, $"%{part}%")
                    && e.ScheduleTraining.TrainingDate >= start
                    && e.ScheduleTraining.TrainingDate <= end)
        .OrderBy(o => o.UserEmail)
        .ThenBy(tb => tb.Exercise.Name)
        .Take(50);
      return await exercises.AnyAsync() ? await exercises.ToListAsync() : null;
    }
  }
}
