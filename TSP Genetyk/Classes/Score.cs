using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TSP_Genetyk.Classes
{
    public class Score
    {
        public string MutationType { get; set; }
        public string CrossingType { get; set; }
        public int TTL { get; set; }
        public int Cost { get; set; }
        public float CrossingFactor { get; set; }
        public float MutationFactor { get; set; }
        public int PopulationSize { get; set; }
        public Score(int mutationType, int crossingType, int ttl, int cost, float mutationFactor, float crossingFactor, int populationSize)
        {
            TTL = ttl;
            Cost = cost;
            if (mutationType == 1) MutationType = "Invert";
            else MutationType = "Scrumble";
            if (crossingType == 1) CrossingType = "PMX";
            else CrossingType = "OX";
            MutationFactor = mutationFactor;
            CrossingFactor = crossingFactor;
            PopulationSize = populationSize;
        }
    }
}
