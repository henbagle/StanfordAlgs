using System;
using StanfordAlgs.Part2;
using StanfordAlgs.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace StanfordAlgsCLI
{
    // I use this to write stuff to the console when I want to know what's going on and debug stuff.
    // Also to read/write to files for programming assignments.
    // Probably a better way of going about it.
    internal class Program
    {
        private static void Main(string[] args)
        {
            AdjacencyListGraph<int> graph = Helpers.BuildDirectedGraphFromFile("C:/Users/benha/source/repos/StanfordAlgs/CLI/Problems/2Week2PA1.txt", ' ', 875714);
            Console.WriteLine("Loaded in graph!");
            List<List<Node<int>>> output = ConnectedComponents.StronglyConnectedComponents<int>(graph);

            Console.WriteLine("Finished computing SCCs");

            output.Sort((List<Node<int>> a, List<Node<int>> b) => { return b.Count - a.Count; });

            for (int i = 0; i < 5; i++)
            {
                if (output.Count <= i)
                {
                    Console.Write("0");
                }
                else
                {
                    Console.Write(output[i].Count);
                }
                Console.Write(",");
            }
            //875713
        }
    }
}