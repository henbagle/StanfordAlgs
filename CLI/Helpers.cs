using System;
using System.Collections.Generic;
using System.IO;

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
    }
}