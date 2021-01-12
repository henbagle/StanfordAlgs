using System;

namespace StanfordAlgs
{
    // WEEK 3 - PROBLEM SET 3
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
            p = SortUtils.Partition(ref arr, p, s, e);

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
            p = SortUtils.Partition(ref arr, p, s, e);

            int comps = e - s - 1;

            // Sort both halves
            comps += SortRangeCountComparisons(ref arr, s, p);
            comps += SortRangeCountComparisons(ref arr, p + 1, e);

            // Return
            return comps;
        }

        public static int ChoosePivot<T>(ref T[] arr, int s, int e)
            where T : IComparable<T>
        {
            return _rand.Next(s, e);
        }
    }
}