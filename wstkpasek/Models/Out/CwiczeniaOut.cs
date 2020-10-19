using System.Collections.Generic;
using wstkpasek.Models.Exercises;
using wstkpasek.Models.SeriesModel;

namespace wstkpasek.Models.Out
{
    public class CwiczenieOut
    {
        public CwiczenieOut()
        {
            Cwiczenie = new List<CwiczeniaOut>();
            Rodzaje = new List<Type>();
        }

        public List<CwiczeniaOut> Cwiczenie { get; set; }
        public List<Type> Rodzaje { get; set; }
    }
    public class CwiczeniaOut
    {
        public CwiczeniaOut()
        {
            Cwiczenia = new List<CwiczenieSerie>();
        }

        public string Partia { get; set; }
        public List<CwiczenieSerie> Cwiczenia { get; set; }
    }
    public class CwiczenieSerie
    {
        public CwiczenieSerie()
        {
            Serie = new List<Series>();
        }

        public Exercise Exercise { get; set; }
        public List<Series> Serie { get; set; }

    }
}