using System.Collections.Generic;
using System.Threading.Tasks;
using wstkp.Models.SeriesModel;

namespace wstkp.Models.Exercises
{
  public interface IExerciseRepository
  {
    List<Exercise> GetExercises(string email);
    List<Exercise> GetExerciseByPart(Part part, string email);
    List<Exercise> GetExerciseByPart(string partId, string email);
    Exercise GetExercise(int exerciseId, string email);
    List<Part> GetParts(string email);
    Part GetPart(string partId, string email);
    Type GetType(string typeId, string email);
    List<Type> GetTypes(string email);
    Task<bool> CheckExerciseAsync(int exerciseId, string email);
    Task<bool> PartsExists();
    Task<List<Exercise>> GetPublicExercisesAsync();
    Task<List<Exercise>> GetExercisesAdminAsync(string User, string Name, string Part);
    Task<List<Part>> GetPartsAdminAsync();
    Task<List<Type>> GetTypesAdminAsync();
  }
}
