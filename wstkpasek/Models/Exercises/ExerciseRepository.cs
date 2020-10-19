using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wstkp.Models.Database;
using wstkp.Models.SeriesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace wstkp.Models.Exercises
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
              .Where(c => c.UserEmail == email && c.Part.Name == partId);
            return !e.Any() ? null : e.ToList();
        }
        public List<Exercise> GetExerciseByPart(Part part, string email)
        {
            return db.Exercises.Where(c => c.UserEmail == email && c.Part == part).ToList();
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
            foreach (var par in from exercise in exercises where !parts.Exists(p => p.Name == exercise.PartId) select new Part
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
            return db.Types.Where(r => r.UserEmail == email || r.Public).ToList();
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
  }
}
