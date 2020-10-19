using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.Schedule.Training
{
  public interface IScheduleTrainingRepository
  {
    Task<List<ScheduleTraining>> GetScheduleTrainingsForMonthAndYearAsync(int month, int year, string email);
    Task<List<ScheduleTraining>> GetScheduleTrainingByYear(int year, string email);
    Task<ScheduleTraining> GetScheduleTrainingAsync(int scheduleTrainingId, string email);
    Task<ScheduleTraining> GetNextTraining(string email);
    Task<List<ScheduleTraining>> GetTrainingHistoryAsync(int count, string email);
    Task<List<ScheduleTraining>> GetScheduleTrainingsAdminAsync(string user, string name, bool finish, DateTime startDate, DateTime endDate);
  }
}
