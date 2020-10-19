using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.In
{
  public class FinishTrainingIn
  {
    public int ScheduleTrainingId { get; set; }
    public bool CreateNextTraining { get; set; } = false;
    public bool UpdateSeries { get; set; } = false;
    public DateTime NextTrainingDate { get; set; }
    public DateTime FinishDate { get; set; } = DateTime.Now;
  }
}
