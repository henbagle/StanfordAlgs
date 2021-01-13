using System;
using StanfordAlgs;
using StanfordAlgs.Graphs;

namespace StanfordAlgsCLI
{
    // I use this to write stuff to the console when I want to know what's going on and debug stuff.
    // Also to read/write to files for programming assignments.
    // Probably a better way of going about it.
    internal class Program
    {
        private static void Main(string[] args)
        {
            AdjacencyListGraph<int> graph = new AdjacencyListGraph<int>();
            string[] lines = Helpers.GetLinesFromFile("C:/Users/benha/source/repos/StanfordAlgs/CLI/Problems/kargerMinCut.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                graph.AddNode(i + 1);
            }

            foreach (string line in lines)
            {
                string[] elements = line.Split('\t');
                int node = int.Parse(elements[0]);
                for (int i = 1; i < elements.Length; i++)
                {
                    if (int.TryParse(elements[i], out int to))
                    {
                        if (!graph.GetNode(node - 1).IsConnectedTo(graph.GetNode(to - 1)))
                        {
                            graph.AddEdge(node - 1, to - 1);
                        }
                    }
                }
            }

            Console.WriteLine(MinimumCut.GetMinimumCut(graph));
        }
    }
}