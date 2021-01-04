using System;
using StanfordAlgsPart1;

namespace StanfordAlgsCLI
{
    // I use this to write stuff to the console when I want to know what's going on and debug stuff.
    // Also to read/write to files for programming assignments.
    // Probably a better way of going about it.
    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] input = Helpers.GetIntsFromFile("C:/Users/benha/source/repos/StanfordAlgs/CLI/Problems/QuickSort.txt");

            Console.Write(QuickSort.SortRangeCountComparisons(ref input, 0, 10000));
        }
    }
}