using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wstkp.Models.Exercises;

namespace wstkp.Models.TrainingModel
{
  public interface ITrainingRepository
  {
    Task<Training> GetTraining(int trainingId, string email);
    Task<List<Training>> GetTrainings(string email);
    bool CheckTrainingOwner(int trainingId, string email);
    Task<List<Exercise>> GetExerciseInTrainingAsync(int trainingId, string email);
    Task<Exercise> FindExercise(Exercise cw, string email);
    Task<TrainingExercise> GetTrainingExerciseAsync(int trainingId, int exerciseId, string email);
    Task<TrainingExercise> GetTrainingExerciseByOrderAsync(int trainingId, int order, string email);
    Task<int> GetMaxOrderInTrainingAsync(int trainingId, string email);
    Task<List<Training>> GetTrainingByPartAndExerciseAsync(int exerciseId, string part, string email);
    Task<List<Training>> GetTrainingByPartAsync(string part, string email);
    Task<List<Training>> GetTrainingByExerciseAsync(int exerciseId, string email);
    Task<List<TrainingExercise>> GetExercisesTrainingsAsync(int trainingId, string email);
    Task<List<Training>> GetPublicTrainingsAsync();
    Task<List<Training>> GetTrainingsAdminAsync(string User, string Name);
  }
}
