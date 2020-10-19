using wstkpasek.Models.Exercises;
using wstkpasek.Models.Schedule.Training;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.Schedule.Exercise
{
  [Table("schedule_exercise")]
  public class ScheduleExercise
  {
    public int ScheduleExerciseId { get; set; }
    [Required]
    public string UserEmail { get; set; }
    public int ExerciseId { get; set; }
    [ForeignKey("ExerciseId")]
    public Exercises.Exercise Exercise { get; set; }
    public int ScheduleTrainingId { get; set; }
    [ForeignKey("ScheduleTrainingId")]
    public ScheduleTraining ScheduleTraining { get; set; }
    public int Order { get; set; } = 1;
    public bool Started { get; set; } = false;
  }
}
