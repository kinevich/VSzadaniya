using System;
using System.Collections.Generic;
using System.Linq;

namespace RemovedAdded
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();

            Dictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(4, 5);
            dict.Add(5, 5);
            dict.Add(3, 5);
            dict.Add(1, 5);
            dict.Add(6, 5);
            dict.Add(10, 5);
            Dictionary<int, int> clonedDict = new Dictionary<int, int>(dict);

            Console.WriteLine("Original Dictionary");
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine();

            Dictionary<int, int> modifiedDict = service.Modify(clonedDict);

            Console.WriteLine("Modified Dictionary");
            foreach (KeyValuePair<int, int> kvp in modifiedDict)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine();

            Tuple<Dictionary<int, int>, Dictionary<int, int>> tuple = GetRemovedAndAdded(dict, modifiedDict);

            Console.WriteLine("Added");
            foreach (KeyValuePair<int, int> kvp in tuple.Item1)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine();

            Console.WriteLine("Removed");
            foreach (KeyValuePair<int, int> kvp in tuple.Item2)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine();
        }

        private static Tuple<Dictionary<int, int>, Dictionary<int, int>> GetRemovedAndAdded
            (Dictionary<int, int> originalDict, Dictionary<int, int> modifiedDict)
        {
            Dictionary<int, int> removed = new Dictionary<int, int>();
            Dictionary<int, int> added = new Dictionary<int, int>();

            foreach (KeyValuePair<int, int> kvp in originalDict)
            {
                if (!modifiedDict.ContainsKey(kvp.Key))
                    removed.Add(kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<int, int> kvp in modifiedDict)
            {
                if (!originalDict.ContainsKey(kvp.Key))
                    added.Add(kvp.Key, kvp.Value);
            }

            return new Tuple<Dictionary<int, int>, Dictionary<int, int>>(added, removed);
        }
    }

    class Service
    {
        public Dictionary<int, int> Modify(Dictionary<int, int> dict)
        {
            Random random = new Random();

            if (dict.Count > 0)
            {
                int numberOfAdded = random.Next(dict.Count);
                for (int i = 1; i <= numberOfAdded; ++i)
                {
                    dict.Add(random.Next(i + 4234), random.Next(i + 3432));
                }
            }

            if (dict.Count > 0)
            {
                int numberOfRemoved = random.Next(dict.Count);
                for (int i = 1; i <= numberOfRemoved; ++i)
                {
                    int randomIndex = random.Next(dict.Count);
                    int randomKey = dict.Keys.ElementAt(randomIndex);
                    dict.Remove(randomKey);
                }
            }

            return dict;
        }
    }
}


