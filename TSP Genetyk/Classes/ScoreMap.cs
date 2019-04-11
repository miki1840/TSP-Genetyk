using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;

namespace TSP_Genetyk.Classes
{
    public sealed class ScoreMap : ClassMap<Score>
    {
        public ScoreMap()
        {
            Map(m => m.CrossingType).Name("Metoda krzyzowania");
            Map(m => m.MutationType).Name("Metoda mutacji");
            Map(m => m.TTL).Name("Czas dzialania");
            Map(m => m.Cost).Name("Koszt");
            Map(m => m.MutationFactor).Name("Wspolczynnik mutacji");
            Map(m => m.CrossingFactor).Name("Wspólczynnik krzyzowania");
            Map(m => m.PopulationSize).Name("Rozmiar populacji");
        }
    }
}
