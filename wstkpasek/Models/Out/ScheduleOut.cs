using wstkpasek.Models.Exercises;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.TrainingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.Out
{
  public class ScheduleOut
  {
    public List<ScheduleTraining> Trainings { get; set; }
    public List<Training> TrainingList { get; set; }
    public DateTime DateNow { get; set; } = DateTime.Now;
    public List<Part> Parties { get; set; }
    public List<Exercise> Exercises { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string Part { get; set; }
    public Exercise Exercise { get; set; }
    public string Message { get; set; }
    public Const Const { get; set; } = new Const();
    public List<ScheduleSeries> ScheduleSeriesHistory { get; set; }
  }
}
