using wstkp.Models.SeriesModel;

namespace wstkp.Models.Out
{
    public class SeriaDetailOut
    {
        public Series Series { get; set; }
        public string ReturnUrl { get; set; } = "/trening";
    }
}