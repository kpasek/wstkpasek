using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkp.Models.Exercises
{
    [Table("exercise_parts")]
    public class Part
    {
        [Key]
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public bool Public { get; set; } = true;

        public Part()
        {

        }
        public Part(string name)
        {
            Name = name;
        }
    }
}