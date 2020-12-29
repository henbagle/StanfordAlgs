using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgsPart1
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
    }
}