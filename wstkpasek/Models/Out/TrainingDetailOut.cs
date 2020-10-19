using System.Collections.Generic;
using wstkp.Models.Exercises;
using wstkp.Models.Schedule.Series;
using wstkp.Models.Schedule.Training;
using wstkp.Models.TrainingModel;

namespace wstkp.Models.Out
{
  public class TrainingDetailOut
  {
    public Training Training { get; set; }
    public List<Exercise> Exercises { get; set; }
    public List<Exercise> TrainingExercises { get; set; }
    public List<Part> Partie { get; set; }
    public List<Type> Types { get; set; }
    public string Parties { get; set; }
    public string ReturnUrl { get; set; } = "/trening";
    public List<ScheduleSeries> SeriesHistory { get; set; }
    public List<string> Args { get; set; }

  }
}