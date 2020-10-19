using System.Collections.Generic;
using wstkp.Models.Exercises;
using wstkp.Models.Resources;
using wstkp.Models.Schedule.Exercise;
using wstkp.Models.Schedule.Series;
using wstkp.Models.Schedule.Training;
using wstkp.Models.SeriesModel;
using wstkp.Models.TrainingModel;
using Microsoft.AspNetCore.Identity;

namespace wstkp.Models.Out
{
  public class AdminOut
  {
    public List<IdentityUser<int>> Users { get; set; }
    public List<MyMessage> Messeges { get; set; }
    public List<Training> TrainingList { get; set; }
    public List<Series> SeriesList { get; set; }
    public List<Exercise> ExerciseList { get; set; }
    public List<Part> PartList { get; set; }
    public List<Exercises.Type> TypeList { get; set; }
    public List<ScheduleTraining> ScheduleTrainigList { get; set; }
    public List<ScheduleExercise> ScheduleExerciseList { get; set; }
    public List<ScheduleSeries> ScheduleSeriesList { get; set; }
  }
}