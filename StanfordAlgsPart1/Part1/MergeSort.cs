﻿using System;

namespace StanfordAlgs
{
    public class MergeSort
    {
        // WEEK 1 - Implemented independently

        public static T[] Sort<T>(T[] arr)
            where T : IComparable<T>
        {
            if (arr.Length == 1) return arr; // Base case
            else
            {
                (T[] a1, T[] a2) = SortUtils.SplitArrayInHalf(arr);
                // Sort both halfs, then merge them together
                return Merge(Sort(a1), Sort(a2), null);
            }
        }

        public static T[] Sort<T>(T[] arr, Func<T, T, int> compare)
            where T : IComparable<T>
        {
            if (arr.Length == 1) return arr; // Base case
            else
            {
                (T[] a1, T[] a2) = SortUtils.SplitArrayInHalf(arr);
                // Sort both halfs, then merge them together
                return Merge(Sort(a1, compare), Sort(a2, compare), compare);
            }
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