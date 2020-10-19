using System.Collections.Generic;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.TrainingModel;

namespace wstkpasek.Models.Out
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