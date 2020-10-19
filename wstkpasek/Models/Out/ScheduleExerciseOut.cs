using wstkpasek.Models.Exercises;
using wstkpasek.Models.Schedule.Exercise;
using wstkpasek.Models.Schedule.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Type = wstkpasek.Models.Exercises.Type;

namespace wstkpasek.Models.Out
{
    public class ScheduleExerciseOut
    {
        public Schedule.Exercise.ScheduleExercise ScheduleExercise { get; set; }
        public List<ScheduleSeries> ScheduleSeries { get; set; }
        public List<Part> Parties { get; set; }
        public List<Type> Types { get; set; }
        public string Partia { get; set; }
        public int ScheduleTrainingId { get; set; }
    }
}
