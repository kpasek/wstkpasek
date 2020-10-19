using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkp.Models.Schedule.Exercise
{
  public interface IScheduleExerciseRepository
  {
    Task<ScheduleExercise> GetScheduleExerciseAsync(int scheduleExerciseId, string email);
    Task<List<ScheduleExercise>> GetScheduleExerciseForScheduleTrainingAsync(int scheduleTrainingId, string email);
    Task<int> GetMaxOrderForScheduleExerciseAsync(int scheduleTrainingId, string email);
    Task<ScheduleExercise> GetScheduleExerciseByOrderAsync(int order, int scheduleTrainingId, string email);
    Task<List<ScheduleExercise>> GetScheduleExercisesAdminAsync(string user, string name, bool started, string part, DateTime start, DateTime end);
  }
}
