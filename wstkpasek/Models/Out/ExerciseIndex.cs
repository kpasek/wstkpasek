using System.Collections.Generic;
using wstkp.Models.Exercises;

namespace wstkp.Models.Out
{
    public class ExerciseIndex
    {
        public List<Exercise> Exercises { get; set; }
        public string returnUrl { get; set; }
    }
}