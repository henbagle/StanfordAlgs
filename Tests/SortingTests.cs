using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgsPart1;

namespace Tests
{
    [TestClass]
    public class SortingTests
    {
        private int[] sortedArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        private int[] reverseArr = { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        private int[] notSortedArr = { 15, 8, 5, 12, 10, 1, 16, 9, 11, 7, 20, 3, 2, 6, 17, 18, 4, 13, 14, 19 };
        private int[] equalArr = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        private int[] negativeArr = { -1, 0, 5, -10, 20, 13, -7, 3, 2, -3 };
        private int[] negativeArrSorted = { -10, -7, -3, -1, 0, 2, 3, 5, 13, 20 };

        private int[] small1 = { 3, 4, 1, 5, 2 };

        private int[] smallSorted1 = { 1, 2, 3, 4, 5 };

        [TestMethod]
        public void BubbleSort_CorrectlySorts()
        {
            CollectionAssert.AreEqual(sortedArr, Sorts.BubbleSort(notSortedArr));
            CollectionAssert.AreEqual(sortedArr, Sorts.BubbleSort(reverseArr));
            CollectionAssert.AreEqual(sortedArr, Sorts.BubbleSort(sortedArr));
            CollectionAssert.AreEqual(equalArr, Sorts.BubbleSort(equalArr));
            CollectionAssert.AreEqual(negativeArrSorted, Sorts.BubbleSort(negativeArr));
        }

        [TestMethod]
        public void InsertionSort_CorrectlySorts()
        {
            CollectionAssert.AreEqual(sortedArr, Sorts.InsertionSort(notSortedArr));
            CollectionAssert.AreEqual(sortedArr, Sorts.InsertionSort(reverseArr));
            CollectionAssert.AreEqual(sortedArr, Sorts.InsertionSort(sortedArr));
            CollectionAssert.AreEqual(equalArr, Sorts.InsertionSort(equalArr));
            CollectionAssert.AreEqual(negativeArrSorted, Sorts.InsertionSort(negativeArr));
        }

        [TestMethod]
        public void SelectionSort_CorrectlySorts()
        {
            CollectionAssert.AreEqual(sortedArr, Sorts.SelectionSort(notSortedArr));
            CollectionAssert.AreEqual(sortedArr, Sorts.SelectionSort(reverseArr));
            CollectionAssert.AreEqual(sortedArr, Sorts.SelectionSort(sortedArr));
            CollectionAssert.AreEqual(equalArr, Sorts.SelectionSort(equalArr));
            CollectionAssert.AreEqual(negativeArrSorted, Sorts.SelectionSort(negativeArr));
        }

        [TestMethod]
        public void MergeSort_CorrectlySorts()
        {
            CollectionAssert.AreEqual(sortedArr, MergeSort.Sort(notSortedArr));
            CollectionAssert.AreEqual(sortedArr, MergeSort.Sort(reverseArr));
            CollectionAssert.AreEqual(sortedArr, MergeSort.Sort(sortedArr));
            CollectionAssert.AreEqual(equalArr, MergeSort.Sort(equalArr));
            CollectionAssert.AreEqual(negativeArrSorted, MergeSort.Sort(negativeArr));
        }

        [TestMethod]
        public void QuickSort_CorrectlySorts()
        {
            CollectionAssert.AreEqual(sortedArr, QuickSort.Sort(notSortedArr));
            CollectionAssert.AreEqual(sortedArr, QuickSort.Sort(reverseArr));
            CollectionAssert.AreEqual(sortedArr, QuickSort.Sort(sortedArr));
            CollectionAssert.AreEqual(equalArr, QuickSort.Sort(equalArr));
            CollectionAssert.AreEqual(negativeArrSorted, QuickSort.Sort(negativeArr));
        }

        [TestMethod]
        public void QuickSort_CorrectlySortsInPlace()
        {
            QuickSort.SortInPlace(ref notSortedArr);
            QuickSort.SortInPlace(ref reverseArr);
            QuickSort.SortInPlace(ref sortedArr);
            QuickSort.SortInPlace(ref equalArr);
            QuickSort.SortInPlace(ref negativeArr);

            CollectionAssert.AreEqual(sortedArr, notSortedArr);
            CollectionAssert.AreEqual(sortedArr, reverseArr);
            CollectionAssert.AreEqual(sortedArr, sortedArr);
            CollectionAssert.AreEqual(equalArr, equalArr);
            CollectionAssert.AreEqual(negativeArrSorted, negativeArr);
        }
    }
}