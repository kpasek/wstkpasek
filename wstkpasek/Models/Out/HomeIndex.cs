using System.Collections.Generic;
using wstkp.Models.Schedule.Series;
using wstkp.Models.Schedule.Training;
using wstkp.Models.User;

namespace wstkp.Models.Out
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