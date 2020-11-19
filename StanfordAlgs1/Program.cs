using System;

namespace StanfordAlgs1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[] { 10, 11, 4, 2, 4, 5, 10, 19, 400, 2, 5, 6, 2, 9 };
            int[] testSort = Sorts.InsertionSort(test);
            Helpers.PrintValues(testSort);
        }
    }
}
