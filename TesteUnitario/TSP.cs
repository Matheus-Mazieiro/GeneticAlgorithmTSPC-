 using System;
using System.Collections.Generic;
using System.Text;


namespace AG
{
    public class TSP
    {
        public int[,] adj;// = new int[100, 100];

        public TSP(int seed, int size)
        {
            adj = initMatrix(seed, size);
            PrintMatrix();
        }

        public int[,] initMatrix(int seed, int size)
        {
            var rand = new Random(seed);
            int[,] adj = new int[size, size];
            for (int i = 0; i < adj.GetLength(0); i++)
                for (int j = 0; j < adj.GetLength(1); j++)
                    adj[i, j] = rand.Next(0, 100);
            return adj;
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < adj.GetLength(0); i++)
            {
                for (int j = 0; j < adj.GetLength(1); j++)
                {
                    Console.Write(string.Format(" {0} ", adj[i, j]));
                }
                    Console.WriteLine("");
            }
        }
    }
}
