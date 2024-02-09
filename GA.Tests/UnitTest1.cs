using NUnit.Framework;
using AG;

namespace GA.Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void TestGenerateSolution(int size)
        {
            int[] s = new int[size];
            s = GeneticAlgorithm.GenerateSolution(size);
            for (int i = 0; i < s.Length; i++)
                for (int j = i + 1; j < s.Length; j++)
                    if (s[i] == s[j])
                        Assert.Fail();
            Assert.Pass();
        }

        [TestCase(5, new int[] { 0, 4, 1, 2, 3}, 197)]
        [TestCase(5, new int[] { 1, 2, 0, 3, 4}, 243)]
        [TestCase(5, new int[] { 2, 4, 0, 1, 3}, 456)]
        [TestCase(10, new int[] { 0, 8, 2, 9, 6, 4, 1, 7, 3, 5}, 607)]
        [TestCase(10, new int[] { 5, 2, 6, 4, 8, 9, 1, 3, 0, 7}, 399)]
        public void TestCost(int size, int[] s, int expected)
        {
            TSP tsp = new TSP(0, size);
            GeneticAlgorithm.setAdj(tsp.adj);
            Assert.AreEqual(GeneticAlgorithm.Cost(s), expected);
        }

        [TestCase(5, new int[] { 0, 4, 1, 2, 3 }, new int[] { 1, 2, 0, 3, 4 }, -1)]
        [TestCase(5, new int[] { 1, 2, 0, 3, 4 }, new int[] { 2, 4, 0, 1, 3}, -1)]
        [TestCase(5, new int[] { 0, 4, 1, 2, 3 }, new int[] { 2, 4, 0, 1, 3}, -1)]
        [TestCase(5, new int[] { 0, 4, 1, 2, 3 }, new int[] { 2, 3, 0, 4, 1}, 0)]
        [TestCase(5, new int[] { 2, 4, 0, 1, 3}, new int[] { 0, 4, 1, 2, 3 }, 1)]
        public void TestCompareIndiv(int size, int[] a, int[] b, int x)
        {
            //A > B = 1
            TSP tsp = new TSP(0, size);
            GeneticAlgorithm.setAdj(tsp.adj);
            if(x < 0)
                Assert.Less(GeneticAlgorithm.CompareIndiv(a, b), 0);
            else if(x > 0)
                Assert.Greater(GeneticAlgorithm.CompareIndiv(a, b), 0);
            else
                Assert.AreEqual(GeneticAlgorithm.CompareIndiv(a, b), 0);
        }

        [TestCase(new int[] { 2, 4, 0, 1, 3})]
        [TestCase(new int[] { 2, 4, 0, 1, 3, 5, 6, 7, 8, 9})]
        [TestCase(new int[] { 5, 2, 4, 6, 8, 0, 1, 9, 7, 3})]
        public void TestMutation(int[]s)
        {
            int[] v = new int[s.Length];
            s.CopyTo(v, 0);
            GeneticAlgorithm.Mutation(s);
            bool mutated = false;
            for (int i = 0; i < s.Length; i++)
                if(v[i] != s[i])
                {
                    mutated = true;
                    break;
                }
            Assert.IsTrue(mutated);
        }


        [TestCase(100, 5, 70, 1000, 50, 160)]
        [TestCase(20, 5, 70, 100, 10, 160)]
        [TestCase(100, 50, 100, 1000, 50, 300)]
        public void TestGeneticAlgorithm(int nIndiv, int n, int rateMut, int gen, int nIndivGen, int expectedLimit)
        {
            TSP tsp = new TSP(0, n);
            GeneticAlgorithm.setAdj(tsp.adj);
            int test = GeneticAlgorithm.Cost(GeneticAlgorithm.GA(nIndiv, n, rateMut, gen, nIndivGen));
            Assert.LessOrEqual(test, expectedLimit);
        }
    }
}