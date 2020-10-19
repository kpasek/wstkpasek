using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.User
{
    [Table("user_weight")]
    public class Weight
    {
        [Key]
        public int WeightId { get; set; }
        public string UserEmail { get; set; }
        [Required]
        public double WeightIdKg { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
