using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.Out
{
    public class ProgressOut
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public double LoadAv { get; set; }
        public double RepeatAv { get; set; }
        public double DistanseAv { get; set; }
        public double TimeAv { get; set; }
        public double RestTimeAv { get; set; }
    }
}
