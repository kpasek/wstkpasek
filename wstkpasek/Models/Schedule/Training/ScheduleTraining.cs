using wstkpasek.Models.TrainingModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.Schedule.Training
{
    [Table("schedule_trainings")]
    public class ScheduleTraining
    {
        [Key]
        public int ScheduleTrainingId { get; set; }
        public string UserEmail { get; set; }
        [Required]
        public DateTime TrainingDate { get; set; } = DateTime.Now;
        public DateTime TrainingFinishDate { get; set; }
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public int ExerciseNumber { get; set; } = 2;
        public bool Finish { get; set; } = false;
    }
}
