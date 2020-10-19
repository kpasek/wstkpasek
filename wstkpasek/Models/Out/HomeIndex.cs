using System.Collections.Generic;
using wstkpasek.Models.Schedule.Series;
using wstkpasek.Models.Schedule.Training;
using wstkpasek.Models.User;

namespace wstkpasek.Models.Out
{
    public class HomeIndex
    {
        public List<ScheduleTraining> ScheduleTrainings { get; set; }
        public List<ScheduleSeries> ScheduleSeries { get; set; }
        public ScheduleTraining ScheduleTraining { get; set; }
        public Const Const { get; }
        public Profile Profile { get; set; }

        public HomeIndex()
        {
            Const = new Const();
        }
    }
}