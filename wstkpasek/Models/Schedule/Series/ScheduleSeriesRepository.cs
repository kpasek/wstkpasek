using wstkpasek.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.Schedule.Series
{
    public class ScheduleSeriesRepository : IScheduleSeriesRepository
    {
        private readonly AppDBContext db;

        public ScheduleSeriesRepository(AppDBContext db)
        {
            this.db = db;
        }

        public async Task<ScheduleSeries> GetScheduleSeriesAsync(int seriesId, string email)
        {
            var scheduleSeries = db.ScheduleSeries.Where(s => s.ScheduleSeriesId == seriesId && s.UserEmail == email);
            return !await scheduleSeries.AnyAsync() ? null : await scheduleSeries.SingleAsync();
        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesForScheduleTrainingAsync(int scheduleTrainingId, string email)
        {
            var scheduleSeries = db.ScheduleSeries
              .Include(c => c.ScheduleExercise)
              .ThenInclude(t => t.ScheduleTraining)
              .Include(t => t.ScheduleExercise)
              .ThenInclude(c => c.Exercise)
              .Where(s => s.ScheduleExercise.ScheduleTrainingId == scheduleTrainingId && s.UserEmail == email)
              .OrderBy(o => o.ScheduleExercise.Order)
              .ThenBy(t => t.Order);
            return !await scheduleSeries.AnyAsync() ? null : await scheduleSeries.ToListAsync();
        }

        public async Task<ScheduleSeries> GetScheduleSeriesByOrderAsync(int scheduleExerciseId, int order, string email)
        {
            var scheduleSeries = db.ScheduleSeries
            .Where(s => s.ScheduleExerciseId == scheduleExerciseId && s.Order == order && s.UserEmail == email);
            return !await scheduleSeries.AnyAsync() ? null : await scheduleSeries.SingleAsync();
        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesForScheduleExerciseAsync(int scheduleExerciseId, string email)
        {
            var scheduleSeries = db.ScheduleSeries
              .Include(s => s.ScheduleExercise)
              .ThenInclude(t => t.ScheduleTraining)
              .Where(s => s.ScheduleExerciseId == scheduleExerciseId && s.UserEmail == email)
              .OrderBy(o => o.Order);
            if (!await scheduleSeries.AnyAsync()) return null;
            return await scheduleSeries.ToListAsync();
        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesInLastTrainingAsync(ScheduleSeries series, string email)
        {
            var scheduleExercises = db.ScheduleExercises
              .Include(i => i.ScheduleTraining)
              .Where(exercise => exercise.ExerciseId == series.ScheduleExercise.ExerciseId
                    && exercise.ScheduleTraining.TrainingDate < series.ScheduleExercise.ScheduleTraining.TrainingDate)
              .OrderByDescending(o => o.ScheduleTraining.TrainingDate).Take(1);
            if (!await scheduleExercises.AnyAsync()) return new List<ScheduleSeries>();
            var scheduleSeries = db.ScheduleSeries
              .Include(e => e.ScheduleExercise)
              .ThenInclude(tie => tie.Exercise)
              .Include(t => t.ScheduleExercise)
              .ThenInclude(ti => ti.ScheduleTraining)
              .Where(s => scheduleExercises.Contains(s.ScheduleExercise));
            return !await scheduleSeries.AnyAsync() ? new List<ScheduleSeries>() : await scheduleSeries.ToListAsync();
        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesByYearAsync(int year, string email)
        {
            var scheduleSeries = db.ScheduleSeries
                .Include(c => c.ScheduleExercise)
                .ThenInclude(t => t.ScheduleTraining)
                .Where(s => s.ScheduleExercise.ScheduleTraining.TrainingDate.Year == year && s.UserEmail == email);
            if (!await scheduleSeries.AnyAsync()) return new List<ScheduleSeries>();
            return await scheduleSeries.ToListAsync();
        }

        public async Task<int> GetMaxOrderForScheduleExerciseAsync(int scheduleExerciseId, string email)
        {
            var max = db.ScheduleSeries.Where(s => s.ScheduleExerciseId == scheduleExerciseId && s.UserEmail == email);
            return !await max.AnyAsync() ? 0 : await max.MaxAsync(m => m.Order);
        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesForLastTrainingsAsync(int count, int trainingId, string email)
        {
            if (count <= 0) return null;
            var trainings = db.ScheduleTrainings
              .Where(w => w.UserEmail == email && w.TrainingId == trainingId && w.Finish)
              .OrderByDescending(o => o.TrainingDate)
              .Take(count);
            if (!await trainings.AnyAsync()) return null;
            var series = db.ScheduleSeries
              .Include(i => i.ScheduleExercise)
              .ThenInclude(ti => ti.ScheduleTraining)
              .Include(i => i.ScheduleExercise)
              .ThenInclude(ti => ti.Exercise)
              .Where(s => trainings.Contains(s.ScheduleExercise.ScheduleTraining));
            return await series.AnyAsync() ? await series.ToListAsync() : null;

        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesForLastTrainingsAsync(int count, int trainingId, DateTime date, string email)
        {
            if (count <= 0) return null;
            var trainings = db.ScheduleTrainings
              .Where(w => w.UserEmail == email && w.TrainingId == trainingId && w.Finish && w.TrainingDate < date)
              .OrderByDescending(o => o.TrainingDate)
              .Take(count);
            if (!await trainings.AnyAsync()) return null;
            var series = db.ScheduleSeries
              .Include(i => i.ScheduleExercise)
              .ThenInclude(ti => ti.ScheduleTraining)
              .Include(i => i.ScheduleExercise)
              .ThenInclude(ti => ti.Exercise)
              .Where(s => trainings.Contains(s.ScheduleExercise.ScheduleTraining));
            return await series.AnyAsync() ? await series.ToListAsync() : null;

        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesForExerciseAsync(int exerciseId, int count, string email)
        {
            if (count <= 0) return null;
            var exercises = db.ScheduleExercises
              .Include(i => i.ScheduleTraining)
              .Where(w => w.UserEmail == email && w.ScheduleTraining.Finish && w.ExerciseId == exerciseId)
              .OrderByDescending(o => o.ScheduleTraining.TrainingDate)
              .Take(count);
            if (!await exercises.AnyAsync()) return null;
            var series = db.ScheduleSeries
              .Include(i => i.ScheduleExercise)
              .ThenInclude(ti => ti.ScheduleTraining)
              .Where(s => exercises.Contains(s.ScheduleExercise));
            return await series.AnyAsync() ? await series.ToListAsync() : null;
        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesByYearMonthAsync(int year, int month, string email)
        {
            var scheduleSeries = db.ScheduleSeries
          .Include(c => c.ScheduleExercise)
          .ThenInclude(t => t.ScheduleTraining)
          .Where(s => s.ScheduleExercise.ScheduleTraining.TrainingDate.Year == year && s.ScheduleExercise.ScheduleTraining.TrainingDate.Month == month && s.UserEmail == email);
            if (!await scheduleSeries.AnyAsync()) return new List<ScheduleSeries>();
            return await scheduleSeries.ToListAsync();
        }

        public async Task<List<ScheduleSeries>> GetScheduleSeriesAdminAsync(string user, string name, string part, bool finish, DateTime startDate, DateTime endDate)
        {
            if (startDate.Year == 1) startDate = DateTime.Now.AddMonths(-1);
            if (endDate.Year == 1) endDate = DateTime.Now.AddDays(7);

            var series = db.ScheduleSeries.Include(i => i.ScheduleExercise)
                                          .ThenInclude(ii => ii.ScheduleTraining)
                                          .Include(iii => iii.ScheduleExercise)
                                          .ThenInclude(iiii => iiii.Exercise)
                                          .Where(s => EF.Functions.ILike(s.UserEmail, $"%{user}%")
                                                      && EF.Functions.ILike(s.ScheduleExercise.Exercise.Name, $"%{name}%")
                                                      && EF.Functions.ILike(s.ScheduleExercise.Exercise.PartId, $"%{part}%")
                                                      && s.Finish == finish
                                                      && s.ScheduleExercise.ScheduleTraining.TrainingDate >= startDate
                                                      && s.ScheduleExercise.ScheduleTraining.TrainingDate <= endDate)
                                          .OrderBy(o => o.UserEmail)
                                          .ThenByDescending(tbb => tbb.ScheduleExercise.ScheduleTraining.TrainingDate)
                                          .ThenBy(tb => tb.ScheduleExercise.Exercise.Name)
                                          .Take(50);
            return await series.AnyAsync() ? await series.ToListAsync() : null;
        }
        public async Task<List<ScheduleSeries>> GetSeriesByDatesAsync(string email, DateTime? datefrom, DateTime? dateTo)
        {
            if (!dateTo.HasValue) dateTo = DateTime.Now;
            if (!datefrom.HasValue) datefrom = DateTime.Now.AddMonths(-3);
            var series = db.ScheduleSeries
            .Include(ei => ei.ScheduleExercise)
            .ThenInclude(ti => ti.ScheduleTraining)
            .Include(e => e.ScheduleExercise)
            .ThenInclude(ex => ex.Exercise)
            .Where(s => s.ScheduleExercise.ScheduleTraining.TrainingDate >= datefrom && s.ScheduleExercise.ScheduleTraining.TrainingDate <= dateTo && s.UserEmail == email);

            return await series.AnyAsync() ? await series.ToListAsync() : new List<ScheduleSeries>();
        }

        public async Task<List<ScheduleSeries>> GetSeriesByDatesAsync(string email, string part, DateTime? datefrom, DateTime? dateTo)
        {
            if (!dateTo.HasValue) dateTo = DateTime.Now;
            if (!datefrom.HasValue) datefrom = DateTime.Now.AddMonths(-3);
            var series = db.ScheduleSeries
            .Include(ei => ei.ScheduleExercise)
            .ThenInclude(ti => ti.ScheduleTraining)
            .Include(e => e.ScheduleExercise)
            .ThenInclude(ex => ex.Exercise)
            .Where(s => s.ScheduleExercise.ScheduleTraining.TrainingDate >= datefrom && s.ScheduleExercise.ScheduleTraining.TrainingDate <= dateTo && s.ScheduleExercise.Exercise.PartId == part && s.Finish == true && s.UserEmail == email)
            .OrderBy(o => o.ScheduleExercise.Exercise.Name);

            return await series.AnyAsync() ? await series.ToListAsync() : new List<ScheduleSeries>();
        }
        public async Task<List<ScheduleSeries>> GetSeriesByDatesAsync(string email, int exerciseId, DateTime? datefrom, DateTime? dateTo)
        {
            if (!dateTo.HasValue) dateTo = DateTime.Now;
            if (!datefrom.HasValue) datefrom = DateTime.Now.AddMonths(-3);
            var series = db.ScheduleSeries
            .Include(ei => ei.ScheduleExercise)
            .ThenInclude(ti => ti.ScheduleTraining)
            .Include(e => e.ScheduleExercise)
            .ThenInclude(ex => ex.Exercise)
            .Where(s => s.ScheduleExercise.ScheduleTraining.TrainingDate >= datefrom && s.ScheduleExercise.ScheduleTraining.TrainingDate <= dateTo && s.ScheduleExercise.Exercise.ExerciseId == exerciseId && s.Finish == true && s.UserEmail == email)
            .OrderBy(o => o.ScheduleExercise.Exercise.Name);

            return await series.AnyAsync() ? await series.ToListAsync() : new List<ScheduleSeries>();
        }
    }
}
