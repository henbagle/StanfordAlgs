﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StanfordAlgsPart1
{
    // WEEK 2 - SECTION 3
    public class Inversions
    {
        // Some functions to count inversions - pairs in an array that are out of order
        // Eg: [4, 3, 2, 1] has 6 inversions. [4, 3], [4, 2], [4, 1] ... [2, 1]
        private int[] arr;
        public Tuple<int, int>[] inversions; // Using tuples was a dumbo idea

        public int CountInversions()
        {
            return inversions.Length;
        }

        public Inversions(int[] arrIn)
        {
            arr = new int[arrIn.Length];
            arr = arrIn;

            if (arr.Length <= 1)
            {
                inversions = new Tuple<int, int>[0];
            }
            else
            {
                // Set up left and right half arrays
                int mid = arr.Length / 2;

                int[] left;
                if (arr.Length % 2 != 0) left = new int[mid + 1];
                else left = new int[mid];

                int[] right = new int[mid];

                Array.Copy(arr, 0, left, 0, left.Length);
                Array.Copy(arr, left.Length, right, 0, right.Length);

                // Recursively check left side, right side, split inversions
                Inversions leftInvs = new Inversions(left);
                Inversions rightInvs = new Inversions(right);
                MergeAndFindSplitInversions(leftInvs, rightInvs);
            }
        }

        // Subroutine to get split inversions between two arrays, and also merge
        void MergeAndFindSplitInversions(Inversions left, Inversions right)
        {
            // This is the exact same as the merge subroutine from merge sort
            // Except there's an extra part about figuring out all the inversions
            int ai = 0;
            int bi = 0;
            arr = new int[(left.arr.Length + right.arr.Length)];
            List<Tuple<int, int>> splitInvs = new List<Tuple<int, int>>();

            // Loop through the output array
            for (int k = 0; k < arr.Length; k++)
            {
                // Step through each array in parallel, committing the smallest value of the two
                // to the output array, then looking at the next value in that input array

                // Handle what happens when you reach the end of an array
                if (bi == right.arr.Length) // At the end of the b array
                {
                    arr[k] = left.arr[ai];
                    ai++;
                }
                else if (ai == left.arr.Length) // at the end of the a array
                {
                    arr[k] = right.arr[bi];
                    bi++;
                }
                else if (left.arr[ai] <= right.arr[bi]) // a value is smaller
                {
                    arr[k] = left.arr[ai];
                    ai++;
                }
                else // b value is smaller, ergo the entire rest of the a array must be an inversion
                {
                    // Loop over the rest of the A array adding inversions - i think this makes the algorithm not n log(n) any longer - probably n^2
                    // If we were to just keep a running count of the NUMBER of inversions instead of actually figuring them all out, it would be n log n
                    for(int ai2 = ai; ai2 < left.arr.Length; ai2++)
                    {
                        splitInvs.Add(Tuple.Create(left.arr[ai2], right.arr[bi]));
                    }
                    arr[k] = right.arr[bi];
                    bi++;
                }

            }

            // Sum the resulting Tuple arrays together
            inversions = new Tuple<int, int>[left.inversions.Length + right.inversions.Length + splitInvs.Count];
            left.inversions.CopyTo(inversions, 0);
            right.inversions.CopyTo(inversions, left.inversions.Length);
            splitInvs.CopyTo(inversions, (left.inversions.Length + right.inversions.Length));
        }
    }
}
