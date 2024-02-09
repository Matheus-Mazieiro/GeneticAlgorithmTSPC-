using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG
{
    public static class GeneticAlgorithm
    {
        public static int[,] adjMatrix;

        public static void setAdj(int[,] adj)
        {
            adjMatrix = adj;
        }

        public static int[] GenerateSolution(int n, Random rand = null)
        {
            int[] s = new int[n];
            for (int j = 0; j < n; j++)
            {
                s[j] = j;
            }

            if(rand == null)
                rand = new Random();
            int i = n;
            while (i > 1)
            {
                i--;
                int k = rand.Next(n);
                int value = s[k];
                s[k] = s[i];
                s[i] = value;
            }
            return s;
        }


        public static List<int[]> GeneratePopulation(int nIndiv, int n)
        {
            List<int[]> pop = new List<int[]>();
            Random rand = new Random();
            for (int i = 0; i < nIndiv; i++)
            {
                pop.Add(GenerateSolution(n, rand));
            }
            return pop;
        }

        public static int Cost(int[] indiv)
        {
            int cost = 0;
            for (int i = 0; i < indiv.Length - 1; i++)
                cost += adjMatrix[indiv[i], indiv[i + 1]];
            cost += adjMatrix[indiv[indiv.Length - 1], indiv[0]];
            return cost;
        }

        public static void PrintSolution(int[] s)
        {
            Console.Write("Cromossomo: ");
            for (int i = 0; i < s.Length; i++)
                Console.Write(string.Format(" {0} |", s[i]));
            Console.WriteLine(string.Format("--> {0}", Cost(s)));
        }

        public static int CompareIndiv(int[] a, int[] b)
        {
            // Lógica de comparação aqui (neste exemplo, assume-se que a aptidão é a soma dos valores dos indivíduos)
            int A = Cost(a);
            int B = Cost(b);
            return A.CompareTo(B);
        }

        public static void EvaluatePop(List<int[]> pop)
        {
            pop.Sort(CompareIndiv);
        }

        public static int[] SelecionaPai(List<int[]> pop)
        {
            Random rand = new Random();
            int[] p1 = pop[rand.Next(0, pop.Count)];
            int[] p2 = pop[rand.Next(0, pop.Count)];
            return (Cost(p1) <= Cost(p2)) ? p1 : p2;
        }

        public static int[] Crossover(int[] p1, int[] p2)
        {
            int[] filho = new int[p1.Length];
            Random rand = new Random();
            int a = rand.Next(p1.Length);
            int b = rand.Next(p1.Length);
            if (b < a)
            {
                int aux = a;
                a = b;
                b = aux;
            }
            int i = 0;
            for (int j = a; j <= b; j++)
                filho[i++] = p1[j];

            for (int j = 0; j < p2.Length; j++)
            {
                bool copia = true;
                for (int k = 0; k < i && copia == true; k++)
                {
                    if (filho[k] == p2[j])
                        copia = false;
                }
                if (copia)
                    filho[i++] = p2[j];
            }
            return filho;
        }

        public static void Mutation(int[] c)
        {
            Random rand = new Random();
            int a = rand.Next(0, c.Length);
            int b = rand.Next(0, c.Length);
            int aux = c[a];
            c[a] = c[b];
            c[b] = aux;
        }

        public static int[] GA(int nIndiv, int n, int rateMut, int gen, int nIndivGen)
        {
            List<int[]> pop = GeneratePopulation(nIndiv, n);

            foreach (var item in pop)
            {
                PrintSolution(item);
            }


            EvaluatePop(pop);

            for (int i = 0; i < gen; i++)
            {
                for (int j = 0; j < nIndivGen; j++)
                {
                    int[] p1 = SelecionaPai(pop);
                    int[] p2 = SelecionaPai(pop);
                    int[] c = Crossover(p1, p2);
                    Random rand = new Random();
                    if (rand.Next(0, 100) >= rateMut)
                        Mutation(c);
                    pop.Add(c);
                }
                EvaluatePop(pop);
                int remAmunt = pop.Count() - nIndiv;
                pop.RemoveRange(nIndiv, remAmunt);
            }

            
            return pop[0];
        }
    }
}
