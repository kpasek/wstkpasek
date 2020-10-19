using System.Collections.Generic;
using System.Threading.Tasks;

namespace wstkp.Models.SeriesModel
{
  public interface ISeriesRepository
  {
    //List<Series> GetSerieDlaCwiczenia(Cwiczenie cwiczenie, string Email);
    Series GetSeries(int seriesId, string email);
    List<Series> GetSeries(string email);
    int GetOrderForExercise(int exerciseId, int seriesId, string email);
    Task<List<Series>> GetSeriesForExerciseAsync(int exerciseId, string email);
    Task<Series> GetSeriesByOrderAsync(int exerciseId, int seriesOrder, string email);
    Task<int> GetMaxOrderInExerciseAsync(int exerciseId, string email);
    Task <List<Series>> GetSeriesAdminAsync(string User, string ExerciseName);
  }
}
