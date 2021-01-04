using Part1;
using System;

namespace Part1
{
    // WEEK 4
    // Input: Array with distinct numbers, similar input to sorting algorithms
    // Output: ith order statistic. EG {10, 8, 2, 4}, 3rd order statistic is 8.
    public class Selection
    {
        private static Random _rand = new Random();

        // Absolute worst case (unlikely): O(n^2)
        // Absolute best and average case: O(n)
        public static T RandomizedSelection<T>(T[] arr, int i)
            where T : IComparable<T>
        {
            T[] copy = new T[arr.Length];
            Array.Copy(arr, copy, arr.Length); ;
            return RSelect(ref copy, i, 0, copy.Length);
        }

        private static T RSelect<T>(ref T[] arr, int i, int s, int e)
            where T : IComparable<T>
        {
            if (e - s <= 0) throw new ArgumentOutOfRangeException();
            else if (e - s == 1)
            {
                return arr[s];
            }

            // Very similar to QuickSort, except instead of recursing on both halves, we only recurse on the relevant one

            int p = _rand.Next(s, e);
            p = SortUtils.Partition(ref arr, p, s, e);

            // After partitioning, the pivot is in the correct location. IE, the pth element is the pth order statistic
            // We can then determine which half to recurse on, like binary search
            // We use p + 1 because the ith order is 1 indexed, not zero. You want the 5th value which is arr[4], not arr[5]
            if (p + 1 == i) return arr[p];
            else if (p + 1 > i)
            {
                return RSelect(ref arr, i, s, p);
            }
            else
            {
                return RSelect(ref arr, i, p + 1, e);
            }
        }
    }
}