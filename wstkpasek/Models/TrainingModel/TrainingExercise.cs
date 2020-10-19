using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wstkpasek.Models.Exercises;

namespace wstkpasek.Models.TrainingModel
{
  [Table("trainings_exercises")]
  public class TrainingExercise
  {
    [Key]
    public int TrainingExerciseId { get; set; }
    [Required]
    public string UserEmail { get; set; }
    public int TrainingId { get; set; }
    [ForeignKey("TrainingId")]
    public Training Training { get; set; }
    public int ExerciseId { get; set; }
    [ForeignKey("ExerciseId")]
    public Exercise Exercise { get; set; }
    public int Order { get; set; } = 1;
  }
}