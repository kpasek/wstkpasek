using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wstkp.Models.TrainingModel;

namespace wstkp.Models.Exercises
{
    [Table("schedule_exercises")]
    public class ScheduleExercie
    {
        [Key]
        public int ScheduleExerciseId { set; get; }
        [Required]
        public string UserEmail { get; set; }
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
    }
}
