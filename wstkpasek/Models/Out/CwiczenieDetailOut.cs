using System.Collections.Generic;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.SeriesModel;

namespace wstkpasek.Models.Out
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