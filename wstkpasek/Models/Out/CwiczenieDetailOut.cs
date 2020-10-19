using System.Collections.Generic;
using wstkp.Models.Exercises;
using wstkp.Models.Schedule.Series;
using wstkp.Models.SeriesModel;

namespace wstkp.Models.Out
{
  public class ExerciseOutDetailModel
  {
    public Exercise Exercise { get; set; }
    public List<Series> SeriesList { get; set; }
    public List<Series> SeriesInExerciseList { get; set; }
    public List<Part> Parties { get; set; }
    public List<Type> Types { get; set; }
    public string returnUrl { get; set; }
    public List<ScheduleSeries> ScheduleSeries { get; set; }
  }
}