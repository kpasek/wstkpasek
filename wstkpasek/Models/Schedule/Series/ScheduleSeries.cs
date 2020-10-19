using wstkp.Models.Schedule.Exercise;
using wstkp.Models.SeriesModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace wstkp.Models.Schedule.Series

{
  [Table("schedule_series")]
  public class ScheduleSeries
  {
    [Key]
    public int ScheduleSeriesId { set; get; }
    [Required]
    public string UserEmail { get; set; }
    public string Name { get; set; } = "";
    public int Order { get; set; } = 1;
    public int ScheduleExerciseId { get; set; }
    [ForeignKey("ScheduleExerciseId")]
    public ScheduleExercise ScheduleExercise { get; set; }
    public int Repeats { get; set; } = 0;
    public double Load { get; set; } = 0;
    public int Time { get; set; } = 0;
    public double Distance { get; set; } = 0;
    public int RestTime { get; set; } = 0;
    public int Intensity { get; set; } = 3;
    public bool Finish { get; set; } = false;
    
  }
}
