using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TSP_Genetyk.Classes
{
    class MatrixReader
    {
        public int[][] Matrix { get; set; }
        public string name { get; set; }
        private int size;

        public void load(string path)
        {

            travellingSalesmanProblemInstance TSPins = new travellingSalesmanProblemInstance();
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(travellingSalesmanProblemInstance));
                TSPins = (travellingSalesmanProblemInstance) serializer.Deserialize(reader);
            }

            travellingSalesmanProblemInstanceVertexEdge[][] TSPmatrix = TSPins.graph;
            size = TSPmatrix.Length;
            name = TSPins.name;
            Matrix = new int[size][];
            for (int i = 0; i < size; i++)
            {
                Matrix[i] = new int[size];
            }

            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                Matrix[i][j] = int.Parse(TSPmatrix[i][j].cost + "");
        }

        public void print()
        {
            for (int i = 0; i < Matrix.Length; i++)
            {
                for (int j = 0; j < Matrix.Length; j++) Console.Write(String.Format("{0,4}", Matrix[i][j]));
                System.Console.WriteLine();
            }
        }

        public void random(int size)
        {
            this.size = size;
            Matrix=new int[size][];
            for (int i = 0; i < size; i++)
            {
                Matrix[i] = new int[size];
            }
            Random random=new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        Matrix[i][j] = random.Next(100) + 1;
                    }
                    else
                    {
                        Matrix[i][j] = 0;
                    }
                }
            }
        }

    }



}
