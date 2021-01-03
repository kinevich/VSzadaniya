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

            Tuple<IEnumerable<int>, IEnumerable<int>> tuple =
                GetAddedAndRemoved(collection, modifiedCollection);

            Console.WriteLine("Added");
            Array.ForEach(tuple.Item1.ToArray(), Console.WriteLine);
            Console.WriteLine();

            Console.WriteLine("Removed");
            Array.ForEach(tuple.Item2.ToArray(), Console.WriteLine);
            Console.WriteLine();
        }

        private static Tuple<IEnumerable<int>, IEnumerable<int>> GetAddedAndRemoved
            (IEnumerable<int> originalCollection, IEnumerable<int> modifiedCollection)
        {
            HashSet<int> added = new HashSet<int>();
            HashSet<int> removed = new HashSet<int>();

            HashSet<int> original = originalCollection.ToHashSet();
            HashSet<int> modified = modifiedCollection.ToHashSet();

            foreach (int item in original)
            {
                if (!modified.Contains(item))
                    removed.Add(item);
            }

            foreach (int item in modified)
            {
                if (!original.Contains(item))
                    added.Add(item);
            }

            return new Tuple<IEnumerable<int>, IEnumerable<int>>(added, removed);
        }
    }
}


