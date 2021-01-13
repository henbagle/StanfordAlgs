using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StanfordAlgs.Graphs;

namespace StanfordAlgs
{
    // PART 1 WEEK 4
    // Random Contraction algorithm for finding the minimum cut
    public class MinimumCut
    {
        private static Random _rand = new Random();

        public static int GetMinimumCut<T>(AdjacencyListGraph<T> g)
        {
            int NUM_TRIALS = (int)Math.Pow(g.NodeCount, 2);
            int result = g.EdgeCount;
            AdjacencyListGraph<T> gc;

            // Run this a lot of times
            for (int i = 0; i < NUM_TRIALS; i++)
            {
                // Create a deep clone of the original graph and run a trial
                gc = g.Clone();
                int trialResult = MinCutTrial(gc);
                //if (i % 100 == 0) Console.WriteLine($"Trial: {i}, Result: {result}");
                if (trialResult < result) result = trialResult;
            }

            if (result == g.EdgeCount) throw new Exception("Something went wrong.");
            else return result;
        }

        private static int MinCutTrial<T>(AdjacencyListGraph<T> g)
        {
            while (g.NodeCount > 2)
            {
                // Contract random edges until there are only two nodes left
                int edgeID = _rand.Next(0, g.EdgeCount);
                Edge<T> edge = g.E.ElementAt(edgeID);
                g.Contract(edge);
            }

            return g.EdgeCount;
        }
    }
}