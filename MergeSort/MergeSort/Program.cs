using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] { 3, 4, 7, 5, 8};
            Array.ForEach(nums, Console.WriteLine);
            Console.WriteLine();

            int[] sortedNums = MergeSort(nums);
            Array.ForEach(sortedNums, Console.WriteLine);
        }

        static int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
                return array;

            int middle = array.Length / 2;
            int[] arrayLeft = array[..middle];
            int[] arrayRight = array[middle..];

            arrayLeft = MergeSort(arrayLeft);
            arrayRight = MergeSort(arrayRight);

            return Merge(arrayLeft.ToList(), arrayRight.ToList());
        }

        static int[] Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();

            while (left.Count != 0 && right.Count != 0)
            {
                if (left[0] > right[0])
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
                else
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
            }

            while (left.Count != 0)
            {
                result.Add(left[0]);
                left.RemoveAt(0);
            }

            while (right.Count != 0)
            {
                result.Add(right[0]);
                right.RemoveAt(0);
            }

            return result.ToArray();
        }
    }
}
