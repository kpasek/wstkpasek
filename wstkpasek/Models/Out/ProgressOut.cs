using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wstkpasek.Models.Out
{
    public class ProgressOut
    {
        public string Part { get; set; }
        public List<string> Dates { get; set; }
        public List<double> Loads { get; set; }
        public List<double> Repeats { get; set; }
        public List<double> Distanses { get; set; }
        public List<double> Times { get; set; }
        public List<double> RestTimes { get; set; }
    }
}
