using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG
{
    class Program
    {
        static void Main(string[] args)
        {

            int tspSize = 10;
            TSP tsp = new TSP(0, tspSize);
            tsp.PrintMatrix();

            int nIndiv = 20;
            int n = tspSize;
            int rateMut = 70;
            int gen = 100;
            int nIndivGen = 10;
            GeneticAlgorithm.setAdj(tsp.adj);

            GeneticAlgorithm.PrintSolution(GeneticAlgorithm.GA(nIndiv, n, rateMut, gen, nIndivGen));

            Console.WriteLine("Ended!");
        }
    }
}
