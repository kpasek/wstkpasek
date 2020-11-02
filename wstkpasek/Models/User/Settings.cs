using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.User
{
    [Table("user_settings")]
    public class Settings
    {
        [Key]
        public int SettingsId { get; set; }
        public string UserEmail { get; set; }
        public int ColorTheme { get; set; } = 0;
        public int TrainingHour { get; set; } = 18;
        public int TrainingMinute { get; set; } = 0;
        public int TrainingDayInterval { get; set; } = 7;
        [StringLength(6)]
        public string Language { get; set; } = "pl_PL";
    }
}
