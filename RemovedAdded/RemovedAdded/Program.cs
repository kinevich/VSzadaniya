using System;
using System.Collections.Generic;
using System.Linq;

namespace RemovedAdded
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> collection = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            IEnumerable<int> modifiedCollection = new int[] { 1, 2, 3, 8, 9, 10 };

            Console.WriteLine("Original Dictionary");
            Array.ForEach(collection.ToArray(), Console.WriteLine);
            Console.WriteLine();

            Console.WriteLine("Modified Dictionary");
            Array.ForEach(modifiedCollection.ToArray(), Console.WriteLine);
            Console.WriteLine();

            (IEnumerable<int> added, IEnumerable<int> removed) tuple =
                GetAddedAndRemoved(collection, modifiedCollection);

            Console.WriteLine("Added");
            Array.ForEach(tuple.added.ToArray(), Console.WriteLine);
            Console.WriteLine();

            Console.WriteLine("Removed");
            Array.ForEach(tuple.removed.ToArray(), Console.WriteLine);
            Console.WriteLine();
        }

        private static (IEnumerable<int> added, IEnumerable<int> removed) GetAddedAndRemoved
            (IEnumerable<int> originalCollection, IEnumerable<int> modifiedCollection)
        {
            var existingItems = new HashSet<int>(originalCollection);
            var added = new List<int>();

            foreach (int item in modifiedCollection)
            {
                if (existingItems.Contains(item))
                    existingItems.Remove(item);
                else
                    added.Add(item);                
            }

            var removed = existingItems.ToArray();
            return (added.ToArray(), removed);
        }
    }
}


