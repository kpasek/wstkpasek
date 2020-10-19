namespace wstkpasek.Models.InModels
{
    public class ExerciseSeriesIn
    {
        public int ExerciseId { set; get; }
        public int SeriesId { get; set; }
        public int Repeats { get; set; } = 0;
        public double Load { get; set; } = 0;
        public int Time { get; set; } = 0;
        public double Distance { get; set; } = 0;
        public int RestTime { get; set; } = 0;
    }
}