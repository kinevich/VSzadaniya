using System;
using System.Collections.Generic;

namespace Zeroes
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = new int[] { 0, 0, 0, 1, 0, 0 };
            LinkedList<int> linkedList = new LinkedList<int>(ints);
            LinkedList<int> sortedList = RemoveZeroes(linkedList);
            LinkedListNode<int> runner = sortedList.First;
            while (runner != null)
            {
                Console.WriteLine(runner.Value);
                runner = runner.Next;
            }
        }

        private static LinkedList<int> RemoveZeroes(LinkedList<int> linkedList)
        {
            LinkedListNode<int> runner = linkedList.First;
            
            while (runner != null)
            {
                if (linkedList.First.Value == 0)
                {
                    runner = runner.Next;
                    linkedList.RemoveFirst();
                }
                else if (runner.Next != null && runner.Next.Value == 0)
                {
                    linkedList.Remove(runner.Next);
                }
                else
                    runner = runner.Next;
            }
            return linkedList;
        }
    }
}
