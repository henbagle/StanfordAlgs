using System;

namespace StanfordAlgsPart1
{
    public class QuickSort
    {
        // Why'd I do it like this? I wish I knew.
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
            Sort(ref arr, 0, arr.Length);
        }

        public static void Sort<T>(ref T[] arr, int s, int e)
        where T : IComparable<T>
        {
            // Base Case
            if (e - s <= 1)
            {
                return;
            }
            else if (e - s == 2)
            {
                if (arr[s].CompareTo(arr[s + 1]) <= 0)
                {
                    return;
                }
                else
                {
                    (arr[s], arr[s + 1]) = (arr[s + 1], arr[s]);
                    return;
                }
            }

            // Choose a pivot and partion the array around that point
            int p = ChoosePivot(ref arr, s, e);
            p = Partition(ref arr, s + p, s, e);

            // Sort both halves
            Sort(ref arr, s, p);
            Sort(ref arr, p + 1, e);

            // Return
            return;
        }

        private static int Partition<T>(ref T[] arr, int pivotIndex, int s, int e)
            where T : IComparable<T>
        {
            // Pre-processing step: Put the pivot in the first element of the list
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

            (arr[s], arr[i - 1]) = (arr[i - 1], arr[s]);

            // Returns a tuple: The partitoned array, and the new index of the pivot point
            return i - 1;
        }

        private static int ChoosePivot<T>(ref T[] arr, int s, int e)
            where T : IComparable<T>
        {
            return 1;
        }
    }
}