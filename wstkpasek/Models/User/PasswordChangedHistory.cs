using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wstkpasek.Models.User
{
  [Table("log_password_change")]
  public class PasswordChangedHistory
  {
    [Key]
    public int PasswordChangedHistoryId { get; set; }
    public string Email { get; set; }
    public string OldHashedPassord { get; set; }
    public DateTime TimeStamp { get; set; }
  }
}
