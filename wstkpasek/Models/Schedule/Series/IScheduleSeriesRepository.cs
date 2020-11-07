using wstkpasek.Models.Schedule.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.Schedule.Series
{
    public interface IScheduleSeriesRepository
    {
        Task<ScheduleSeries> GetScheduleSeriesAsync(int seriesId, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesForScheduleExerciseAsync(int scheduleExerciseId, string email);
        Task<int> GetMaxOrderForScheduleExerciseAsync(int scheduleExerciseId, string email);
        Task<ScheduleSeries> GetScheduleSeriesByOrderAsync(int scheduleExerciseId, int order, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesForScheduleTrainingAsync(int scheduleTrainingId, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesInLastTrainingAsync(ScheduleSeries series, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesByYearAsync(int year, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesForLastTrainingsAsync(int count, int trainingId, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesForLastTrainingsAsync(int count, int trainingId, DateTime date, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesForExerciseAsync(int exerciseId, int count, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesByYearMonthAsync(int year, int month, string email);
        Task<List<ScheduleSeries>> GetScheduleSeriesAdminAsync(string user, string name, string part, bool finish, DateTime startDate, DateTime endDate);
        Task<List<ScheduleSeries>> GetSeriesByDatesAsync(string email, DateTime? datefrom, DateTime? dateTo);
        Task<List<ScheduleSeries>> GetSeriesByDatesAsync(string email, string part, DateTime? datefrom, DateTime? dateTo);
        Task<List<ScheduleSeries>> GetSeriesByDatesAsync(string email, int exerciseId, DateTime? datefrom, DateTime? dateTo);
    }
}
