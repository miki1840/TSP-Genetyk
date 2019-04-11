using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TSP_Genetyk.Classes
{
    class TSPGenetic
    {
        //intput variables 
        private long _ttl;
        private int _mutationFactor;
        private int _crossingFactor;
        private int _crossingMethod;
        private int _mutationMethod;
        private int _populationSize;
        private int[][] _matrix;
        private int _size;
        //class variables
        private Specimen _bestSpecimen;
        private Random _random;
        private int _scrumbleIterations;
        

        public TSPGenetic(int[][] matrix, long ttl, int populationSize, float mutationFactor, float crossingFactor,
            int mutationMethod, int crossingMethod)
        {
            _matrix = matrix;
            _ttl = ttl*1000;
            _populationSize = populationSize;
            _mutationFactor = 100-(int)(mutationFactor*100);
            _crossingFactor = 100-(int)(crossingFactor*100);
            _mutationMethod = mutationMethod;
            _crossingMethod = crossingMethod;
            _size = matrix.Length;
            _random=new Random();
            _scrumbleIterations = (int)Math.Sqrt(_size);
            _bestSpecimen=new Specimen();
            _bestSpecimen.Cost = int.MaxValue;
        }
        private int rateRute(int[] tab)
        {
            int score = 0;
            for (int i = 1; i < tab.Length; i++)
            {
                score += _matrix[tab[i - 1]][tab[i]];
            }
            return score;
        }
        private int[] randomPath()
        {
            
            int[] newsolution = new int[_size+1];
            List<int> vortexes = new List<int>();
            for (int i = 1; i < newsolution.Length - 1; i++) vortexes.Add(i);
            Random random = new Random();
            newsolution[0] = 0;
            newsolution[newsolution.Length - 1] = 0;
            for (int i = 1; i < newsolution.Length - 1; i++)
            {
                int a = random.Next(vortexes.Count);
                newsolution[i] = vortexes[a];
                vortexes.RemoveAt(a);
            }
            newsolution[newsolution.Length - 1] = 0;
            return newsolution;
        }

        private Specimen InvertMutation(int[] parrent)
        {
            int[] offspring = new int[parrent.Length];
            int a=0, b=0;
            do
            {
                a = _random.Next(parrent.Length-2)+1;
                b = _random.Next(parrent.Length-2)+1;
                if (a > b)
                {
                    int t = a;
                    a = b;
                    b = t;
                }
            } while (a==b);
            
            for (int i = 0; i < a; i++)
            {
                offspring[i] = parrent[i];
            }

            for (int i = 0; i < b-a+1; i++)
            {
                offspring[a + i] = parrent[b - i];
            }

            for (int i = b + 1; i < parrent.Length; i++)
            {
                offspring[i] = parrent[i];
            }

            Specimen toReturn = new Specimen();
            toReturn.Path = offspring;
            toReturn.Cost = rateRute(offspring);
            return toReturn;
        }

        private Specimen ScrumbleMutation(int[] parrent1)
        {
            int[] offspring = (int[])parrent1.Clone();
            for (int i = 0; i < _scrumbleIterations; i++)
            {
                int a, b, t;
                do
                {
                    a = _random.Next(_size - 2) + 1;
                    b = _random.Next(_size - 2) + 1;
                } while (a == b);
               
                t = offspring[a];
                offspring[a] = offspring[b];
                offspring[b] = t;
            }
            Specimen toReturn=new Specimen();
            toReturn.Path = offspring;
            toReturn.Cost = rateRute(offspring);
            return toReturn;
        }
        private void PMXCrossing(int[] parrent1, int[] parrent2, out Specimen offspring1Out, out Specimen offspring2Out)
        {
            int[] offspring1 = new int[parrent1.Length];
            int[] offspring2 = new int[parrent1.Length];
            ArrayList genom1 = new ArrayList();
            ArrayList genom2 = new ArrayList();
            int a = 0, b = 0;
            //losowanie 2 różnych punktów do krzyżownia 
            do
            {
                a = _random.Next(parrent1.Length-2)+1;
                b = _random.Next(parrent1.Length-2)+1;
                if (a > b)
                {
                    int t = a;
                    a = b;
                    b = t;
                }
            } while (a == b);
           
            for (int i = a; i <= b; i++)
            {
                offspring1[i] = parrent2[i];
                offspring2[i] = parrent1[i];
                genom2.Add(parrent1[i]);
                genom1.Add(parrent2[i]);
            }

            for (int i = 0; i < a; i++)
            {
                if (!genom1.Contains(parrent1[i])) offspring1[i] = parrent1[i];
                if (!genom2.Contains(parrent2[i])) offspring2[i] = parrent2[i];
            }
            for (int i = b+1; i < parrent1.Length; i++)
            {
                if (!genom1.Contains(parrent1[i])) offspring1[i] = parrent1[i];
                if (!genom2.Contains(parrent2[i])) offspring2[i] = parrent2[i];
            }

            for (int i = 1; i < a; i++)
            {
                if (offspring1[i] == 0)
                {
                    offspring1[i] = (int)genom2[genom1.IndexOf(parrent1[i])];
                    while (genom1.Contains(offspring1[i]))
                    {
                        offspring1[i] = (int)genom2[genom1.IndexOf(offspring1[i])];
                    }
                }

                if (offspring2[i] == 0)
                {
                    offspring2[i] = (int)genom1[genom2.IndexOf(parrent2[i])];
                    while (genom2.Contains(offspring2[i]))
                    {
                        offspring2[i] = (int)genom1[genom2.IndexOf(offspring2[i])];
                    }
                }
            }
            for (int i = b+1; i < parrent1.Length-1; i++)
            {
                if (offspring1[i] == 0)
                {
                    offspring1[i] = (int)genom2[genom1.IndexOf(parrent1[i])];
                    while (genom1.Contains(offspring1[i]))
                    {
                        offspring1[i] = (int)genom2[genom1.IndexOf(offspring1[i])];
                    }
                }

                if (offspring2[i] == 0)
                {
                    offspring2[i] = (int)genom1[genom2.IndexOf(parrent2[i])];
                    while (genom2.Contains(offspring2[i]))
                    {
                        offspring2[i] = (int)genom1[genom2.IndexOf(offspring2[i])];
                    }
                }
            }
            offspring1Out=new Specimen();
            offspring2Out=new Specimen();
            offspring1Out.Path = offspring1;
            offspring2Out.Path = offspring2;
            offspring1Out.Cost = rateRute(offspring1);
            offspring2Out.Cost = rateRute(offspring2);


        }
        private void OXCrossing(int[] parrent1, int[] parrent2, out Specimen offspring1Out, out Specimen offspring2Out)
        {
            int[] offspring1 = new int[parrent1.Length];
            int[] offspring2 = new int[parrent1.Length];
            ArrayList genom1 = new ArrayList();
            ArrayList genom2 = new ArrayList();
            int a = 0, b = 0;
            //losowanie 2 różnych punktów do krzyżownia 
            do
            {
                a = _random.Next(parrent1.Length - 2) + 1;
                b = _random.Next(parrent1.Length - 2) + 1;
                if (a > b)
                {
                    int t = a;
                    a = b;
                    b = t;
                }
            } while (a == b);
            
            for (int i = a; i <= b; i++)
            {
                offspring1[i] = parrent2[i];
                offspring2[i] = parrent1[i];
                genom1.Add(parrent2[i]);
                genom2.Add(parrent1[i]);
            }
            //iterrators
            int offspringit = b + 1, parrentit = b + 1;
            //towrzenie potomka 1
            while (offspringit < _size)
            {
                if (!genom1.Contains(parrent1[parrentit]))
                {
                    offspring1[offspringit] = parrent1[parrentit];
                    offspringit++;
                    parrentit++;
                }
                else
                {
                    parrentit++;
                }

                if (parrentit == _size) parrentit = 1;
            }
            if (parrentit == _size ) parrentit = 1;
            offspringit = 1;
            while (offspringit < a)
            {
                if (!genom1.Contains(parrent1[parrentit]))
                {
                    offspring1[offspringit] = parrent1[parrentit];
                    offspringit++;
                    parrentit++;
                }
                else
                {
                    parrentit++;
                }
            }
            //tworzenie potomka 2
            offspringit = b + 1;
            parrentit = b + 1;
            while (offspringit < _size )
            {
                if (!genom2.Contains(parrent2[parrentit]))
                {
                    offspring2[offspringit] = parrent2[parrentit];
                    offspringit++;
                    parrentit++;
                }
                else
                {
                    parrentit++;
                }

                if (parrentit == _size ) parrentit = 1;
            }
            if (parrentit == _size ) parrentit = 1;
            offspringit = 1;
            while (offspringit < a)
            {
                if (!genom2.Contains(parrent2[parrentit]))
                {
                    offspring2[offspringit] = parrent2[parrentit];
                    offspringit++;
                    parrentit++;
                }
                else
                {
                    parrentit++;
                }
            }
            offspring1Out = new Specimen();
            offspring2Out = new Specimen();
            offspring1Out.Path = offspring1;
            offspring2Out.Path = offspring2;
            offspring1Out.Cost = rateRute(offspring1);
            offspring2Out.Cost = rateRute(offspring2);
        }

        private Specimen[] Crossing(Specimen[] population)
        {
            Specimen[] newPopulation = new Specimen[_populationSize];
            int survivors = 0; //ilość osobników która przejdzie do następnego pokolenia, poprzez brak krzyżowania 
            int[] ruller = new int[_populationSize]; //tablica przechowywująca szanse na wylosowanie
            //Uzupełnienie tej tablicy w sposób, który faworyzuje lepsze osobniki 
            ruller[0] = population[population.Length - 1].Cost - population[0].Cost;
            for (int i=1;i<_populationSize;i++) ruller[i]=ruller[i-1]+ population[population.Length - 1].Cost - population[i].Cost; 
            
            for (int i = 0; i < population.Length / 2; i++)
            {
                //losowanie pierwszego osobnika do krzyżowania i wyszukanie jego indeksu 
                int random1, random2;
                random1 = _random.Next(ruller[ruller.Length - 1]);
                if (random1 < ruller[0]) random1 = 0;
                else
                {

                    for (int j = 1; j < ruller.Length; j++)
                    {
                        if (random1 < ruller[j])
                        {
                            random1 = j;
                            break;
                        }
                    }
                }
                //losowanie drugiego innego osobnika 
                
                    random2 = _random.Next(ruller[ruller.Length - 1]);
                    if (random2 < ruller[0]) random2 = 0;
                    else
                    {

                        for (int j = 1; j < ruller.Length; j++)
                        {
                            if (random2 < ruller[j])
                            {
                                random2 = j;
                                break;
                            }
                        }
                    }
                if (random2 == random1) random2++;
                if (random2 >= population.Length) random2 = 0;

                

                if (_random.Next(101) > _crossingFactor) //losowanie tego czy osobniki się skrzyżują lub nie
                {
                    switch (_crossingMethod)
                    {
                        case 1:
                            PMXCrossing(population[random1].Path, population[random2].Path, out newPopulation[2 * i],
                                out newPopulation[2 * i + 1]);
                            break;
                        case 2:
                            OXCrossing(population[random1].Path, population[random2].Path, out newPopulation[2 * i], out newPopulation[2 * i + 1]);
                            break;
                    }
                }
                else
                {
                    survivors += 2;
                }

            }

            
            for (int i=0;i<newPopulation.Length;i++)
            {
                if (newPopulation[i] == null)
                {
                    newPopulation[i] = population[_random.Next(population.Length)];
                }
            }

            return newPopulation;
        }

        private Specimen[] Mutate(Specimen[] population)
        {
            Specimen[] newPopulation = new Specimen[_populationSize];
            int i = 0;
            foreach (var t in population)
            {
                if (_random.Next(101) > _mutationFactor)
                {
                    switch (_mutationMethod)
                    {
                        case 1:
                            newPopulation[i] = InvertMutation(t.Path);
                            break;
                        case 2:
                            newPopulation[i] = ScrumbleMutation(t.Path);
                            break;
                    }
                }
                else
                {
                    newPopulation[i] = t;
                }
                i++;
            }
            return newPopulation;
        }

        private Specimen[] RateAndSort(Specimen[] population)
        {
            population = population.OrderBy(x => x.Cost).ToArray<Specimen>(); //tu może będzie trzeba porawić
           
            if (population[0].Cost < _bestSpecimen.Cost)
            {
                _bestSpecimen = population[0];
            }
            return population;
        }
        

        public void run()
        {
            DateTime end = DateTime.Now.AddMilliseconds(_ttl);
            Specimen[] population= new Specimen[_populationSize];
            //population[0] = GenerateGreedy();
            for (int i = 0; i < _populationSize; i++)
            {
                population[i]=new Specimen();
                population[i].Path = randomPath();
                population[i].Cost = rateRute(population[i].Path);

            }

            population=RateAndSort(population);
            while (end.CompareTo(DateTime.Now)>0)// jak nie działa to tu inny znak 
            {
                population = Crossing(population);
                population = Mutate(population);
                population=RateAndSort(population);
            }
        }

        public Specimen GetBestSpecimen()
        {
            return _bestSpecimen;
        }

        public void printPath(int[] path)
        {
            foreach (int i in path)
            {
                Console.Write(i+"=>");//Console.Write(String.Format("{0,4}",i));
            }

            Console.Write("\b \b");
            Console.Write("\b \b");
            Console.WriteLine();
        }

        private Specimen GenerateGreedy()
        {
            Specimen toReturn = new Specimen();
            int[] path = new int[_matrix.Length+1];
            int cost = 0;
            HashSet<int> vortexes = new HashSet<int>();
            path[0] = 0;
            for (int i = 1; i < _matrix.Length; i++) vortexes.Add(i);
            int prevVortex = 0;
            for (int i = 1; i < _matrix.Length; i++)
            {
                int bestS = int.MaxValue;
                int bestV = 0;
                foreach (int x in vortexes)
                {
                    if (_matrix[prevVortex][x] < bestS)
                    {
                        bestS = _matrix[prevVortex][x];
                        bestV = x;
                    }
                }
                path[i] = bestV;
                cost += _matrix[prevVortex][bestV];
                prevVortex = bestV;
                vortexes.Remove(bestV);
            }
            path[path.Length - 1] = 0;
            cost += _matrix[prevVortex][0];
            toReturn.Path = path;
            toReturn.Cost = cost;
            return toReturn;
        }

    }
}
