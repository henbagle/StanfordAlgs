using System;
using System.Collections.Generic;
using System.Text;

namespace StanfordAlgsPart1
{
    // WEEK 2 - Implemented independently
    public class Search
    {
        // Search through a sorted list to find a target element
        public static int BinarySearch(int target, int[] arr)
        {
            int startIndex = 0;
            int endIndex = arr.Length - 1;

            // Start and endIndex can be equal - check if target is the very last value you get to
            while (startIndex <= endIndex)
            {
                int middleIndex = ((endIndex - startIndex) / 2) + startIndex;

                if (arr[middleIndex] == target)
                {
                    return middleIndex;
                }

                // Target is above middle, look at upper half
                if(arr[middleIndex] < target)
                {
                    startIndex = middleIndex + 1; // We've already looked at middle, so we can start one away from it. 
                    // It also ensures that if we can't find the element, endIndex is less than startIndex.
                }
                else // Look at bottom half
                {
                    endIndex = middleIndex - 1;
                }
            }
            return -1;

        }
    }
}
