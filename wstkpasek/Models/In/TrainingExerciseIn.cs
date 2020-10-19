namespace wstkpasek.Models.InModels
{
    public class TrainingExerciseIn
    {
        public int TrainingId { get; set; }
        public int ExerciseId { set; get; }
        public string Name { get; set; }
        public string Part { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

    }
}