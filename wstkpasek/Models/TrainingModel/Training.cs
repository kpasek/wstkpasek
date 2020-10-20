using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.TrainingModel
{
    [Table("trainings")]
    public class Training
    {
        [Key]
        public int TrainingId { get; set; }
        public string UserEmail { get; set; }
        // public DateTime DataUtworzenia { get; set; } = DateTime.Now;
        // public DateTime Data { get; set; }
        public string Name { get; set; }
        public int ExerciseNumber { get; set; } = 2;
        public bool Public { get; set; } = false;
    }
}
