using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.Exercises
{
    [Table("exercise_types")]
    public class Type
    {
        [Key]
        public string TypeName { get; set; }
        public string UserEmail { get; set; }
        public bool Public { get; set; } = true;


        public Type(string typeName)
        {
            TypeName = typeName;
        }
        public Type()
        {
        }
    }
}