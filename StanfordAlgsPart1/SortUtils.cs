using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class SortUtils
    {
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

        // Partition array in place around given element index
        // Input: reference to array, index of pivot, starting and ending index to work in a subarray
        public static int Partition<T>(ref T[] arr, int pivotIndex, int s, int e)
            where T : IComparable<T>
        {
            if (pivotIndex < s || pivotIndex > e || e > arr.Length || s < 0) throw new ArgumentOutOfRangeException();

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
    }
}