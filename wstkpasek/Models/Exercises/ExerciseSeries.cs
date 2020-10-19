// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using kptr.Models.SeriesModel;
//
// namespace kptr.Models.Exercises
// {
//   [Table("exercise_series")]
//   public class ExerciseSeries
//   {
//     [Key]
//     public int ExerciseSeriesId { get; set; }
//     public string UserEmail { get; set; }
//     public int ExerciseId { get; set; }
//     [ForeignKey("ExerciseId")]
//     public Exercise Exercise { get; set; }
//     public int SeriesId { get; set; }
//     [ForeignKey("SeriesId")]
//     public Series Series { get; set; }
//     public int Order { get; set; } = 1;
//   }
// }