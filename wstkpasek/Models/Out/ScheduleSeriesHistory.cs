using System.Collections.Generic;
using wstkpasek.Models.Schedule.Series;

namespace wstkpasek.Models.Out
{
    public class ScheduleSeriesHistory : ScheduleSeries
    {
        public List<ScheduleSeries> History { get; set; }
    }
}