using System;
using System.IO;

namespace TraversingDirectoryTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"";
            Traverse(path);
        }

        private static void Traverse(string path)
        {
            string[] directories = Directory.GetDirectories(path);

            if (directories == null || directories.Length == 0)
                return;

            foreach (string dir in directories)
            {
                Console.WriteLine(dir);
                Traverse(dir);
            }
        }
    }
}
