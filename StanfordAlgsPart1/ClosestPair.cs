using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordAlgsPart1
{
    // Divide and Conquer algorithm for finding the closest pair of an array of points
    public class FindClosestPair
    {
        public Point[] XRanked;
        public Point[] YRanked;
        public FindClosestPair(Point[] P)
        {
            XRanked = Sorts.MergeSort(P, (A, B) => { return (int)(A.X - B.X); });
            YRanked = Sorts.MergeSort(P, (A, B) => { return (int)(A.Y - B.Y); });
        }

        //public (Point, Point) ClosestPair()
        //{
        //    if(XRanked.Length <= 3)
        //    {
        //        return BaseCase(XRanked);
        //    }
        //    else
        //    {
        //        // Divide and conquer
        //    }
        //}

        public (Point, Point) BaseCase(Point[] arr)
        {
            if (arr.Length > 3) throw new ArgumentOutOfRangeException("Input too long.");

            Point a = arr[0];
            Point b = arr[1];
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (Point.EuclideanDistance(arr[i], arr[j]) < Point.EuclideanDistance(a, b))
                    {
                        a = arr[i];
                        b = arr[j];
                    }
                }
            }
            return (a, b);
        }

        
    }

    public class Point : IComparable<Point>
    {
        public float X { get; }
        public float Y { get; }
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
        public int CompareTo(Point to) // This can be refined
        {
            return (int)EuclideanDistance(this, to);
        }
        
        public static double EuclideanDistance(Point a, Point b)
        {
            double x = Math.Pow((a.X - b.X), 2);
            double y = Math.Pow((a.Y - b.Y), 2);
            return Math.Sqrt(x + y);
        }
    }
}
