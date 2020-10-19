using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace wstkpasek.Models.TrainingModel
{
    public class TrainingRepository : ITrainingRepository
    {

        private readonly AppDBContext db;

        public TrainingRepository(AppDBContext db)
        {
            this.db = db;
        }

        public async Task<Training> GetTraining(int trainingId, string email)
        {
            var trainings = db.Trainings.Where(t => t.TrainingId == trainingId && t.UserEmail == email);
            return await trainings.AnyAsync() ? await trainings.SingleAsync() : null;
        }

        public async Task<List<Training>> GetTrainings(string email)
        {
            var trainings = db.Trainings.Where(t => t.UserEmail == email);
            return await trainings.AnyAsync() ? await trainings.ToListAsync() : null;
        }

        public bool CheckTrainingOwner(int trainingId, string email)
        {
            var t = db.Trainings.Where(tr => tr.TrainingId == trainingId && tr.UserEmail == email);
            return t.Any();
        }

        public async Task<List<Exercise>> GetExerciseInTrainingAsync(int trainingId, string email)
        {
            var trainingExercises = db.TrainingExercises
              .Include(tc => tc.Exercise)
              .Where(t => t.TrainingId == trainingId && t.UserEmail == email)
              .OrderBy(t => t.Order);
            if (!await trainingExercises.AnyAsync()) return null;

            var list = await trainingExercises.ToListAsync();
            return list.Select(cw => cw.Exercise).ToList();
        }
        public async Task<Exercise> FindExercise(Exercise cw, string email)
        {
            var exercises = db.Exercises.Where(c => c.Name == cw.Name
                                                    && c.Description == cw.Description
                                                    && c.PartId == cw.PartId
                                                    && c.TypeId == cw.TypeId
                                                    && c.UserEmail == email);

            return exercises.Any() ? await exercises.SingleAsync() : null;
        }

        public async Task<TrainingExercise> GetTrainingExerciseAsync(int trainingId, int exerciseId, string email)
        {
            var trainingExercises = db.TrainingExercises
              .Include(t => t.Training)
              .Include(c => c.Exercise)
              .Where(t => t.TrainingId == trainingId && t.ExerciseId == exerciseId && t.UserEmail == email);
            return !trainingExercises.Any() ? null : await trainingExercises.SingleAsync();
        }

        public async Task<TrainingExercise> GetTrainingExerciseByOrderAsync(int trainingId, int order, string email)
        {
            var trainingExercises = db.TrainingExercises
              .Include(t => t.Training)
              .Include(c => c.Exercise)
              .Where(t => t.TrainingId == trainingId && t.Order == order && t.UserEmail == email);
            return !trainingExercises.Any() ? null : await trainingExercises.SingleAsync();
        }

        public async Task<int> GetMaxOrderInTrainingAsync(int trainingId, string email)
        {
            var trainingExercises = db.TrainingExercises.Where(t => t.TrainingId == trainingId && t.UserEmail == email);
            return !await trainingExercises.AnyAsync() ? 0 : trainingExercises.Max(m => m.Order);
        }

        public async Task<List<Training>> GetTrainingByPartAndExerciseAsync(int exerciseId, string part, string email)
        {
            var tc = db.TrainingExercises
              .Include(c => c.Exercise)
              .ThenInclude(c => c.Part)
              .Include(t => t.Training)
              .Where(t => t.ExerciseId == exerciseId && t.UserEmail == email && t.Exercise.PartId == part);
            if (!await tc.AnyAsync()) return null;
            var lists = await tc.ToListAsync();
            return lists.Select(tr => tr.Training).ToList();
        }

        public async Task<List<Training>> GetTrainingByPartAsync(string part, string email)
        {
            var trainingExercises = db.TrainingExercises
              .Include(c => c.Exercise)
              .ThenInclude(p => p.Part)
              .Include(t => t.Training)
              .Distinct()
              .Where(t => t.UserEmail == email && t.Exercise.PartId == part);
            return !await trainingExercises.AnyAsync()
              ? null
              : trainingExercises.Select(trainingExercise => trainingExercise.Training).Distinct().ToList();
        }

        public async Task<List<Training>> GetTrainingByExerciseAsync(int exerciseId, string email)
        {
            var trainingExercises = db.TrainingExercises
              .Include(t => t.Training)
              .Where(t => t.ExerciseId == exerciseId && t.UserEmail == email);
            if (!await trainingExercises.AnyAsync()) return null;
            return await trainingExercises.Select(trainingExercise => trainingExercise.Training).ToListAsync();
        }
        public async Task<List<TrainingExercise>> GetExercisesTrainingsAsync(int trainingId, string email)
        {
            var trainingExercises = db.TrainingExercises.Include(c => c.Exercise)
              .Where(t => t.TrainingId == trainingId && t.UserEmail == email);
            return !await trainingExercises.AnyAsync() ? null : await trainingExercises.ToListAsync();
        }

        public async Task<List<Training>> GetPublicTrainingsAsync()
        {
            var trainings = db.Trainings.Where(t => t.Public);
            return !await trainings.AnyAsync() ? null : await trainings.ToListAsync();
        }

    public async Task<List<Training>> GetTrainingsAdminAsync(string User, string Name)
    {
      var trainings = db.Trainings.Where(t => EF.Functions.ILike(t.UserEmail, $"%{User}%")
                                              && EF.Functions.ILike(t.Name, $"%{Name}%"))
                                  .Take(50);
      return await trainings.AnyAsync() ? await trainings.ToListAsync() : null;
    }
  }
}
