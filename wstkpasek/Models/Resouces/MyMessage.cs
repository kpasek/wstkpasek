using System.ComponentModel.DataAnnotations;

namespace wstkp.Models.Resources
{
  public class MyMessage
  {
    [Key]
    [StringLength(30)]
    public string MessegesCode { get; set; }
    [StringLength(6)]
    public string Language { get; set; }
    public string Message { get; set; }
    [StringLength(2)]
    public string Type { get; set; } = "I";
  }
}