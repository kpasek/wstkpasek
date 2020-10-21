using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wstkpasek.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace wstkpasek.Models.SeriesModel
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppDBContext db;

        public SeriesRepository(AppDBContext appDb)
        {
            this.db = appDb;
        }

        public int GetOrderForExercise(int exerciseId, int seriesId, string email)
        {
            var series = db.Series.Where(s => s.ExerciseId == exerciseId && s.SeriesId == seriesId && s.UserEmail == email);
            return series.Any() ? 0 : series.Single().Order;
        }

        public async Task<List<Series>> GetSeriesForExerciseAsync(int exerciseId, string email)
        {
            var series = db.Series.Where(s => s.ExerciseId == exerciseId && s.UserEmail == email).OrderBy(o => o.Order);
            return await series.AnyAsync() ? await series.ToListAsync() : new List<Series>();

        }

        public async Task<Series> GetSeriesByOrderAsync(int exerciseId, int seriesOrder, string email)
        {
            var series = db.Series.Where(s => s.ExerciseId == exerciseId && s.Order == seriesOrder && s.UserEmail == email);
            return await series.AnyAsync() ? await series.SingleAsync() : null;
        }

        public async Task<int> GetMaxOrderInExerciseAsync(int exerciseId, string email)
        {
            var series = db.Series.Where(s => s.ExerciseId == exerciseId && s.UserEmail == email);
            return await series.AnyAsync() ? await series.Select(s => s.Order).MaxAsync() : 0;
        }

        public Series GetSeries(int seriesId, string email)
        {
            return db.Series.Single(s => s.SeriesId == seriesId && s.UserEmail == email);
        }

        public List<Series> GetSeries(string email)
        {
            return db.Series.Where(s => s.UserEmail == email).ToList();
        }
        public bool CheckSeriesOwner(int seriesId, string email)
        {
            return db.Series.Where(w => w.SeriesId == seriesId && w.UserEmail == email).Any();
        }


        public async Task<List<Series>> GetSeriesAdminAsync(string User, string ExerciseName)
        {
            var series = db.Series
              .Include(i => i.Exercise)
              .Where(s => EF.Functions.ILike(s.UserEmail, $"%{User}%") && EF.Functions.ILike(s.Exercise.Name, $"%{ExerciseName}%"))
              .Take(50);
            return await series.AnyAsync() ? await series.ToListAsync() : null;
        }
    }
}
