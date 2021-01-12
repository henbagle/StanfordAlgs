using System;
using System.Collections.Generic;
using System.Linq;

namespace StanfordAlgs
{
    // Divide and Conquer algorithm for finding the closest pair of an array of points
    public class ClosestPair
    {
        public Point[] XRanked;
        public Point[] YRanked;

        public ClosestPair(Point[] P)
        {
            if (P.Length <= 1) throw new ArgumentException("Input list must have at least 2 points");
            XRanked = MergeSort.Sort(P, (A, B) => { return (int)(A.X - B.X); });
            YRanked = MergeSort.Sort(P, (A, B) => { return (int)(A.Y - B.Y); });
        }

        public (Point, Point) FindClosestPair()
        {
            return FindClosestPair(XRanked, YRanked);
        }

        public static (Point, Point) FindClosestPair(Point[] PX, Point[] PY)
        // PX and PY: Subsets of P, sorted by X and Y value respectively. Contain same values in different orders.
        {
            if (PX.Length <= 3)
            {
                // Brute force solution
                return BaseCase(PX);
                // Could use PY here, would not make a difference
            }
            else
            {
                (Point[] leftX, Point[] rightX) = SortUtils.SplitArrayInHalf(PX); // Get the left and right half of all poimts
                (Point[] leftY, Point[] rightY) = DivideP(leftX, PY); // Extract a sorted-by-Y array containing all values from the left half and the right half
                // This happens in Linear time. I don't believe it.
                // leftY is all the values in leftX but sorted by Y. rightY is the same.

                // Recursively find the closest pair in each half.
                (Point p1, Point q1) = FindClosestPair(leftX, leftY);
                (Point p2, Point q2) = FindClosestPair(rightX, rightY);

                // Smallest value we've already found
                double delta = Math.Min(Point.EuclideanDistance(p1, q1), Point.EuclideanDistance(p2, q2));

                // Find the closest pair between left and right
                (Point p3, Point q3) = ClosestSplitPair(PX, PY, delta);

                // Figure out which one is smaller, return that
                if (Point.EuclideanDistance(p3, q3) <= delta)
                {
                    return (p3, q3);
                }
                else if (Point.EuclideanDistance(p2, q2) == delta)
                {
                    return (p2, q2);
                }
                else
                {
                    return (p1, q1);
                }
            }
        }

        // Returns still-sorted subset of PSorted that contains all the values from HalfOfP
        private static (Point[], Point[]) DivideP(Point[] HalfOfP, Point[] PSorted)
        {
            Point[] outLeft = new Point[HalfOfP.Length];
            int li = 0;
            Point[] outRight = new Point[PSorted.Length - HalfOfP.Length];
            int ri = 0;
            for (int i = 0; i < PSorted.Length; i++)
            {
                if (HalfOfP.Contains(PSorted[i]))
                {
                    outLeft[li] = PSorted[i];
                    li++;
                }
                else
                {
                    outRight[ri] = PSorted[i];
                    ri++;
                }
            }

            return (outLeft, outRight);
        }

        private static (Point, Point) ClosestSplitPair(Point[] PX, Point[] PY, double d)
        {
            Point p1 = null; // Pointers to best pair we've found
            Point p2 = null;
            double best = d; // Euclidean distance between best pair, starting at delta (best pair from left half, right half)

            // Define xBar, the midpoint between left and right used earlier
            int xBarIndex = (PX.Length / 2) - 1;
            if (PX.Length % 2 == 1) xBarIndex = (PX.Length / 2);
            double xBar = (double)PX[xBarIndex].X;
            (float xMin, float xMax) = ((float)(xBar - d), (float)(xBar + d)); // Midpoint of X, +- the previous smallest distance between points.
            List<Point> Sy = new List<Point>();

            // Generate Sy, a list of points sorted by Y value for which the x value is between xMin and xMax
            // Throw out all values for which the X distance is greater than delta
            // If they're close together but on the same half, they'll be picked up earlier and don't matter to this. A split pair will be < delta from the center line in X.
            for (int i = 0; i < PY.Length; i++)
            {
                if (PY[i].X > xMin && PY[i].X < xMax)
                {
                    Sy.Add(PY[i]);
                }
            }

            // For some vague reason, a point in Sy's split pair that's < than delta
            // will always be within 8 points of its pair point.
            for (int i = 0; i < Sy.Count - 1; i++)
            {
                for (int j = 1; j < Math.Min(Sy.Count - i, 8); j++) // This makes it linear time - this iteration is not dependent on N
                {
                    if (Point.EuclideanDistance(Sy[i], Sy[i + j]) < best) // If the distance between [i], [i+j] is less than our best, make it our best. J is always < 8.
                    {
                        best = Point.EuclideanDistance(Sy[i], Sy[i + j]);
                        p1 = Sy[i];
                        p2 = Sy[i + j];
                    }
                }
            }

            // Return our best points, possibly null.
            return (p1, p2);
        }

        public static (Point, Point) BaseCase(Point[] arr)
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
            if (a == null || b == null) return Double.NaN;

            double x = Math.Pow((a.X - b.X), 2);
            double y = Math.Pow((a.Y - b.Y), 2);
            return Math.Sqrt(x + y);
        }
    }
}