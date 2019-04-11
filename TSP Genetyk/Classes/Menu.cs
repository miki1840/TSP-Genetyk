using System;
using System.Collections.Generic;
using System.Text;
using TSP_Genetyk.Modifiers;

namespace TSP_Genetyk.Classes
{
    class Menu
    {
        public static void menu()
        {
            
            bool exist = true;
            int chose, chose1;
            int populationSize=300;
            int temp;//zmienna tymczasowa do wczytywania intów 
            float tempFloat;
            float mutationFactor=0.1f;
            float crossingFactor=0.8f;
            int crossingMethod=1;
            int mutationMethod=1;
            MatrixReader matrixReader= new MatrixReader();
            int ttl = 30; //domyślne kryterium stopu
            TSPGenetic tsp;
            
            bool loaded = false;
            while (exist)
            {
                System.Console.WriteLine(
                    "1. Wczytaj dane\n2. Kryterium stopu\n3. Rozmiar populacji początkowej\n4. Współczynnik mutacji\n5. Współczynnik krzyżowania\n6. Wybór metody krzyżowania\n7. Wybór metody mutacji\n8. Uruchom algorytm\n9. Testy\n0 Zakończ program");         
                chose = int.Parse(System.Console.ReadLine());
                switch (chose)
                {
                    case 1:
                        string path = Variables.Paths[0];
                        System.Console.WriteLine("1. ftv47\n2. ftv170\n3. rbg403\n4.Inny plik");
                        if (!int.TryParse(System.Console.ReadLine(), out chose1))
                            System.Console.WriteLine("To nie jest liczba");
                        else
                        {
                            bool exist1 = true;
                            while (exist1)
                            {
                                switch (chose1)
                                {
                                    case 1:
                                        path = Variables.Paths[0];
                                        exist1 = false;
                                        break;
                                    case 2:
                                        path = Variables.Paths[1];
                                        exist1 = false;
                                        break;
                                    case 3:
                                        path = Variables.Paths[2];
                                        exist1 = false;
                                        break;
                                    case 4:
                                        Console.WriteLine("Podaj scieżkę: ");
                                        path = Console.ReadLine();
                                        if (!System.IO.File.Exists(path)) exist1 = false;
                                        else Console.WriteLine("Plik nie istnieje");
                                        break;
                                }
                            }
                            matrixReader.load(path);
                            matrixReader.print();
                            loaded = true;
                        }
                        break;
                    case 2:
                        System.Console.WriteLine("Podaj wartość stopu w sekundach: ");
                        if(!int.TryParse(System.Console.ReadLine(), out temp)) System.Console.WriteLine("To nie jest liczba");
                        else
                        {
                            if (temp > 0 && temp < 10000) ttl = temp;
                            else System.Console.WriteLine("Liczba spoza zakresu");
                        }
                        break;
                    case 3:
                        System.Console.WriteLine("Podaj wartość stopu w sekundach: ");
                        if (!int.TryParse(System.Console.ReadLine(), out temp))
                            System.Console.WriteLine("To nie jest liczba");
                        else populationSize = temp;
                            break;
                    case 4:
                        System.Console.WriteLine("Podaj wartość współczynnika mutacji: ");
                        if (!float.TryParse(System.Console.ReadLine(), out tempFloat))
                            System.Console.WriteLine("To nie jest liczba");
                        else mutationFactor = tempFloat;
                        break;
                    case 5:
                        System.Console.WriteLine("Podaj wartość współczynnika krzyżowania: ");
                        if (!float.TryParse(System.Console.ReadLine(), out tempFloat))
                            System.Console.WriteLine("To nie jest liczba");
                        else crossingFactor = tempFloat;
                        break;
                    case 6:
                        System.Console.WriteLine("1. PMX\n2. OX ");
                        if (!int.TryParse(System.Console.ReadLine(), out temp)) System.Console.WriteLine("To nie jest liczba");
                        else
                        {
                            if (temp > 0 && temp < 3) crossingMethod = temp;
                            else System.Console.WriteLine("Liczba spoza zakresu");
                        }
                        break;
                    case 7:
                        System.Console.WriteLine("1. Invert 1\n2. Scrumble");
                        if (!int.TryParse(System.Console.ReadLine(), out temp)) System.Console.WriteLine("To nie jest liczba");
                        else
                        {
                            if (temp > 0 && temp < 3) mutationMethod = temp;
                            else System.Console.WriteLine("Liczba spoza zakresu");
                        }
                        break;
                    case 8:
                        if (loaded)
                        {
                            tsp = new TSPGenetic(matrixReader.Matrix, ttl, populationSize, mutationFactor,
                                crossingFactor, mutationMethod, crossingMethod);
                            tsp.run();
                            Console.WriteLine("Najleszy koszt to: " + tsp.GetBestSpecimen().Cost +
                                              ", który został uzyskany przy ścieżce:");
                            tsp.printPath(tsp.GetBestSpecimen().Path);
                        }
                        else
                        {
                            Console.WriteLine("Najpierw wczytaj dane");
                        }

                        break;
                    case 9:
                        Test.run();
                        break;
                    case 0:
                        exist = false;
                        break;
                    default:
                        Console.WriteLine("Poza zakresem");
                        break;



                }
            }
        }
    }
}
