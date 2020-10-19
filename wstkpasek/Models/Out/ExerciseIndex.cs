using System.Collections.Generic;
using wstkpasek.Models.Exercises;

namespace wstkpasek.Models.Out
{
    public class ExerciseIndex
    {
        public List<Exercise> Exercises { get; set; }
        public string returnUrl { get; set; }
    }
}