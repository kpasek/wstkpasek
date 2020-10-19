using System.Collections.Generic;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.Resources;
using wstkpasek.Models.Schedule.Exercise;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.SeriesModel;
using wstkpasek.Models.TrainingModel;
using Microsoft.AspNetCore.Identity;

namespace wstkpasek.Models.Out
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