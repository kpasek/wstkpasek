﻿using wstkp.Models.Schedule.Exercise;
using wstkp.Models.Schedule.Series;
using wstkp.Models.Schedule.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkp.Models.Out
{
    public class StartTrainingIndex
    {
        public ScheduleTraining Training { get; set; }
        public List<ScheduleSeries> SeriesList { get; set; }
        public List<List<ScheduleSeries>> History { get; set; }
        public List<ScheduleSeries> SeriesToComplete { get; set; }
        public List<ScheduleSeries> SeriesCompleted { get; set; }
        public DateTime DateNow { get; set; } = DateTime.Now;
        public DateTime NextTrainingDate { get; set; }
        public string ReturnUrl { get; set; }
    }
}
