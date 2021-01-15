using System;
using System.Collections.Generic;
using System.IO;
using StanfordAlgs.Graphs;

namespace StanfordAlgsCLI
{
    public class Helpers
    {
        public static void PrintValues(Array myArr)
        {
            System.Collections.IEnumerator myEnumerator = myArr.GetEnumerator();
            int i = 0;
            int cols = myArr.GetLength(myArr.Rank - 1);
            while (myEnumerator.MoveNext())
            {
                if (i < cols)
                {
                    i++;
                }
                else
                {
                    Console.WriteLine();
                    i = 1;
                }
                Console.Write("\t{0}", myEnumerator.Current);
            }
            Console.WriteLine();
        }

        public static int[] GetIntsFromFile(string location)
        {
            List<int> output = new List<int>();
            StreamReader reader = new StreamReader(location);
            while (!reader.EndOfStream)
            {
                if (int.TryParse(reader.ReadLine(), out int num))
                {
                    output.Add(num);
                }
            }

            return output.ToArray();
        }

        public static string[] GetLinesFromFile(string location)
        {
            List<string> output = new List<string>();
            StreamReader reader = new StreamReader(location);
            while (!reader.EndOfStream)
            {
                output.Add(reader.ReadLine());
            }

            return output.ToArray();
        }

        public static AdjacencyListGraph<int> BuildGraphFromFile(string location, char separator)
        {
            AdjacencyListGraph<int> graph = new AdjacencyListGraph<int>();
            string[] lines = GetLinesFromFile(location);
            for (int i = 0; i < lines.Length; i++)
            {
                graph.AddNode(i + 1);
            }

            foreach (string line in lines)
            {
                string[] elements = line.Split(separator);
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

            return graph;
        }
    }
}