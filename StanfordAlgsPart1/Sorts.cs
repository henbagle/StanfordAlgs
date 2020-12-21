using System;

namespace StanfordAlgsPart1
{
    public class Sorts
    {
        public static int[] SelectionSort(int[] arr)
        {
            // Create a shallow copy
            int[] a = (int[])arr.Clone();
            int minI;

            // Could do this recursively, go through every sub-array ([0...n), [1...n], ... [n-1, n])
            for (int i = 0; i < a.Length - 1; i++)
            {
                minI = i;
                // Go through the whole sub-array looking for the index of the minimum value
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < a[minI]) minI = j;
                }
                // If it's not already at the bottom, put it there
                if (minI != i)
                {
                    int swap = a[minI];
                    a[minI] = a[i];
                    a[i] = swap;
                }
                // Move on to the next sub-array
            }
            return a;
        }

        public static int[] BubbleSort(int[] arr)
        {
            // Create a shallow copy
            int[] a = (int[])arr.Clone();
            int[] b = new int[a.Length - 1];

            //Base case
            if (a.Length == 1)
            {
                return arr;
            }

            //Loop over all inputs except the last one
            for (int i = 0; i < a.Length - 1; i++)
            {
                // Swap i with i+1 if they are in the wrong sorting order
                if (a[i] > a[i + 1])
                {
                    int temp = a[i];
                    a[i] = a[i + 1];
                    a[i + 1] = temp;
                }
            }
            // The last value in the array is now the largest

            // Make a shallow copy of the array from 0 to n-1
            Array.ConstrainedCopy(a, 0, b, 0, a.Length - 1);

            // Sort that array in the same manner (recursively), then return
            b = BubbleSort(b);
            b.CopyTo(a, 0);
            return a;
        }

        public static int[] InsertionSort(int[] arr)
        {
            int[] a = (int[])arr.Clone();

            // Loop over every element in the array
            for (int i = 1; i < a.Length; i++)
            {
                // Starting at that value, compare it with the previous element
                int j = i;

                // Shift it down until it is larger than the value before it or is the first value
                while (j > 0 && a[j - 1] > a[j])
                {
                    int swap = a[j];
                    a[j] = a[j - 1];
                    a[j - 1] = swap;
                    j--;
                }
            }

            return a;
        }
    }
}