using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.TrainingModel
{
    [Table("trainings")]
    public class Training
    {
        [Key]
        public int TrainingId { get; set; }
        public string UserEmail { get; set; } = "TEST";
        public string Name { get; set; } = "TEST";
        public int ExerciseNumber { get; set; } = 2;
        public bool Public { get; set; } = false;
    }
}
