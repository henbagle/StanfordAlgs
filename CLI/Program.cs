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
            AdjacencyListGraph<int> graph = Helpers.BuildGraphFromFile("C:/Users/benha/source/repos/StanfordAlgs/CLI/Problems/kargerMinCut.txt", '\t');

            Console.WriteLine(MinimumCut.GetMinimumCut(graph));
        }
    }
}