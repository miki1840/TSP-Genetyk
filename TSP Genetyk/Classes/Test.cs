using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using TSP_Genetyk.Modifiers;

namespace TSP_Genetyk.Classes
{
    class Test
    {
       
        public static void run()
        {
            ArrayList scores=new ArrayList();
            for (int file = 0; file < 3; file++)
            {
                scores=new ArrayList();
                Specimen fileBest = new Specimen();
                fileBest.Cost = int.MaxValue;
                MatrixReader mr=new MatrixReader();
                mr.load(Variables.Paths[file]);
                Console.WriteLine(mr.name);

                for (int crossParam = 1; crossParam <= 2; crossParam++)
                {
                    for (int mutatuinParam = 1; mutatuinParam <= 2; mutatuinParam++)
                    {
                        for (int PopulationSizeIndex = 0; PopulationSizeIndex < 3; PopulationSizeIndex++)
                        {
                            for (int ttl = 40; ttl <= 120; ttl += 40)
                            {
                                int cost=0;
                                for (int i = 0; i < 3; i++)
                                {
                                    TSPGenetic tsp = new TSPGenetic(mr.Matrix, ttl,
                                        Variables.PopulationSize[PopulationSizeIndex], 0.01f, 0.8f, mutatuinParam,
                                        crossParam);
                                    tsp.run();
                                    cost += tsp.GetBestSpecimen().Cost;
                                    if (tsp.GetBestSpecimen().Cost < fileBest.Cost)
                                    {
                                        fileBest = tsp.GetBestSpecimen();
                                    }
                                }
                                scores.Add(new Score(mutatuinParam, crossParam, ttl, cost / 3, 0.01f, 0.8f, Variables.PopulationSize[PopulationSizeIndex]));
                                Console.WriteLine(((Score)scores[scores.Count - 1]).CrossingType + " " + ((Score)scores[scores.Count - 1]).MutationType + " " + ((Score)scores[scores.Count - 1]).PopulationSize);
                            }
                        }
                    }
                }
                
                using (StreamWriter streamWriter = File.CreateText(@"D:\tsp3\wyniki\pkt2" + mr.name + ".txt"))
                {
                    var writer = new CsvWriter(streamWriter);
                    writer.Configuration.RegisterClassMap<ScoreMap>();
                    writer.WriteRecords(scores);
                }
                
                //wybieranie najlepszego trymu mutacji i krzyżowania
                Score best=new Score(1,1,1,int.MaxValue,0f, 0f, 0);
                foreach (Score score in scores)
                {
                    if (score.Cost < best.Cost) best = (Score)score;
                }

                int bestmutator;
                int bestcrosser;
                if (best.CrossingType.Equals("PMX")) bestcrosser = 1;
                else bestcrosser = 2;
                if (best.MutationType.Equals("Invert")) bestmutator = 1;
                else bestmutator = 2;
                scores=new ArrayList();
                float[] mutators = {0.01f, 0.05f, 0.1f};
                float[] crossators = {0.5f, 0.7f, 0.9f};
                for (int mutator = 0; mutator < 3; mutator++)
                {
                    int cost = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        TSPGenetic tsp = new TSPGenetic(mr.Matrix,60,best.PopulationSize,mutators[mutator],0.8f,bestmutator,bestcrosser);
                        tsp.run();
                        cost += tsp.GetBestSpecimen().Cost;
                        if (tsp.GetBestSpecimen().Cost < fileBest.Cost)
                        {
                            fileBest = tsp.GetBestSpecimen();
                        }
                        
                    }

                    scores.Add(new Score(bestmutator, bestcrosser, 60, cost / 3, mutators[mutator], 0.8f,
                        best.PopulationSize));
                    Console.WriteLine("Mutation " + mutator);
                }
                using (StreamWriter streamWriter = File.CreateText(@"D:\tsp3\wyniki\pkt3" + mr.name + ".txt"))
                {
                    var writer = new CsvWriter(streamWriter);
                    writer.Configuration.RegisterClassMap<ScoreMap>();
                    writer.WriteRecords(scores);
                }
                scores=new ArrayList();
                for (int crossator = 0; crossator < 3; crossator++)
                {
                    int cost = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        TSPGenetic tsp=new TSPGenetic(mr.Matrix,60,best.PopulationSize,0.01f,crossators[crossator],bestmutator,bestcrosser);
                        tsp.run();
                        cost += tsp.GetBestSpecimen().Cost;
                        if (tsp.GetBestSpecimen().Cost < fileBest.Cost)
                        {
                            fileBest = tsp.GetBestSpecimen();
                        }
                        
                    }

                    scores.Add(new Score(bestmutator, bestcrosser, 60, cost / 3, 0.01f, crossators[crossator],
                        best.PopulationSize));
                    Console.WriteLine("Crossing " + crossator);
                }
                using (StreamWriter streamWriter = File.CreateText(@"D:\tsp3\wyniki\pkt4" + mr.name + ".txt"))
                {
                    var writer = new CsvWriter(streamWriter);
                    writer.Configuration.RegisterClassMap<ScoreMap>();
                    writer.WriteRecords(scores);
                }

                using (StreamWriter streamWriter = File.CreateText(@"D:\tsp3\wyniki\pkt5" + mr.name + ".txt"))
                {
                    streamWriter.WriteLine("Cost: "+fileBest.Cost+"\n");
                    foreach (var i in fileBest.Path)
                    {
                       streamWriter.Write(i+"=>");
                    }
                    streamWriter.Write("\b");
                    streamWriter.Write("\b");
                }
            }
        }
    }
}
