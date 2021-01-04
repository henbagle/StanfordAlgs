using System;
using StanfordAlgsPart1;

namespace StanfordAlgsPart1
{
    public class QuickSort
    {
        private static Random _rand = new Random();

        public static T[] Sort<T>(T[] arr)
            where T : IComparable<T>
        {
            // My QuickSort implementation sorts in place by default (to "save memory")
            // To make it not do that, we just gotta make a copy of the original array.
            T[] result = new T[arr.Length];
            Array.Copy(arr, result, arr.Length);

            SortInPlace(ref result);

            return result;
        }

        // Mutates original array
        public static void SortInPlace<T>(ref T[] arr)
            where T : IComparable<T>
        {
            SortRange(ref arr, 0, arr.Length);
        }

        private static void SortRange<T>(ref T[] arr, int s, int e)
        where T : IComparable<T>
        {
            // Base Case
            if (e - s <= 1)
            {
                return;
            }

            // Choose a pivot and partion the array around that point
            int p = ChoosePivot(ref arr, s, e);
            p = Partition(ref arr, p, s, e);

            // Sort both halves
            SortRange(ref arr, s, p);
            SortRange(ref arr, p + 1, e);

            // Return
            return;
        }

        public static int SortRangeCountComparisons<T>(ref T[] arr, int s, int e)
        where T : IComparable<T>
        {
            // Base Case
            if (e - s <= 1)
            {
                return 0;
            }

            // Choose a pivot and partion the array around that point
            int p = ChoosePivot(ref arr, s, e);
            p = Partition(ref arr, p, s, e);

            int comps = e - s - 1;

            // Sort both halves
            comps += SortRangeCountComparisons(ref arr, s, p);
            comps += SortRangeCountComparisons(ref arr, p + 1, e);

            // Return
            return comps;
        }

        private static int Partition<T>(ref T[] arr, int pivotIndex, int s, int e)
            where T : IComparable<T>
        {
            // Pre-processing step: Put the pivot in the first element of the list (bottom of the range, s)
            T pivot = arr[pivotIndex];
            arr[pivotIndex] = arr[s];
            arr[s] = pivot;

            bool startSwapping = false;

            int i = s + 1; // Index of boundary between smaller than the pivot and larger than the pivot

            for (int j = s + 1; j < e; j++) // Go over the rest of the array
            {
                // GOAL: All elements we've looked at so far (index < j) are partitioned into larger/smaller than pivot
                // i delineates the boundary between these two sub arrays, j delineates the boundary between what we have/haven't looked at
                // We make swaps with the jth index depending on it's status

                if (arr[j].CompareTo(pivot) <= 0)
                {
                    // If this element is less than the pivot, swap it with i and increase i
                    if (startSwapping) // This prevents unecessary swaps
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
                    i++;
                }
                else // j increases but i doesn't, we're gonna have to start swapping
                {
                    startSwapping = true;
                }
            }

            // Put the pivot back where it belongs
            (arr[s], arr[i - 1]) = (arr[i - 1], arr[s]);

            // Returns the new index of the pivot point
            return i - 1;
        }

        private static int ChoosePivot<T>(ref T[] arr, int s, int e)
            where T : IComparable<T>
        {
            return _rand.Next(s, e);
        }
    }
}