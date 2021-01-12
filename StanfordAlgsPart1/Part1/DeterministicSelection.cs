using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgs
{
    // WEEK 4 - Optional implementation of the deterministic selection algorithm via median-of-medians
    // O(n), but has larger coefficients and uses more memory than the in-place randomized algorithm.
    // Concept: Instead of picking a random pivot, bucket elements into fives and take the median of those medians as the pivot
    public class DeterministicSelection
    {
        public static T Select<T>(T[] arr, int i)
            where T : IComparable<T>
        {
            if (arr.Length == 1)
            {
                return arr[0];
            }
            int p = ChoosePivot(arr);

            p = SortUtils.Partition(ref arr, p, 0, arr.Length);

            // After partitioning, the pivot is in the correct location. IE, the pth element is the pth order statistic
            // We can then determine which half to recurse on, like binary search
            // We use p + 1 because the ith order is 1 indexed, not zero. You want the 5th value which is arr[4], not arr[5]
            if (p + 1 == i) return arr[p];
            else if (p + 1 > i)
            {
                T[] half = new T[p - 1];
                Array.Copy(arr, half, p - 1);
                return Select(half, i);
            }
            else
            {
                T[] half = new T[arr.Length - p - 1];
                Array.Copy(arr, p + 1, half, 0, arr.Length - p - 1);
                return Select(half, i - p - 1);
            }
        }

        private static int ChoosePivot<T>(T[] arr)
            where T : IComparable<T>
        {
            // Choose pivot based on the median of medians.
            // Assign all elements to a data structure that keeps track of the original array index.
            TVal<T>[] arr2 = new TVal<T>[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr2[i] = new TVal<T>(arr[i], i);
            }

            // Run the Choose Pivot recursive subroutine, and return the index of the element it returns
            TVal<T> result = CPRecurse<T>(arr2);

            return result.i;
        }

        private static TVal<T> CPRecurse<T>(TVal<T>[] arr)
            where T : IComparable<T>
        {
            // Base case - return the median element
            if (arr.Length <= 5)
            {
                TVal<T>[] basecase = MergeSort.Sort(arr);
                return basecase[GetMiddleValue(arr)];
            }

            // Split the input array into arrays of five values
            // Multidimensional jagged array
            TVal<T>[][] split = new TVal<T>[(arr.Length + 4) / 5][];
            for (int i = 0; i < split.Length; i++)
            {
                if (i < (split.Length - 1))
                {
                    split[i] = new TVal<T>[5];
                    Array.Copy(arr, (i * 5), split[i], 0, 5); ;
                }
                else
                {
                    split[i] = new TVal<T>[arr.Length - (i * 5)];
                    Array.Copy(arr, i * 5, split[i], 0, split[i].Length);
                }
            }

            // For each set of five values, find the median and add it to an array
            TVal<T>[] medians = new TVal<T>[split.Length];
            for (int i = 0; i < split.GetLength(0); i++)
            {
                split[i] = MergeSort.Sort(split[i]);
                medians[i] = split[i][GetMiddleValue(split[i])];
            }

            // Recurse on this set of median values
            return CPRecurse(medians);
            // In the lecture, this recurses with the whole algorithm looking for the n/2th order
            // EG: return Select(medians, medians.Length/2); but I couldn't get this to work because I'm bad at C#
            // I'm 95% sure the ChoosePivot routine still works with O(n) time via the master method
        }

        // This method could be extracted to SortUtils class
        private static int GetMiddleValue<T>(TVal<T>[] arr2)
            where T : IComparable<T>
        {
            if (arr2.Length % 2 == 1) return (arr2.Length) / 2;
            else return (arr2.Length / 2) - 1;
        }
    }

    // Wrapper of T that can keep track of its original array index
    // Exposes the CompareTo method of T
    internal class TVal<T> : IComparable<TVal<T>>
        where T : IComparable<T>
    {
        public T v; // Value
        public int i; // Index in original input array

        public TVal(T V, int I)
        {
            v = V;
            i = I;
        }

        public int CompareTo(TVal<T> to)
        {
            return v.CompareTo(to.v);
        }
    }
}