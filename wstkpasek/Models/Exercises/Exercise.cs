using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.Exercises
{
  [Table("exercises")]
  public class Exercise
  {
    [Key]
    public int ExerciseId { set; get; }
    public string UserEmail { get; set; }
    public bool Public { get; set; } = false;
    public string Name { get; set; }
    public string PartId { get; set; }
    [ForeignKey("PartId")]
    public Part Part { get; set; }
    public string TypeId { get; set; }
    [ForeignKey("TypeId")]
    public Type Type { get; set; }
    public string Description { get; set; }
  }
}
