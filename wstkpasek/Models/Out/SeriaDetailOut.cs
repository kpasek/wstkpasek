using wstkpasek.Models.SeriesModel;

namespace wstkpasek.Models.Out
{
    public class SeriaDetailOut
    {
        public Series Series { get; set; }
        public string ReturnUrl { get; set; } = "/trening";
    }
}