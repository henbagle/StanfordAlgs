using System;

namespace StanfordAlgs1
{
    class Sorts
    {
        public static int[] SelectionSort(int[] arr)
        {
            return arr;
        }
        public static int[] BubbleSort(int[] arr)
        {
            // Create a shallow copy

            int[] a = (int[])arr.Clone();
            int[] b = new int[a.Length - 1];

            if (a.Length == 1)
            {
                return arr;
            }


            for (int i = 0; i < a.Length - 1; i++)
            {
                if (a[i] > a[i + 1])
                {
                    int temp = a[i];
                    a[i] = a[i + 1];
                    a[i + 1] = temp;
                }
            }

            Array.ConstrainedCopy(a, 0, b, 0, a.Length - 1);
            b = BubbleSort(b);
            b.CopyTo(a, 0);
            return a;

        }
        public static int[] InsertionSort(int[] arr)
        {
            return arr;
        }
        public static int[] MergeSort(int[] arr)
        {
            return arr;
        }
    }
    class Helpers
    {
        public static void PrintValues(Array myArr)
        {
            System.Collections.IEnumerator myEnumerator = myArr.GetEnumerator();
            int i = 0;
            int cols = myArr.GetLength(myArr.Rank - 1);
            while (myEnumerator.MoveNext())
            {
                if (i < cols)
                {
                    i++;
                }
                else
                {
                    Console.WriteLine();
                    i = 1;
                }
                Console.Write("\t{0}", myEnumerator.Current);
            }
            Console.WriteLine();
        }
    }
}
