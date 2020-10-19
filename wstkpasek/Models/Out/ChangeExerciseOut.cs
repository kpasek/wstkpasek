using wstkp.Models.Exercises;
using wstkp.Models.Schedule.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace wstkp.Models.Out
{
    public class ChangeExerciseOut
    {
        public List<Exercise> Exercises { get; set; }
        public Schedule.Exercise.ScheduleExercise ScheduleExercise { get; set; }
        public string Part { get; set; }
        public List<Part> Parties { get; set; }
        public string ReturnUrl { get; set; } = "/harmonogram";
    }
}
