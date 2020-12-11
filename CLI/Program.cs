using System;
using StanfordAlgsPart1;

namespace StanfordAlgsCLI
{
    // I use this to write stuff to the console when I want to know what's going on and debug stuff.
    // Probably a better way of going about it.
    class Program
    {
        static void Main(string[] args)
        {
            int[] notSortedArr = { 15, 8, 5, 12, 10, 1, 16, 9, 11, 7, 20, 3, 2, 6, 17, 18, 4, 13, 14, 19 };
            int[] sortedArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            int[] toGetInversions = { 4, 3, 2, 1 };
            Inversions gotEm = new Inversions(toGetInversions);
            Helpers.PrintValues(gotEm.inversions);
        }
    }
}
