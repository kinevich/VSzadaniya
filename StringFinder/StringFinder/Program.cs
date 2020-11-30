using System;
using System.IO;

namespace StringFinder
{
    class StringFinder
    {
        public delegate void Del(string name, int num);
        public event Del Found;

        public void Start(string directoryPath, string text)
        {
            foreach (string fileName in Directory.GetFiles(directoryPath))
            {
                string filePath = Path.GetFullPath(fileName);
                if (Path.GetExtension(filePath) == ".txt")
                {
                    string[] lines = File.ReadAllLines(filePath);
                    var lineNumber = Array.IndexOf(lines, text);
                    if (lineNumber >= 0)
                        Found(fileName, lineNumber);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"E:\Test_StringFinder";
            string text = "kostya";
            StringFinder stringFinder = new StringFinder();

            stringFinder.Found += StringFinder_Found;

            stringFinder.Start(directoryPath, text);
        }

        private static void StringFinder_Found(string name, int num)
        {
            Console.WriteLine($"TEXT FOUND. File: {name}. Line number: {num}.");
        }
    }

}
