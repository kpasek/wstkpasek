using wstkp.Models.Exercises;
using wstkp.Models.Schedule.Exercise;
using wstkp.Models.Schedule.Series;
using wstkp.Models.Schedule.Training;
using System.Collections.Generic;

namespace wstkp.Models.Out
{
  public class ScheduleTrainingDetailOut
  {
    public ScheduleTraining ScheduleTraining { get; set; }
    public List<Schedule.Exercise.ScheduleExercise> ScheduleExercises { get; set; }
    public List<Exercise> Exercises { get; set; }
    public List<ScheduleTraining> TrainingHistory { get; set; }
    public List<Part> Parties { get; set; }
    public string Part { get; set; }
    public List<Type> Types { get; set; }
    public List<ScheduleSeries> SeriesHistory { get; set; }
    public string ReturnUrl { get; set; } = "/harmonogram";
  }
}
