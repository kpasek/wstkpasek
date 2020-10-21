using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using wstkpasek.Models.Exercises;

namespace wstkpasek.Models.SeriesModel
{
    [Table("series")]
    public class Series
    {
        [Key]
        public int SeriesId { get; set; }
        public string UserEmail { get; set; }

        public int ExerciseId { get; set; } = 0;
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }
        public string Name { get; set; }
        public int Repeats { get; set; } = 0;
        public double Load { get; set; } = 0;
        public int Time { get; set; } = 0;
        public double Distance { get; set; } = 0;
        public int RestTime { get; set; } = 0;
        public int Order { get; set; } = 1;
    }
}
