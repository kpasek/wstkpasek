using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wstkpasek.Models.Database;
using wstkpasek.Models.SeriesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using wstkpasek.Models.TrainingModel;
using wstkpasek.Models.Out;

namespace wstkpasek.Models.Exercises
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDBContext db;

        public ExerciseRepository(AppDBContext dBContext)
        {
            db = dBContext;
        }

        public Task<bool> PartsExists()
        {
            return db.Parts.AnyAsync();
        }

        public List<Exercise> GetExercises(string email)
        {

            var exercises = db.Exercises.Where(c => c.UserEmail == email).OrderBy(c => c.PartId);
            return !exercises.Any() ? null : exercises.ToList();
        }

        public List<Exercise> GetExerciseByPart(string partId, string email)
        {
            var e = db.Exercises
              .Include(p => p.Part)
              .Where(c => c.UserEmail == email && c.Part.Name == partId)
              .OrderBy(o => o.Name);
            return !e.Any() ? new List<Exercise>() : e.ToList();
        }
        public List<Exercise> GetExerciseByPart(Part part, string email)
        {
            return db.Exercises.Include(i => i.Part).Where(c => c.UserEmail == email && c.Part == part).ToList();
        }

        public Exercise GetExercise(int exerciseId, string email)
        {
            var exercises = db.Exercises.Where(e => e.UserEmail == email && e.ExerciseId == exerciseId);
            return exercises.Any() ? exercises.Single() : null;
        }

        public Part GetPart(string partId, string email)
        {
            var parts = db.Parts.Where(p => (p.UserEmail == email || p.Public) && p.Name == partId);
            try
            {
                return parts.Single();
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<Part> GetParts(string email)
        {
            var exercises = db.Exercises.Where(c => c.UserEmail == email || c.Public).ToList();
            var parts = new List<Part>();
            foreach (var par in from exercise in exercises
                                where !parts.Exists(p => p.Name == exercise.PartId)
                                select new Part
                                {
                                    Name = exercise.PartId,
                                    UserEmail = email
                                })
            {
                parts.Add(par);
            }
            return parts;
        }

        public Type GetType(string typeId, string email)
        {
            var types = db.Types.Where(t => (t.UserEmail == email || t.Public) && t.TypeName == typeId);
            try
            {
                return types.Single();
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<Type> GetTypes(string email)
        {
            return db.Types.Where(r => r.UserEmail == email || r.Public).Distinct().ToList();
        }


        public async Task<bool> CheckExerciseAsync(int exerciseId, string email)
        {
            var exercises = db.Exercises.Where(c => c.ExerciseId == exerciseId && c.UserEmail == email);
            return await exercises.AnyAsync();
        }

        public async Task<List<Exercise>> GetPublicExercisesAsync()
        {
            var cw = db.Exercises.Where(c => c.Public);
            return !await cw.AnyAsync() ? null : await cw.ToListAsync();
        }

        public async Task<List<Exercise>> GetExercisesAdminAsync(string User, string Name, string Part)
        {
            var exercises = db.Exercises
              .Where(e => EF.Functions.ILike(e.UserEmail, $"%{User}%") && EF.Functions.ILike(e.Name, $"%{Name}%") && EF.Functions.ILike(e.PartId, $"%{Part}%"))
              .OrderByDescending(o => o.Name)
              .Take(50);
            return await exercises.AnyAsync() ? await exercises.ToListAsync() : null;
        }

        public async Task<List<Part>> GetPartsAdminAsync()
        {
            var parts = db.Parts;
            return await parts.AnyAsync() ? await parts.ToListAsync() : null;
        }

        public async Task<List<Type>> GetTypesAdminAsync()
        {
            var types = db.Types;
            return await types.AnyAsync() ? await types.ToListAsync() : null;
        }

        public async Task<List<ExerciseWithOrder>> GetExercisesForTraining(int trainingId, string email)
        {
            var trainingExercises = db.TrainingExercises
                .Include(tc => tc.Exercise)
                .Where(t => t.TrainingId == trainingId && t.UserEmail == email)
                .OrderBy(t => t.Order);
            if (!await trainingExercises.AnyAsync()) return new List<ExerciseWithOrder>();
            var exercisesWithOrder = trainingExercises.Select(exercise => new ExerciseWithOrder
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Exercise.Name,
                Public = exercise.Exercise.Public,
                Description = exercise.Exercise.Description,
                Order = exercise.Order,
                PartId = exercise.Exercise.PartId,
                TypeId = exercise.Exercise.TypeId
            })
                .ToList();

            return exercisesWithOrder;
        }

        public async Task<int> GetExerciseOrderAsync(int trainingId, int exerciseId, string email)
        {
            var te = db.TrainingExercises.Where(w => w.TrainingId == trainingId && w.ExerciseId == exerciseId && w.UserEmail == email)
                                         .Take(1);
            if (!await te.AnyAsync()) return 0;
            return te.Select(s => s.Order).Single();
        }


        public async Task<List<TrainingExercise>> GetExercisesOrderForTraining(int trainingId, string email)
        {
            var items = db.TrainingExercises.Where(w => w.TrainingId == trainingId && w.UserEmail == email);
            return await items.AnyAsync() ? await items.ToListAsync() : null;
        }
    }
}
