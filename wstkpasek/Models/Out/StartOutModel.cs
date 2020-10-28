using System;
using System.Collections.Generic;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;

namespace wstkpasek.Models.Out
{
    public class StartOutModel
    {
        public ScheduleTraining Training { get; set; }
        public List<ScheduleSeriesHistory> SeriesList { get; set; }
        public List<ScheduleSeriesHistory> SeriesToComplete { get; set; }
        public List<ScheduleSeriesHistory> SeriesCompleted { get; set; }
        public DateTime DateNow { get; set; } = DateTime.Now;
        public DateTime NextTrainingDate { get; set; }
    }
}