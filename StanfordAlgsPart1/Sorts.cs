using System;

namespace StanfordAlgsPart1
{
    public class Sorts
    {
        // WEEK 1 - Implemented independently
        // Todo: Can we use generic Arrays/IComparable[] to sort instead of int[]?

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

        public static T[] MergeSort<T>(T[] arr)
            where T : IComparable<T>
        {
            if (arr.Length == 1) return arr; // Base case
            else
            {
                (T[] a1, T[] a2) = SplitArrayInHalf(arr);
                // Sort both halfs, then merge them together
                return Merge(MergeSort(a1), MergeSort(a2), null);
            }
        }

        public static T[] MergeSort<T>(T[] arr, Func<T, T, int> compare)
            where T : IComparable<T>
        {
            if (arr.Length == 1) return arr; // Base case
            else
            {
                (T[] a1, T[] a2) = SplitArrayInHalf(arr);
                // Sort both halfs, then merge them together
                return Merge(MergeSort(a1, compare), MergeSort(a2, compare), compare);
            }
        }

        public static (T[], T[]) SplitArrayInHalf<T>(T[] arrayToSplit)
            where T : IComparable<T>
        {
            if (arrayToSplit.Length <= 1) throw new ArgumentOutOfRangeException("Input must have two or more elements.");

            // Create new arrays based on input N size
            T[] left;
            T[] right = new T[arrayToSplit.Length / 2];

            // Handle odd n arrays for left side
            if (arrayToSplit.Length % 2 == 1) left = new T[(arrayToSplit.Length / 2) + 1];
            else left = new T[arrayToSplit.Length / 2];

            // Copy over values from the original to the two arrays
            Array.Copy(arrayToSplit, left, left.Length);
            Array.Copy(arrayToSplit, left.Length, right, 0, (arrayToSplit.Length / 2));

            // Return tuple of arrays
            return (left, right);
        }

        // Merges two sorted arrays
        private static T[] Merge<T>(T[] a, T[] b, Func<T, T, int> userCompare)
            where T : IComparable<T>
        {
            T[] @out = new T[a.Length + b.Length];
            int ai = 0;
            int bi = 0;

            // Set a default compare function if there is no specified one
            Func<T, T, int> compareFunction;
            if (userCompare == null) compareFunction = (a, b) => { return a.CompareTo(b); }; // use default type CompareTo behavior
            else compareFunction = userCompare;

            // Loop through the output array
            for (int k = 0; k < @out.Length; k++)
            {
                // Step through each array in parallel, committing the smallest value of the two
                // to the output array, then looking at the next value in that input array

                // First two ifs handle what happens when you reach the end of an array
                if (bi == b.Length) // At the end of the b array
                {
                    @out[k] = a[ai];
                    ai++;
                }
                else if (ai == a.Length) // at the end of the a array
                {
                    @out[k] = b[bi];
                    bi++;
                }
                else if (compareFunction(a[ai], b[bi]) < 0) // a value is smaller
                {
                    @out[k] = a[ai];
                    ai++;
                }
                else // b value is smaller
                {
                    @out[k] = b[bi];
                    bi++;
                }
            }

            return @out;
        }
    }
}